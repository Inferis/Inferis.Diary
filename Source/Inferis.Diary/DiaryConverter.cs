using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Inferis.Diary.Plugins;
using Inferis.Diary.Plugins.Paragraph;
using Inferis.Kimalas.Data.Diary;

namespace Inferis.Diary {
    public class DiaryConverter {
        public DiaryConverter()
        {
            Plugins = new List<IDiaryPlugin>();
        }

        public IList<IDiaryPlugin> Plugins { get; private set; }

        public string ToHtml(string diary)
        {
            if (string.IsNullOrWhiteSpace(diary))
                return diary;

            return diary.Split(new[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(ParagraphHandler)
                .Union(new[] { "" })
                .JoinWithBuilder("\r\n");
        }

        private string ParagraphHandler(string paragraph)
        {
            var pbuilder = new StringBuilder();
            var lines = paragraph.Split(new[] { "\r\n" }, StringSplitOptions.None);

            var plugins = new Stack<IDiaryParagraphPlugin>(new IDiaryParagraphPlugin[] {
                new BlockQuoteParagraphPlugin(),
                new ParagraphizerPlugin(),
            });

            Action<IList<string>> prevYield = l => l.JoinWithBuilder("\r\n", pbuilder);
            while (plugins.Count > 0) {
                var plugin = plugins.Pop();
                var wrappedYield = prevYield;
                prevYield = l => plugin.Handle(l, pbuilder, wrappedYield);
            }

            prevYield(lines);

            return pbuilder.ToString();
        }

        private class ConcatPlugin : IDiaryParagraphPlugin {
            public void Handle(IList<string> sourceLines, StringBuilder sink, Action<IList<string>> yield)
            {
                sourceLines.JoinWithBuilder(" ", sink);
                Debug.Assert(yield == null, "yield should be null for ConcatPlugin.");
            }
        }
    }

    public static class BLinqExtensions {
        public static string JoinWithBuilder(this IEnumerable<string> strings, string seperator)
        {
            return strings.JoinWithBuilder(seperator, new StringBuilder()).ToString();
        }

        public static StringBuilder JoinWithBuilder(this IEnumerable<string> strings, string seperator, StringBuilder builder)
        {
            string marker = Guid.NewGuid().ToString();
            strings.Union(new[] { marker })
                .Aggregate((a, b) => {
                    if (a != null) builder.Append(a);
                    if (b != marker) {
                        builder.Append(seperator);
                        builder.Append(b);
                    }
                    return null;
                });

            return builder;
        }
    }
}
