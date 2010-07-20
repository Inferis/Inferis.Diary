using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Inferis.Diary.Plugins.Paragraph {
    public class ItalicsAndBoldPlugin : IDiaryParagraphPlugin {
        private static Regex bold, italics;

        static ItalicsAndBoldPlugin()
        {
            bold = new Regex(@"(\*\*|__)(?=\S)([^\r]*?\S[*_]*)\1");
            italics = new Regex(@"(\*|_)(?=\S)([^\r]*?\S)\1");
        }

        public void Handle(IList<string> sourceLines, StringBuilder sink, Action<IList<string>> yield)
        {
            var marker = Guid.NewGuid().ToString("N");
            var result = string.Join(marker, sourceLines);
            result = bold.Replace(result, "<strong>$2</strong>");
            result = italics.Replace(result, "<em>$2</em>");
            yield(result.Split(new[] { marker }, StringSplitOptions.None));
        }
    }
}
