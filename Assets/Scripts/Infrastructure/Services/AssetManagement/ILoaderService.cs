using System.Threading;
using Cysharp.Threading.Tasks;
using Infrastructure.Bootstrapper;
using UnityEngine;

namespace Infrastructure.Services.AssetManagement
{
    public interface ILoaderService : IInitializableAsync
    {
        UniTask<T> LoadAssetAsync<T>(string path, CancellationToken token = default) where T : Object;
    }
}