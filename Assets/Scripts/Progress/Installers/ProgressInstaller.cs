using Logging;
using Progress.Services;
using Zenject;

namespace Progress.Installers
{
    public class ProgressInstaller : Installer<ProgressInstaller>
    {
        public override void InstallBindings()
        {
            BindServices();
            
            DebugLogger.LogMessage(message: $"Installed", sender: this);
        }
        
        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<ProgressService>().AsSingle();
            Container.BindInterfacesAndSelfTo<ProgressStorage>().AsSingle();
        }
    }
}
