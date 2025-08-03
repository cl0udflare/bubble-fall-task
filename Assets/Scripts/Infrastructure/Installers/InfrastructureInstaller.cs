using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.Loading;
using Infrastructure.Services.SceneLocator;
using Infrastructure.Services.SceneTransition;
using Logging;
using Zenject;

namespace Infrastructure.Installers
{
    public class InfrastructureInstaller : Installer<InfrastructureInstaller>
    {
        public override void InstallBindings()
        {
            BindServices();
            
            DebugLogger.LogMessage(message: $"Installed", sender: this);
        }
        
        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<LoaderService>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneLoaderService>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneTransitionService>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneServiceLocator>().AsSingle();
        }
    }
}
