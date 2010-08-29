using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Inferis.Diary.Plugins;
using Inferis.Diary.Plugins.Link;
using Inferis.Diary.Plugins.Paragraph;

namespace Inferis.Diary {
    public class DiaryConverter {
        public DiaryConverter()
        {
            Plugins = new List<IDiaryPlugin>() {
                new PlainLinkPlugin(),
                new ItalicsAndBoldPlugin(),
                new BlockQuoteParagraphPlugin(),
                new CenteredParagraphPlugin(),
                new ParagraphizerPlugin(),
            };
        }

        public IList<IDiaryPlugin> Plugins { get; private set; }

        public string ToHtml(string diary)
        {
            if (string.IsNullOrWhiteSpace(diary))
                return diary;

            return string.Join("\r\n", diary.Split(new[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(ParagraphHandler)
                .Union(new[] { "" }));
        }

        private string ParagraphHandler(string paragraph)
        {
            var pbuilder = new StringBuilder();

            var paragraphPlugins = new Stack<IDiaryParagraphPlugin>(Plugins.OfType<IDiaryParagraphPlugin>());
            var linePlugins = new Stack<IDiaryLinePlugin>(Plugins.OfType<IDiaryLinePlugin>());
            var linkPlugins = new Stack<IDiaryLinkPlugin>(Plugins.OfType<IDiaryLinkPlugin>());

            Action<IList<string>> prevYield = lines => {
                // do line plugins
                while (linePlugins.Count > 0) {
                    var plugin = linePlugins.Pop();
                    lines = lines.SelectMany(plugin.Handle).ToArray();
                }

                var linkRegex = new Regex(@"\[([^\]]+)\]");
                var result = lines.Select(l => linkRegex.Replace(l, me => {
                    var content = me.Groups[1].Value;
                    var plugin = linkPlugins.FirstOrDefault(lp => lp.CanHandle(content));
                    return plugin == null ? content : me.Result(plugin.Handle(content));
                }));

                // join final result into one string
                pbuilder.Append(string.Join("\r\n", result));
            };
            while (paragraphPlugins.Count > 0) {
                var plugin = paragraphPlugins.Pop();
                var wrappedYield = prevYield;
                prevYield = l => plugin.Handle(l, pbuilder, wrappedYield);
            }

            prevYield(paragraph.Split(new[] { "\r\n" }, StringSplitOptions.None));
            return pbuilder.ToString();
        }
    }

    //public static class BLinqExtensions {
    //    public static string JoinWithBuilder(this IEnumerable<string> strings, string seperator)
    //    {
    //        return strings.JoinWithBuilder(seperator, new StringBuilder()).ToString();
    //    }

    //    public static StringBuilder JoinWithBuilder(this IEnumerable<string> strings, string seperator, StringBuilder builder)
    //    {
    //        string marker = Guid.NewGuid().ToString();
    //        strings.Union(new[] { marker })
    //            .Aggregate((a, b) => {
    //                if (a != null) builder.Append(a);
    //                if (b != marker) {
    //                    builder.Append(seperator);
    //                    builder.Append(b);
    //                }
    //                return null;
    //            });

    //        return builder;
    //    }

    //    public static string JoinWithBuilder2(this IEnumerable<string> strings, string seperator)
    //    {
    //        return strings.JoinWithBuilder2(seperator, new StringBuilder()).ToString();
    //    }

    //    public static StringBuilder JoinWithBuilder2(this IEnumerable<string> strings, string seperator, StringBuilder builder)
    //    {
    //        var enumerator = strings.GetEnumerator();
    //        if (!enumerator.MoveNext())
    //            return builder;

    //        builder.Append(enumerator.Current);
    //        while (enumerator.MoveNext()) {
    //            builder.Append(seperator);
    //            builder.Append(enumerator.Current);
    //        }

    //        return builder;
    //    }
    //}
}
