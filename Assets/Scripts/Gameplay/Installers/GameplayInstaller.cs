using Gameplay.Core.Balls.Factory;
using Gameplay.ObjectPools.Factory;
using Gameplay.Services.Inputs;
using Gameplay.Services.StaticData;
using Gameplay.Services.Time;
using Gameplay.Services.UnityRandom;
using Infrastructure.Services.Systems;
using Logging;
using UnityEngine;
using Zenject;

namespace Gameplay.Installers
{
    public class GameplayInstaller : Installer<GameplayInstaller>
    {
        public override void InstallBindings()
        {
            BindServices();
            BindFactories();

            DebugLogger.LogMessage(message: $"Installed", sender: this);
        }

        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<StaticDataService>().AsSingle();
            Container.Bind<ISystemFactory>().To<SystemFactory>().AsSingle();
            Container.Bind<IRandomService>().To<RandomService>().AsSingle();
            Container.Bind<ITimeService>().To<UnityTimeService>().AsSingle();
            Container.BindInterfacesTo<InputService>().AsSingle();
        }

        private void BindFactories()
        {
            Container.Bind<IObjectPoolFactory>().To<ObjectPoolFactory>().AsSingle();
            Container.Bind<IBallFactory>().To<BallFactory>().AsSingle();
        }
    }
}