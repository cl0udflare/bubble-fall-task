using System;
using System.Collections.Generic;
using Infrastructure.Signals;
using Logging;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class SceneInitializationInstaller : MonoInstaller
    {
        [SerializeField] private List<MonoBehaviour> _sceneServices;

        public override void InstallBindings()
        {
            Dictionary<Type, object> servicesMap = new();

            foreach (MonoBehaviour service in _sceneServices)
            {
                Type type = service.GetType();
                Container.BindInterfacesAndSelfTo(type).FromInstance(service).AsSingle();
                servicesMap[type] = service;
            }

            Container.BindInstance(servicesMap).AsSingle();
            Container.BindInterfacesAndSelfTo<SceneNotifier>().AsSingle()
                .WithArguments(servicesMap);
            
            DebugLogger.LogMessage(message: $"Installed", sender: this);
        }
    }
}