﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Inferis.Diary.Plugins.Paragraph {
    public class CenteredParagraphPlugin : IDiaryParagraphPlugin {
        public void Handle(IList<string> sourceLines, DiaryMode mode, StringBuilder sink, Action<IList<string>> yield)
        {
            var bqRegex = new Regex(@"^([ \t]*\><(?:[ \t]|$))", RegexOptions.Compiled);
            var bqTest = bqRegex.Match(sourceLines.First());
            if (bqTest.Success) {
                // it's a blockquote!
                if (mode == DiaryMode.Html) {
                    sink.Append("<div align=\"center\">\r\n");
                    yield(sourceLines.Select(l => bqRegex.Replace(l, "")).ToList());
                    sink.Append("</div>\r\n");
                }
                else {
                    yield(sourceLines.Select(l => "    " + bqRegex.Replace(l, "")).ToList());
                }
            }
            else
                yield(sourceLines);
        }
    }
}
