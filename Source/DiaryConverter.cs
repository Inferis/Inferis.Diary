using System.Collections.Generic;
using Inferis.Kimalas.Data.Diary;

namespace Inferis.Diary
{
    public class DiaryConverter
    {
        public DiaryConverter()
        {
            Plugins = new List<IDiaryPlugin>();
        }

        public IList<IDiaryPlugin> Plugins { get; private set; }

        public string ToHtml(string diary)
        {

            return "";
        }
    }
}
