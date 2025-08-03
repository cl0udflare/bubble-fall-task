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
            InfrastructureInstaller.Install(Container);
            ProgressInstaller.Install(Container);
            UIInstaller.Install(Container);
            GameplayInstaller.Install(Container);
            MediatorInstaller.Install(Container);
            StateMachineInstaller.Install(Container);

            DebugLogger.LogMessage(message: $"Installed", sender: this);

            Container.BindInterfacesAndSelfTo<GameBootstrapper>().AsSingle().NonLazy(); // Bootstrap
        }
    }
}