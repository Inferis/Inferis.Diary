using System;
using System.Collections.Generic;
using System.Text;

namespace Inferis.Diary.Plugins {
    public interface IDiaryParagraphPlugin : IDiaryPlugin {
        void Handle(IList<string> sourceLines, DiaryMode mode, StringBuilder sink, Action<IList<string>> yield);
    }
}