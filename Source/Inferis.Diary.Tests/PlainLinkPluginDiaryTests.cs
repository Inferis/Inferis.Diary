using NUnit.Framework;

namespace Inferis.Diary.Tests
{
    [TestFixture]
    public class PlainLinkPluginDiaryTests
    {
        [Test]
        public void PlainHttpLink()
        {
            var converter = new DiaryConverter();
            var html = converter.ToHtml("Hierzo: [http://www.inferis.org/blog/dag-fien.html]");
            Assert.AreEqual(null, html);
        }

        [Test]
        public void PlainHttpLinkWithTitle()
        {
            var converter = new DiaryConverter();
            var html = converter.ToHtml("Hierzo: [Dag Fien!|http://www.inferis.org/blog/dag-fien.html]");
            Assert.AreEqual(null, html);
        }
    }
}
