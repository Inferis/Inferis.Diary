using System;
using System.Collections.Generic;
using System.Text;

namespace Inferis.Diary.Plugins.Paragraph {
    public class ParagraphizerPlugin : IDiaryParagraphPlugin {
        public void Handle(IList<string> sourceLines, StringBuilder sink, Action<IList<string>> yield)
        {
            sink.Append("<p>");
            yield(sourceLines);
            sink.Append("</p>\r\n");
        }
    }
}
