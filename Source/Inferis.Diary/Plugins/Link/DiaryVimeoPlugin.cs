using System;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Inferis.Flickr;

namespace Inferis.Diary.Plugins.Link
{
    public class DiaryVimeoPlugin : IDiaryLinkPlugin
    {
        private readonly string apiKey;
        private static Regex regex;
        private MatchCache cached;

        static DiaryVimeoPlugin()
        {
            regex = new Regex(@"^(?:http\:\/\/)?(?:www\.)?vimeo\.com/([0-9]+)$", RegexOptions.IgnoreCase);
        }

        public DiaryVimeoPlugin()
        {
            this.apiKey = ConfigurationManager.AppSettings["DiaryFlickrPlugin.ApiKey"];
            if (string.IsNullOrEmpty(apiKey))
                throw new InvalidOperationException("No 'DiaryFlickrPlugin.ApiKey' appsetting found.");
        }

        public DiaryVimeoPlugin(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public IDiaryPluginMetadata ProvideMetadata()
        {
            return new DiaryPluginMetadata(new { UserId = apiKey });
        }

        public bool CanHandle(string source, DiaryMode mode)
        {
            var match = regex.Match(source);
            cached = new MatchCache(source, match);
            return match.Success;
        }

        public string Handle(string source, DiaryMode mode)
        {
            var fromCache = cached;
            var match = (fromCache != null && fromCache.Source.Equals(source)) ? fromCache.Match : regex.Match(source);

            var videoId = match.Groups[1].Value;

            var result = source;
            try {
                result = string.Format(@"<iframe src=""http://player.vimeo.com/video/{0}?byline=0&amp;portrait=0&amp;color=ff9933"" width=""500"" height=""281"" frameborder=""0""></iframe>",
                    videoId);

            }
            catch { }
            return result;
        }
    }
}
