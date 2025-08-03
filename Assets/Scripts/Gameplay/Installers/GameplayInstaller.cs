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
            Container.BindInterfacesAndSelfTo<SystemFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<RandomService>().AsSingle();
            Container.BindInterfacesAndSelfTo<UnityTimeService>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputService>().AsSingle();
        }

        private void BindFactories()
        {
            Container.BindInterfacesAndSelfTo<ObjectPoolFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<BallFactory>().AsSingle();
        }
    }
}