using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Infrastructure.Services.Loading
{
    public interface ISceneLoaderService
    {
        UniTask LoadSceneAsync(SceneType sceneType, Action<float> onProgress, CancellationToken token = default);
    }
}