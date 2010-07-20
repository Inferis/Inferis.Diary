namespace Inferis.Diary.Plugins {
    public interface IDiaryLinkPlugin : IDiaryPlugin {
        bool CanHandle(string source);
        string Handle(string source);
    }
}