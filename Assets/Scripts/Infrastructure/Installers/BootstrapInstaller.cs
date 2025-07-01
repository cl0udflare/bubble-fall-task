using Gameplay.Core.Ball;
using Gameplay.Core.Ball.Factory;
using Gameplay.ObjectPools.Factory;
using Gameplay.Services.Randoms;
using Gameplay.Services.StaticData;
using Gameplay.Services.Systems;
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
    public class BootstrapInstaller : MonoInstaller, IInitializable
    {
        public override void InstallBindings()
        {
            BindInfrastructureServices();
           
            BindGameplayServices();
            BindGameplayFactories();
           
            BindUIFactories();

            StateMachineInstaller.Install(Container);
            MediatorInstaller.Install(Container);

            DebugLogger.LogMessage(message: $"Installed", sender: this);
            
            Container.BindInterfacesAndSelfTo<GameBootstrapper>().AsSingle().NonLazy(); // Bootstrap
        }

        private void BindInfrastructureServices()
        {
            Container.BindInterfacesAndSelfTo<CurtainService>().FromComponentsInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<LoaderService>().AsSingle();
            Container.Bind<IProgressService>().To<ProgressService>().AsSingle();
            Container.BindInterfacesAndSelfTo<ProgressStorage>().AsSingle();
            Container.Bind<ISceneLoaderService>().To<SceneLoaderService>().AsSingle();
        }

        private void BindGameplayServices()
        {
            Container.BindInterfacesAndSelfTo<StaticDataService>().AsSingle();
            Container.Bind<ISystemFactory>().To<SystemFactory>().AsSingle();
            Container.Bind<IRandomService>().To<RandomService>().AsSingle();
        }

        private void BindGameplayFactories()
        {
            Container.Bind<IObjectPoolFactory>().To<ObjectPoolFactory>().AsSingle();
            Container.Bind<IBallFactory>().To<BallFactory>().AsSingle();
        }

        private void BindUIFactories()
        {
            Container.Bind<IWindowFactory>().To<WindowFactory>().AsSingle();
        }

        public void Initialize() => 
            Application.targetFrameRate = 60;
    }
}