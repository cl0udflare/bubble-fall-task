using System;
using System.Collections.Generic;
using Zenject;

namespace Infrastructure.Signals
{
    public class SceneNotifier : IInitializable
    {
        private readonly SignalBus _signalBus;
        private readonly Dictionary<Type, object> _services;

        public SceneNotifier(SignalBus signalBus, Dictionary<Type, object> services)
        {
            _signalBus = signalBus;
            _services = services;
        }

        public void Initialize() => 
            _signalBus.Fire(new SceneReadySignal(_services));
    }
}