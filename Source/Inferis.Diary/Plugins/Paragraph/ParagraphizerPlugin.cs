using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inferis.Diary.Plugins.Paragraph
{
    public class ParagraphizerPlugin : IDiaryParagraphPlugin
    {
        public void Handle(IList<string> sourceLines, DiaryMode mode, StringBuilder sink, Action<IList<string>> yield)
        {
            if (mode == DiaryMode.Html)
                HandleHtml(sourceLines, sink, yield);
            else
                HandlePlainText(sourceLines, sink, yield);
        }

        private void HandleHtml(IList<string> sourceLines, StringBuilder sink, Action<IList<string>> yield)
        {
            sink.Append("<p>");
            yield(sourceLines.Select(x => x + "<br/>\r\n").ToList());
            sink.Append("</p>\r\n");
        }

        private void HandlePlainText(IList<string> sourceLines, StringBuilder sink, Action<IList<string>> yield)
        {
            yield(sourceLines.Select(x => x + "\r\n").ToList());
            sink.Append("\r\n");
        }
    }
}
