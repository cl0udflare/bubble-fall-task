using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using Logging;
using Progress.Data;
using UnityEngine;

namespace Progress.Services
{
    public class ProgressStorage : IProgressStorage
    {
        private const string FILE_NAME = "Progress.json";
        
        private IProgressService _progressService;

        public ProgressStorage(IProgressService progressService) => 
            _progressService = progressService;

        public UniTask InitializeAsync(CancellationToken cancellationToken = default)
        {
            Load();
            
            DebugLogger.LogMessage(message: $"Loaded", sender: this);
            return UniTask.CompletedTask;
        }
        
        public void Save()
        {
            ProgressContainer container = new ProgressContainer(
                _progressService.Settings, 
                _progressService.Progress);
            
            string json = JsonUtility.ToJson(container);
            File.WriteAllText(GetPath(), contents: json);
            
            DebugLogger.LogMessage($"Data saved to: {GetPath()}", sender: this);
        }

        public void Load()
        {
            string path = GetPath();
            
            if (!File.Exists(path))
            {
                DebugLogger.LogMessage($"No progress file found. Initializing with defaults.", sender: this);
                _progressService.SetData(new SettingsData(), new ProgressData());
                
                return;
            }

            string json = File.ReadAllText(path);
            ProgressContainer container = JsonUtility.FromJson<ProgressContainer>(json);

            _progressService.SetData(
                container.Settings ?? new SettingsData(), 
                container.Progress ?? new ProgressData());
            
            DebugLogger.LogMessage($"Progress loaded from: {path}", sender: this);
        }

        public void Reset()
        {
            string path = GetPath();

            if (File.Exists(path))
            {
                File.Delete(path);
                DebugLogger.LogMessage($"Progress file deleted: {path}", sender: this);
            }
            else
            {
                DebugLogger.LogMessage($"No progress file found to delete at: {path}", sender: this);
            }
        }
        
        private static string GetPath() => Path.Combine(Application.persistentDataPath, FILE_NAME);
    }
    
    [System.Serializable]
    internal class ProgressContainer
    {
        public SettingsData Settings;
        public ProgressData Progress;

        public ProgressContainer(SettingsData s, ProgressData p)
        {
            Settings = s;
            Progress = p;
        }
    }
}