using System;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Inferis.Flickr;

namespace Inferis.Diary.Plugins.Link
{
    public class DiaryFlickrSlideshowPlugin : IDiaryLinkPlugin
    {
        private readonly string apiKey;
        private static Regex regex;
        private MatchCache cached;

        static DiaryFlickrSlideshowPlugin()
        {
            regex = new Regex(@"^(?:http\:\/\/)?(?:www\.)?flickr\.com/photos/([^/]+)/sets/([0-9]+)/show/?$", RegexOptions.IgnoreCase);
        }

        public DiaryFlickrSlideshowPlugin()
        {
            this.apiKey = ConfigurationManager.AppSettings["DiaryFlickrPlugin.ApiKey"];
            if (string.IsNullOrEmpty(apiKey))
                throw new InvalidOperationException("No 'DiaryFlickrPlugin.ApiKey' appsetting found.");
        }

        public DiaryFlickrSlideshowPlugin(string apiKey)
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
            var setid = match.Groups[2].Value;

            return mode == DiaryMode.Html
                ? string.Format(@"<object width=""500"" height=""375""> <param name=""flashvars"" value=""offsite=true&lang=en-us&page_show_url=%2Fphotos%2F{0}%2Fsets%2F{1}%2Fshow%2F&page_show_back_url=%2Fphotos%2F{0}%2Fsets%2F{1}%2F&set_id={1}&jump_to=""></param> <param name=""movie"" value=""http://www.flickr.com/apps/slideshow/show.swf?v=71649""></param> <param name=""allowFullScreen"" value=""true""></param><embed type=""application/x-shockwave-flash"" src=""http://www.flickr.com/apps/slideshow/show.swf?v=71649"" allowFullScreen=""true"" flashvars=""offsite=true&lang=en-us&page_show_url=%2Fphotos%2F{0}%2Fsets%2F{1}%2Fshow%2F&page_show_back_url=%2Fphotos%2F{0}%2Fsets%2F{1}%2F&set_id={1}&jump_to="" width=""500"" height=""375""></embed></object>",
                    flickruser, setid)
                : source;
        }
    }
}
