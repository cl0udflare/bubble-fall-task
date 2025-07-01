using Progress.Data;

namespace Progress.Services
{
    public interface IProgressService
    {
        SettingsData Settings { get; }
        ProgressData Progress { get; }
        
        void SetData(SettingsData settings, ProgressData progress);
    }
}