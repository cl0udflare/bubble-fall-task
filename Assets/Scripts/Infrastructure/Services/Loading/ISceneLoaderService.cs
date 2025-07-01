using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Infrastructure.Services.Loading
{
    public interface ISceneLoaderService
    {
        UniTask LoadSceneAsync(SceneType sceneType, Func<UniTask> postLoadLogic = null, CancellationToken cancellationToken = default);
    }
}