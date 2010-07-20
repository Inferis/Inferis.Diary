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
            regex = new Regex(@"^(?:([^\|]+?)\|)?((?:f|ht)tp://[^/]+/?.+?)$");
        }

        public PlainLinkPlugin()
            : base(regex)
        {
        }

        protected override string Handle(Match match)
        {
            var url = match.Groups[2].Value;
            var text = match.Groups[1].Value;
            if (string.IsNullOrWhiteSpace(text)) text = url;
            return string.Format("<a href=\"{0}\">{1}</a>", url, text);
        }
    }
}
