namespace Inferis.Diary.Plugins {
    public interface IDiaryLinkPlugin : IDiaryPlugin {
        bool CanHandle(string source, DiaryMode mode);
        string Handle(string source, DiaryMode mode);
    }
}