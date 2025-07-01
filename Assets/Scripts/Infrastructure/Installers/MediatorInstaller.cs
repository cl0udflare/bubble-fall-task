using Logging;
using Zenject;

namespace Infrastructure.Installers
{
    public class MediatorInstaller : Installer<MediatorInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Mediator.Core.Mediator>().AsSingle();

            BindHandlers();
            
            DebugLogger.LogMessage(message: $"Installed", sender: this);
        }

        private void BindHandlers() { }
    }
}