using System.Threading;
using Cysharp.Threading.Tasks;
using Infrastructure.Bootstrapper;
using UnityEngine;

namespace Infrastructure.Services.AssetManagement
{
    public interface ILoaderService : IInitializableAsync
    {
        UniTask<T> LoadAsset<T>(string path, CancellationToken cancellationToken = default) where T : Object;
    }
}