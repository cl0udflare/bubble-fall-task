using System.Threading;
using Cysharp.Threading.Tasks;
using Logging;
using UnityEngine;

namespace Infrastructure.Services.AssetManagement
{
    public class LoaderService : ILoaderService
    {
        public UniTask InitializeAsync(CancellationToken token = default)
        {
            return UniTask.CompletedTask;
        }

        public async UniTask<T> LoadAssetAsync<T>(string path, CancellationToken token = default) where T : Object
        {
            ResourceRequest request = Resources.LoadAsync<T>(path);
            await request.ToUniTask(cancellationToken: token);

            T asset = request.asset as T;
            if (!asset)
            {
                DebugLogger.LogError($"Asset at path '{path}' is not a {typeof(T)} object.");
                return null;
            }

            return asset;
        }
    }
}