using System.Threading;
using Cysharp.Threading.Tasks;
using Logging;
using UnityEngine;

namespace Infrastructure.Services.AssetManagement
{
    public class LoaderService : ILoaderService
    {
        public UniTask InitializeAsync(CancellationToken cancellationToken = default)
        {
            return UniTask.CompletedTask;
        }

        public async UniTask<T> LoadAsset<T>(string path, CancellationToken cancellationToken = default) where T : Object
        {
            ResourceRequest request = Resources.LoadAsync<T>(path);
            await request.ToUniTask(cancellationToken: cancellationToken);

            T asset = request.asset as T;
            if (!asset)
            {
                DebugLogger.LogError($"Asset at path '{path}' is not a T object.");
                return null;
            }

            return asset;
        }
    }
}