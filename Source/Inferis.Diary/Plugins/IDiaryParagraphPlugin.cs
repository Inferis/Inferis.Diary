using System;
using System.Collections.Generic;
using System.Text;

namespace Inferis.Diary.Plugins
{
    public interface IDiaryParagraphPlugin
    {
        void Handle(IList<string> sourceLines, StringBuilder sink, Action<IList<string>> yield);
    }
}