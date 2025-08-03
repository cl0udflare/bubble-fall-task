using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Infrastructure.Bootstrapper
{
    public class InitializationManagement
    {
        private readonly List<IInitializableAsync> _services;

        public InitializationManagement(List<IInitializableAsync> services)
        {
            _services = services;
        }

        public async UniTask InitializeAsync(Action<float> onProgress, CancellationToken token = default)
        {
            float step = 1f / _services.Count;
            float progress = 0f;

            foreach (IInitializableAsync service in _services)
            {
                await service.InitializeAsync(token);
                progress += step;
                onProgress?.Invoke(progress);
            }
        }
    }
}