namespace Inferis.Diary.Plugins {
    public interface IDiaryLinePlugin : IDiaryPlugin {
        string[] Handle(string sourceLine);
    }
}