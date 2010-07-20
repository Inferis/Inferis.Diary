using System.Text.RegularExpressions;

namespace Inferis.Diary
{
    internal class MatchCache
    {
        public MatchCache(string source, Match match)
        {
            Source = source;
            Match = match;
        }

        public string Source { get; set; }
        public Match Match { get; set; }
    }
}