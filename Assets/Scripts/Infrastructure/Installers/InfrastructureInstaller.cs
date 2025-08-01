using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.Loading;
using Infrastructure.Signals;
using Logging;
using Zenject;

namespace Infrastructure.Installers
{
    public class InfrastructureInstaller : Installer<InfrastructureInstaller>
    {
        public override void InstallBindings()
        {
            BindServices();
            BindSignal();
            
            DebugLogger.LogMessage(message: $"Installed", sender: this);
        }
        
        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<LoaderService>().AsSingle();
            Container.Bind<ISceneLoaderService>().To<SceneLoaderService>().AsSingle();
        }
        
        private void BindSignal()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<SceneReadySignal>()
                .OptionalSubscriber()
                .RunAsync();
        }
    }
}
