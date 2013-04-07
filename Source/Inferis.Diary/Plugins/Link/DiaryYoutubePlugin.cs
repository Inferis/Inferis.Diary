using System.Text.RegularExpressions;

namespace Inferis.Diary.Plugins.Link
{
    public class DiaryYoutubePlugin : RegexBasedLinkPluginBase
    {
        private static Regex regex;
        private MatchCache cached;

        static DiaryYoutubePlugin()
        {
            regex = new Regex(@"^(?:https?:\/\/)?(?:www\.)?youtube.com\/watch\?v=([^?&]+)(?:&.*)?$", RegexOptions.IgnoreCase);
        }

        public DiaryYoutubePlugin() : base(regex)
        {
        }

        protected override string Handle(Match match, DiaryMode mode)
        {
            var videoId = match.Groups[1].Value;
            return string.Format(@"<iframe width=""500"" height=""281"" src=""http://www.youtube-nocookie.com/embed/{0}?rel=0"" frameborder=""0"" allowfullscreen></iframe>",
                videoId);
        }

        protected override bool Supports(DiaryMode mode)
        {
            return mode == DiaryMode.Html;
        }
    }
}
