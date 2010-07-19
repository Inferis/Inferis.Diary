using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Inferis.Diary.Plugins.Paragraph {
    public class BlockQuoteParagraphPlugin : IDiaryParagraphPlugin {
        public void Handle(IList<string> sourceLines, StringBuilder sink, Action<IList<string>> yield)
        {
            var bqRegex = new Regex(@"^([ \t]*\>[ \t])", RegexOptions.Compiled);
            var bqTest = bqRegex.Match(sourceLines.First());
            if (bqTest.Success) {
                // it's a blockquote!
                sink.Append("<blockquote>\r\n");
                yield(sourceLines.Select(l => bqRegex.Replace(l, "")).ToList());
                sink.Append("</blockquote>\r\n");
            }
            else
                yield(sourceLines);
        }
    }
}
