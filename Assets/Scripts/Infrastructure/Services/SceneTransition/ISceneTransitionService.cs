using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Infrastructure.Services.Loading;

namespace Infrastructure.Services.SceneTransition
{
    public interface ISceneTransitionService
    {
        UniTask TransitionAsync(SceneType sceneType, Func<UniTask> onStateInit, CancellationToken token = default);
        void ReportInitializationProgress(float normalizedValue);
    }
}