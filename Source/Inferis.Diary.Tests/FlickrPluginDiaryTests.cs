using Inferis.Diary.Plugins.Link;
using NUnit.Framework;

namespace Inferis.Diary.Tests
{
    [TestFixture]
    public class FlickrPluginDiaryTests
    {
        [Test]
        public void DefaultPhotoLink()
        {
            var converter = new DiaryConverter();
            converter.Plugins.Add(new DiaryFlickrPlugin("b58737ad0749d66ea1fae8a6f568aac8"));
            var html = converter.ToHtml("Hierzo: [http://www.flickr.com/photos/fievertelt/4805878246]");
            Assert.AreEqual(null, html);
        }

        [Test]
        public void DefaultPhotoLinkWithTrailingSlash()
        {
            var converter = new DiaryConverter();
            converter.Plugins.Add(new DiaryFlickrPlugin("b58737ad0749d66ea1fae8a6f568aac8"));
            var html = converter.ToHtml("Hierzo: [http://www.flickr.com/photos/fievertelt/4805878246/|small]");
            Assert.AreEqual(null, html);
        }
    }
}
