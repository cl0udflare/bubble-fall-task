using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Infrastructure.Services.Loading;
using UI.Services;

namespace Infrastructure.Services.SceneTransition
{
    public class SceneTransitionService : ISceneTransitionService
    {
        private const float MAX_SCENE_PROGRESS = 0.9f;
        
        private readonly ICurtainService _curtain;
        private readonly ISceneLoaderService _sceneLoader;

        private float _currentProgress;

        public SceneTransitionService(ICurtainService curtain, ISceneLoaderService sceneLoader)
        {
            _curtain = curtain;
            _sceneLoader = sceneLoader;
        }
        
        public async UniTask TransitionAsync(SceneType sceneType, Func<UniTask> onStateInit, CancellationToken token = default)
        {
            _curtain.Show();
            
            await _sceneLoader.LoadSceneAsync(sceneType, sceneProgress =>
            {
                float combined = _currentProgress + (MAX_SCENE_PROGRESS - _currentProgress) * sceneProgress;
                _curtain.SetProgress01(combined);
            }, token);
            
            await UniTask.Delay(500, cancellationToken: token);

            if (onStateInit != null)
                await onStateInit.Invoke().AttachExternalCancellation(token);

            _currentProgress = 0;
            _curtain.SetProgress01(1f);
            _curtain.Hide();
        }

        public void ReportInitializationProgress(float normalizedValue)
        {
            _currentProgress = Math.Clamp(normalizedValue, 0f, 1f);
            _curtain.Show();
            _curtain.SetProgress01(_currentProgress);
        }
    }
}