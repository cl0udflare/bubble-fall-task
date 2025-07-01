using Infrastructure.States;
using Infrastructure.States.GameStates;
using Logging;
using Zenject;

namespace Infrastructure.Installers
{
    public class StateMachineInstaller : Installer<StateMachineInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
            
            BindStates();
            
            DebugLogger.LogMessage(message: $"Installed", sender: this);
        }

        private void BindStates()
        {
            Container.Bind<GameplayState>().AsSingle();
        }
    }
}
