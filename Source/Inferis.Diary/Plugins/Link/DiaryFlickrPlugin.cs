using System;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Inferis.Flickr;

namespace Inferis.Diary.Plugins.Link
{
    public class DiaryFlickrPlugin : IDiaryLinkPlugin
    {
        private readonly string apiKey;
        private static Regex regex;
        private MatchCache cached;

        static DiaryFlickrPlugin()
        {
            regex = new Regex(@"^(?:http\:\/\/)?(?:www\.)?flickr\.com/photos/([^/]+)/([0-9]+).*?(?:\|([^\|]+?))*$", RegexOptions.IgnoreCase);
        }

        public DiaryFlickrPlugin()
        {
            this.apiKey = ConfigurationManager.AppSettings["DiaryFlickrPlugin.ApiKey"];
            if (string.IsNullOrEmpty(apiKey))
                throw new InvalidOperationException("No 'DiaryFlickrPlugin.ApiKey' appsetting found.");
        }

        public DiaryFlickrPlugin(string apiKey)
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

            var flickruser = match.Groups[1].Value;
            var photoid = match.Groups[2].Value;

            var options = match.Groups.OfType<Group>().Skip(2).Select(g => g.Value).ToArray();
            var wanted = options.AnyOfOrDefault(new[] { "small", "medium", "large", "square" }) ?? "medium";

            var result = source;
            try {
                XDocument sizes = FlickrApi.WithKey(apiKey).Photos.GetSizes(photoId: photoid);

                var image = sizes.Descendants("size").First(sz => string.Compare(sz.Attribute("label").Value, wanted, true) == 0);
                var jpg = image.Attribute("source").Value;
                var width = image.Attribute("width").Value;
                var height = image.Attribute("height").Value;

                if (mode == DiaryMode.Html) {
                    result = string.Format("<img src=\"{0}\" width=\"{1}\" height=\"{2}\" />", jpg, width, height);

                    XDocument info = FlickrApi.WithKey(apiKey).Photos.GetInfo(photoId: photoid);

                    var url = info.Descendants("url").Where(p => p.Attribute("type").Value == "photopage").First().Value;
                    result = string.Format("<a href=\"{0}\">{1}</a>", url, result);
                }
                else {
                    result = jpg;
                }
            }
            catch { }
            return result;
        }
    }
}
