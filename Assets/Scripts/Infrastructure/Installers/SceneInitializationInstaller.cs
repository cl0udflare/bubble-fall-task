using System.Collections.Generic;
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
            foreach (MonoBehaviour service in _sceneServices) 
                Container.BindInterfacesAndSelfTo(service.GetType()).FromInstance(service).AsSingle();
                
            DebugLogger.LogMessage(message: $"Installed", sender: this);
        }
    }
}