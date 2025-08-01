using Gameplay.Installers;
using Infrastructure.Bootstrapper;
using Logging;
using Progress.Installers;
using UI.Installers;
using Zenject;

namespace Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            UIInstaller.Install(Container);
            InfrastructureInstaller.Install(Container);
            ProgressInstaller.Install(Container);
            GameplayInstaller.Install(Container);
            StateMachineInstaller.Install(Container);
            MediatorInstaller.Install(Container);

            DebugLogger.LogMessage(message: $"All Installed", sender: this);

            Container.BindInterfacesAndSelfTo<GameBootstrapper>().AsSingle().NonLazy(); // Bootstrap
        }
    }
}