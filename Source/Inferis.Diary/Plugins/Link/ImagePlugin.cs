using System.Text.RegularExpressions;
using System.Web;

namespace Inferis.Diary.Plugins.Link {
    public class ImagePlugin : RegexBasedLinkPluginBase {
        private static readonly Regex regex;

        static ImagePlugin()
        {
            regex = new Regex(@"^(?:([^!]+))?!(https?:\/\/[^\/]+/?.+?\.(?:jpg|jpeg|png|gif|xbm))$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        public ImagePlugin()
            : base(regex)
        {
        }

        protected override bool Supports(DiaryMode mode)
        {
            return mode == DiaryMode.Html;
        }

        protected override string Handle(Match match, DiaryMode mode)
        {
            var url = match.Groups[2].Value;
            var alt = match.Groups[1].Value;
            if (mode == DiaryMode.Html) {
                return string.Format("<img src=\"{0}\" alt=\"{1}\" style='max-width: 600px'>", url, HttpUtility.HtmlEncode(alt));
            }

            return string.IsNullOrEmpty(alt) 
                ? url 
                : string.Format("{1} ({0})", url, alt);
        }
    }
}
