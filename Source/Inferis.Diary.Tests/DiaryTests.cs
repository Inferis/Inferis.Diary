using NUnit.Framework;

namespace Inferis.Diary.Tests
{
    [TestFixture]
    public class DiaryTests
    {
        [Test]
        public void Null_Should_Return_Null()
        {
            var converter = new DiaryConverter();
            var html = converter.ToHtml(null);
            Assert.AreEqual(null, html);
        }

        [Test]
        public void EmptyString_Should_Return_EmptyString()
        {
            var converter = new DiaryConverter();
            var html = converter.ToHtml("");
            Assert.AreEqual("", html);
        }

        [Test]
        public void SingleLine_ConvertsTo_SingleParagraphs()
        {
            var converter = new DiaryConverter();
            var html = converter.ToHtml("Hello there, I'm Tom.");
            Assert.AreEqual("<p>Hello there, I'm Tom.</p>\r\n\r\n", html);
        }

        [Test]
        public void SeperatedLines_ConvertsTo_MultipleParagraphs()
        {
            var converter = new DiaryConverter();
            var html = converter.ToHtml("Hello there, I'm Tom.\r\n\r\nHow are you doing?\r\n\r\nFine thanks.");
            Assert.AreEqual("<p>Hello there, I'm Tom.</p>\r\n\r\n<p>How are you doing?</p>\r\n\r\n<p>Fine thanks.</p>\r\n\r\n", html);
        }

        [Test]
        public void ConsequtiveLines_ConvertsTo_SingleParagraph_WithoutBreaks()
        {
            var converter = new DiaryConverter();
            var html = converter.ToHtml("Hello there, I'm Tom.\r\nHow are you doing?\r\nFine thanks.");
            Assert.AreEqual("<p>Hello there, I'm Tom.\r\nHow are you doing?\r\nFine thanks.</p>\r\n\r\n", html);
        }

        [Test]
        public void Stuffwithblockquote()
        {
            var converter = new DiaryConverter();
            var html = converter.ToHtml("> Hello there, I'm Tom.\r\n> How are you doing?\r\n> Fine thanks.");
            Assert.AreEqual("<blockquote>\r\n<p>Hello there, I'm Tom.\r\nHow are you doing?\r\nFine thanks.</p>\r\n</blockquote>\r\n\r\n", html);
        }
    }
}
