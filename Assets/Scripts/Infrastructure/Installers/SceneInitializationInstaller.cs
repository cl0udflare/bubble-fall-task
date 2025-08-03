using System.Collections.Generic;
using Infrastructure.Services.SceneLocator;
using Logging;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class SceneInitializationInstaller : MonoInstaller
    {
        [SerializeField] private List<MonoBehaviour> _sceneServices;
        
        private ISceneServiceLocator _serviceLocator;

        [Inject]
        private void Construct(ISceneServiceLocator serviceLocator) => 
            _serviceLocator = serviceLocator;

        public override void InstallBindings()
        {
            _serviceLocator.Clear();

            foreach (MonoBehaviour service in _sceneServices)
            {
                Container.BindInterfacesAndSelfTo(service.GetType()).FromInstance(service).AsSingle();
                _serviceLocator.Register(service);
            }
            
            DebugLogger.LogMessage(message: $"Installed", sender: this);
        }
    }
}