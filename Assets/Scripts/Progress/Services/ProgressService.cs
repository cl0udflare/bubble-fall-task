using Progress.Data;

namespace Progress.Services
{
    public class ProgressService : IProgressService
    {
        public SettingsData Settings { get; private set; }
        public ProgressData Progress { get; private set; }
        
        public void SetData(SettingsData settings, ProgressData progress)
        {
            Settings = settings;
            Progress = progress;
        }
    }
}