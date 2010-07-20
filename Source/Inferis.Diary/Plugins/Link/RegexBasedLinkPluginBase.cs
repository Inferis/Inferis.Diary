using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Inferis.Diary.Plugins.Link {
    public abstract class RegexBasedLinkPluginBase : IDiaryLinkPlugin {
        private readonly Regex[] regexes;
        private MatchCache cached;

        protected RegexBasedLinkPluginBase(Regex regex)
            : this(new[] { regex })
        {

        }

        protected RegexBasedLinkPluginBase(Regex[] regexes)
        {
            this.regexes = regexes;
        }

        public bool CanHandle(string source)
        {
            foreach (var regex in regexes) {
                var match = regex.Match(source);
                cached = new MatchCache(source, match);
                if (match.Success)
                    return true;
            }

            return true;
        }

        public string Handle(string source)
        {
            var fromCache = cached;
            Match match = null;
            if (fromCache != null && fromCache.Source.Equals(source))
                match = fromCache.Match;
            else {
                foreach (var regex in regexes) {
                    var tryMatch = regex.Match(source);
                    if (tryMatch.Success) {
                        match = tryMatch;
                    }
                }
            }

            if (match == null)
                return source;

            return Handle(match);
        }

        protected abstract string Handle(Match match);
    }
}
