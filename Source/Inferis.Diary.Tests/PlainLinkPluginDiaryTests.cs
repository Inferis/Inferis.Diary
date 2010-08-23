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
            var html = converter.ToHtml("Hierzo: [http://www.inferis.org/blog/dag-fien.html], en nog!");
            Assert.AreEqual("<p>Hierzo: <a href=\"http://www.inferis.org/blog/dag-fien.html\">http://www.inferis.org/blog/dag-fien.html</a>, en nog!</p>\r\n\r\n", html);
        }

        [Test]
        public void PlainHttpLinkWithTitle()
        {
            var converter = new DiaryConverter();
            var html = converter.ToHtml("Hierzo: [Dag Fien!|http://www.inferis.org/blog/dag-fien.html], en nog!");
            Assert.AreEqual("<p>Hierzo: <a href=\"http://www.inferis.org/blog/dag-fien.html\">Dag Fien!</a>, en nog!</p>\r\n\r\n", html);
        }

        [Test]
        public void HttpLinkWithQueryString()
        {
            var converter = new DiaryConverter();
            var html = converter.ToHtml("Hierzo: [http://www.inferis.org/blog/post.aspx?id=1&id2=2], en nog!");
            Assert.AreEqual("<p>Hierzo: <a href=\"http://www.inferis.org/blog/post.aspx?id=1&id2=2\">http://www.inferis.org/blog/post.aspx?id=1&id2=2</a>, en nog!</p>\r\n\r\n", html);
        }

        [Test]
        public void HttpLinkWithQueryStringAndTitle()
        {
            var converter = new DiaryConverter();
            var html = converter.ToHtml("Hierzo: [Dag Fien!|http://www.inferis.org/blog/post.aspx?id=1&id2=2], en nog!");
            Assert.AreEqual("<p>Hierzo: <a href=\"http://www.inferis.org/blog/post.aspx?id=1&id2=2\">Dag Fien!</a>, en nog!</p>\r\n\r\n", html);
        }
    }
}
