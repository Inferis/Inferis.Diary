using NUnit.Framework;

namespace Inferis.Diary.Tests
{
    [TestFixture]
    public class PlainLinkPluginDiaryTests
    {
        [Test]
        public void WithoutTrailingBracket_DoesNotConvertToAHref()
        {
            var converter = new DiaryConverter();
            var html = converter.ToHtml("Hierzo: [http://www.inferis.org/blog/dag-fien.html, en nog!");
            Assert.AreEqual("<p>Hierzo: [http://www.inferis.org/blog/dag-fien.html, en nog!</p>\r\n\r\n", html);
        }

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

        [Test]
        public void LinkWithUniCode()
        {
            var converter = new DiaryConverter();
            var html = converter.ToHtml("Daring Fireball article at [http://✪df.ws/g21], worth a read!");
            Assert.AreEqual("<p>Daring Fireball article at <a href=\"http://✪df.ws/g21\">http://✪df.ws/g21</a>, worth a read!</p>\r\n\r\n", html);
        }

        [Test]
        public void LinkWithUniCodeWithTitle()
        {
            var converter = new DiaryConverter();
            var html = converter.ToHtml("Daring Fireball [article|http://✪df.ws/g21], worth a read!");
            Assert.AreEqual("<p>Daring Fireball <a href=\"http://✪df.ws/g21\">article</a>, worth a read!</p>\r\n\r\n", html);
        }

        [Test]
        public void FtpLink()
        {
            var converter = new DiaryConverter();
            var html = converter.ToHtml("FTP: [ftp://ftp.inferis.org/muh/hah/], en nog!");
            Assert.AreEqual("<p>FTP: <a href=\"ftp://ftp.inferis.org/muh/hah/\">ftp://ftp.inferis.org/muh/hah/</a>, en nog!</p>\r\n\r\n", html);
        }

        [Test]
        public void FtpLinkWithTitle()
        {
            var converter = new DiaryConverter();
            var html = converter.ToHtml("FTP: [Download here|ftp://ftp.inferis.org/muh/hah/], much fun!");
            Assert.AreEqual("<p>FTP: <a href=\"ftp://ftp.inferis.org/muh/hah/\">Download here</a>, much fun!</p>\r\n\r\n", html);
        }

        [Test]
        public void MailLink()
        {
            var converter = new DiaryConverter();
            var html = converter.ToHtml("Mail me at [mailto:tom@inferis.org], for info!");
            Assert.AreEqual("<p>Mail me at <a href=\"mailto:tom@inferis.org\">tom@inferis.org</a>, for info!</p>\r\n\r\n", html);
        }

        [Test]
        public void MailLinkWithTitle()
        {
            var converter = new DiaryConverter();
            var html = converter.ToHtml("Mail [me|mailto:tom@inferis.org], for info!");
            Assert.AreEqual("<p>Mail <a href=\"mailto:tom@inferis.org\">me</a>, for info!</p>\r\n\r\n", html);
        }
    }
}
