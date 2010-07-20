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
            var html = converter.ToHtml("Hierzo: [http://www.flickr.com/photos/fievertelt/4805878246]");
            Assert.AreEqual(null, html);
        }

        [Test]
        public void DefaultPhotoLinkWithTrailingSlash()
        {
            var converter = new DiaryConverter();
            var html = converter.ToHtml("Hierzo: [http://www.flickr.com/photos/fievertelt/4805878246/|small]");
            Assert.AreEqual(null, html);
        }
    }
}
