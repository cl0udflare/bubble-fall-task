using Logging;
using UI.Curtain.Services;
using UI.Window.Factory;
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
            Container.Bind<IWindowFactory>().To<WindowFactory>().AsSingle();
        }
    }
}
