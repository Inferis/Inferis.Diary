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
        public void GreaterThanAtBeginningOfFirstLine_Creates_Blockquote()
        {
            var converter = new DiaryConverter();
            var html = converter.ToHtml("> Hello there, I'm Tom.\r\nHow are you doing?\r\nFine thanks.");
            Assert.AreEqual("<blockquote>\r\n<p>Hello there, I'm Tom.\r\nHow are you doing?\r\nFine thanks.</p>\r\n</blockquote>\r\n\r\n", html);
        }

        [Test]
        public void Blockquote_Removes_GreaterThan_AtBeginningOfEachLine()
        {
            var converter = new DiaryConverter();
            var html = converter.ToHtml("> Hello there, I'm Tom.\r\n> How are you doing?\r\n> Fine thanks.");
            Assert.AreEqual("<blockquote>\r\n<p>Hello there, I'm Tom.\r\nHow are you doing?\r\nFine thanks.</p>\r\n</blockquote>\r\n\r\n", html);
        }

        [Test]
        public void Asterisk_Creates_EmTag()
        {
            var converter = new DiaryConverter();
            var html = converter.ToHtml("Dit is gewoon, *dit is bold*, en terug gewoon.");
            Assert.AreEqual("<p>Dit is gewoon, <em>dit is bold</em>, en terug gewoon.</p>\r\n\r\n", html);
        }

        [Test]
        public void Asterisk_OverMultipleLines_Creates_EmTag()
        {
            var converter = new DiaryConverter();
            var html = converter.ToHtml("Dit is gewoon, *dit is\r\nbold*, en terug gewoon.");
            Assert.AreEqual("<p>Dit is gewoon, <em>dit is\r\nbold</em>, en terug gewoon.</p>\r\n\r\n", html);
        }

        [Test]
        public void DoubleAsterisk_Creates_StrongTag()
        {
            var converter = new DiaryConverter();
            var html = converter.ToHtml("Dit is gewoon, **dit is bold**, en terug gewoon.");
            Assert.AreEqual("<p>Dit is gewoon, <strong>dit is bold</strong>, en terug gewoon.</p>\r\n\r\n", html);
        }

        [Test]
        public void Asterisk_WithNoEnd_DoesNotCreate_EmTag()
        {
            var converter = new DiaryConverter();
            var html = converter.ToHtml("Dit is gewoon, **dit is bold, en terug gewoon.");
            Assert.AreEqual("<p>Dit is gewoon, **dit is bold, en terug gewoon.</p>\r\n\r\n", html);
        }

        [Test]
        public void DoubleAsterisk_OverMultipleLines_Creates_StrongTag()
        {
            var converter = new DiaryConverter();
            var html = converter.ToHtml("Dit is gewoon, **dit is bold**, en terug gewoon.");
            Assert.AreEqual("<p>Dit is gewoon, <strong>dit is bold</strong>, en terug gewoon.</p>\r\n\r\n", html);
        }

        [Test]
        public void DoubleAsterisk_WithNoEnd_DoesNotCreate_StrongTag()
        {
            var converter = new DiaryConverter();
            var html = converter.ToHtml("Dit is gewoon, **dit is bold, en terug gewoon.");
            Assert.AreEqual("<p>Dit is gewoon, **dit is bold, en terug gewoon.</p>\r\n\r\n", html);
        }

        [Test]
        public void DoubleAsterisk_SpreadOverMultipleParagraphs_DoesNotCreate_StrongTag()
        {
            var converter = new DiaryConverter();
            var html = converter.ToHtml("Dit is gewoon, **dit is bold, en terug gewoon.\r\n\r\nEn dit is terug bold.** Of niet?");
            Assert.AreEqual("<p>Dit is gewoon, **dit is bold, en terug gewoon.</p>\r\n\r\n<p>En dit is terug bold.** Of niet?</p>\r\n\r\n", html);
        }
    }
}
