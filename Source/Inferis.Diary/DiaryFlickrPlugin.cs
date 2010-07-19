using Inferis.Kimalas.Data.Diary;

namespace Inferis.Diary
{
    public class DiaryFlickrPlugin : IDiaryPlugin
    {
        private readonly string flickrUserId;

        public DiaryFlickrPlugin(string flickrUserId)
        {
            this.flickrUserId = flickrUserId;
        }

        public IDiaryPluginMetadata ProvideMetadata()
        {
            return new DiaryPluginMetadata(new { UserId = flickrUserId });
        }
    }
}
