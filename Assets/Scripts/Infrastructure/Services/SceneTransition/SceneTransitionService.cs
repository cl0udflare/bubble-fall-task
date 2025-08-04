using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Infrastructure.Services.Loading;
using UI.Services;
using Zenject;

namespace Infrastructure.Services.SceneTransition
{
    public class SceneTransitionService : ISceneTransitionService
    {
        private const float MAX_SCENE_PROGRESS = 0.9f;
        
        private readonly ICurtainService _curtain;
        private readonly ISceneLoaderService _sceneLoader;
        private readonly DiContainer _container;

        private float _currentProgress;

        public SceneTransitionService(ICurtainService curtain, ISceneLoaderService sceneLoader, DiContainer container)
        {
            _curtain = curtain;
            _sceneLoader = sceneLoader;
            _container = container;
        }
        
        public async UniTask TransitionAsync(SceneType sceneType, Func<UniTask> onStateInit, CancellationToken token = default)
        {
            _curtain.Show();
            
            // Load scene
            await _sceneLoader.LoadSceneAsync(sceneType, sceneProgress =>
            {
                float combined = _currentProgress + (MAX_SCENE_PROGRESS - _currentProgress) * sceneProgress;
                _curtain.SetProgress01(combined);
            }, token);
            
            // Init scene context
            SceneContext sceneContext = _container.Resolve<SceneContextRegistry>().SceneContexts.First();
            await UniTask.WaitUntil(() => sceneContext.HasInstalled, cancellationToken: token);

            // Init action
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