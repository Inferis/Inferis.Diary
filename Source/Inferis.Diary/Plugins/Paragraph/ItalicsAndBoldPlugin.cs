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

        public void Handle(IList<string> sourceLines, DiaryMode mode, StringBuilder sink, Action<IList<string>> yield)
        {
            var marker = Guid.NewGuid().ToString("N");
            var result = string.Join(marker, sourceLines);
            var boldReplacement = mode == DiaryMode.Html ? "<strong>$2</strong>" : "$2";
            var italicsReplacement = mode == DiaryMode.Html ? "<strong>$2</strong>" : "$2";
            result = bold.Replace(result, boldReplacement);
            result = italics.Replace(result, italicsReplacement);
            yield(result.Split(new[] { marker }, StringSplitOptions.None));
        }
    }
}
