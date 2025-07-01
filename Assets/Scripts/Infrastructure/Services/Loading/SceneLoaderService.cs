using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UI.Curtain.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.Services.Loading
{
    public class SceneLoaderService : ISceneLoaderService
    {
        private const float SCENE_LOADING_PROGRESS_WEIGHT = 0.6f;
        private const int CURTAIN_CLOSE_DELAY = 300;
        
        private readonly ICurtainService _curtainService;

        public SceneLoaderService(ICurtainService curtainService) => 
            _curtainService = curtainService;

        public async UniTask LoadSceneAsync(
            SceneType sceneType,
            Func<UniTask> postLoadLogic = null,
            CancellationToken cancellationToken = default)
        {
            _curtainService.Show(text: $"Loading {sceneType}...");
            _curtainService.SetProgress(0f);
            
            string sceneName = sceneType.ToString();

            AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(sceneName);
            sceneLoad.allowSceneActivation = false;

            while (sceneLoad.progress < 0.9f)
            {
                float progress = sceneLoad.progress / 0.9f; 
                _curtainService.SetProgress(progress * SCENE_LOADING_PROGRESS_WEIGHT);
                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
            }
            
            sceneLoad.allowSceneActivation = true;
            await UniTask.WaitUntil(() => sceneLoad.isDone, cancellationToken: cancellationToken);
            
            if (postLoadLogic != null)
            {
                _curtainService.SetProgress(SCENE_LOADING_PROGRESS_WEIGHT + 0.1f); 
                await postLoadLogic.Invoke();
                _curtainService.SetProgress(1f); 
            }

            await UniTask.Delay(CURTAIN_CLOSE_DELAY, cancellationToken: cancellationToken);
            _curtainService.Hide();
        }
    }
}