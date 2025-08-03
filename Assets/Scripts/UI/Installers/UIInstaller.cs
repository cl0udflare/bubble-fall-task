using Logging;
using UI.Services;
using UI.Windows.Factory;
using Zenject;

namespace UI.Installers
{
    public class UIInstaller : Installer<UIInstaller>
    {
        public override void InstallBindings()
        {
            BindServices();
            BindFactories();
            
            DebugLogger.LogMessage(message: $"Installed", sender: this);
        }
        
        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<CurtainService>().FromComponentsInHierarchy().AsSingle();
        }
        
        private void BindFactories()
        {
            Container.BindInterfacesAndSelfTo<WindowFactory>().AsSingle();
        }
    }
}
