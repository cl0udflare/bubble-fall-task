using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.Services.Loading
{
    public class SceneLoaderService : ISceneLoaderService
    {
        public async UniTask LoadSceneAsync(SceneType sceneType, Action<float> onProgress, CancellationToken token = default)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneType.ToString());
            operation.allowSceneActivation = true;

            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);
                onProgress?.Invoke(progress);
                await UniTask.Yield(PlayerLoopTiming.Update, token);
            }

            onProgress?.Invoke(1f);
        }
    }
}