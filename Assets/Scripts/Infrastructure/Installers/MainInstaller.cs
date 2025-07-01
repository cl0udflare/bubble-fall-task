using Gameplay.ObjectPools.Factory;
using Gameplay.Services.StaticData;
using Infrastructure.Bootstrapper;
using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.Loading;
using Logging;
using Progress.Services;
using UI.Curtain.Services;
using UI.Window.Factory;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class MainInstaller : MonoInstaller, IInitializable
    {
        public override void InstallBindings()
        {
            BindServices();
            BindFactories();

            StateMachineInstaller.Install(Container);
            MediatorInstaller.Install(Container);

            DebugLogger.LogMessage(message: $"Installed", sender: this);
            
            Container.BindInterfacesAndSelfTo<GameBootstrapper>().AsSingle().NonLazy(); // Bootstrap
        }

        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<CurtainService>().FromComponentsInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<LoaderService>().AsSingle();
            Container.Bind<IProgressService>().To<ProgressService>().AsSingle();
            Container.BindInterfacesAndSelfTo<ProgressStorage>().AsSingle();
            Container.BindInterfacesAndSelfTo<StaticDataService>().AsSingle();
            Container.Bind<ISceneLoaderService>().To<SceneLoaderService>().AsSingle();
        }

        private void BindFactories()
        {
            Container.Bind<IObjectPoolFactory>().To<ObjectPoolFactory>().AsSingle();
            Container.Bind<IWindowFactory>().To<WindowFactory>().AsSingle();
        }

        public void Initialize() => 
            Application.targetFrameRate = 90;
    }
}