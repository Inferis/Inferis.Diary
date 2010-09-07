using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Inferis.Diary.Plugins.Link {
    public class PlainLinkPlugin : RegexBasedLinkPluginBase {
        private static Regex regex;

        static PlainLinkPlugin()
        {
            regex = new Regex(@"^(?:([^\|]+?)\|)?((?:(?:f|ht)tp://|mailto:)[^/]+/?.+?)$");
        }

        public PlainLinkPlugin()
            : base(regex)
        {
        }

        protected override bool Supports(DiaryMode mode)
        {
            return true;
        }

        protected override string Handle(Match match, DiaryMode mode)
        {
            var url = match.Groups[2].Value;
            var text = match.Groups[1].Value;
            if (mode == DiaryMode.Html) {
                if (string.IsNullOrWhiteSpace(text)) text = url;
                return string.Format("<a href=\"{0}\">{1}</a>", url, Regex.Replace(text, "^mailto:", ""));
            }

            if (string.IsNullOrWhiteSpace(text))
                return Regex.Replace(url, "^mailto:", "");
            return string.Format("{1} ({0})", url, Regex.Replace(text, "^mailto:", ""));
        }
    }
}
