using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inferis.Diary.Plugins.Link;
using NUnit.Framework;

namespace Inferis.Diary.Tests
{
    [TestFixture]
    class ImagePluginTests
    {
        [Test]
        public void X()
        {
            var converter = new DiaryConverter();
            var html = converter.ToHtml("Hierzo: [hello there!http://f.cl.ly/items/0D023Q0g2q3x3e3l0a2e/Image%202012.12.29%2022:45:42.png]");
            Assert.AreEqual(null, html);
        }

        [Test]
        public void X2()
        {
            var converter = new DiaryConverter();
            converter.Plugins.Add(new DiaryYoutubePlugin());
            var html = converter.ToHtml("Hierzo: [http://www.youtube.com/watch?v=1ft-KKKwrvU]");
            Assert.AreEqual(null, html);
        }
    }
}
