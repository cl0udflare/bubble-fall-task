using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UI.Curtain.Services
{
    public class CurtainService : MonoBehaviour, ICurtainService
    {
        [SerializeField] private CurtainView _view;
        
        public async UniTask InitializeAsync(CancellationToken cancellationToken = default)
        {
            Show();
            await UniTask.CompletedTask;
        }

        public void Show(string text = "Loading...")
        {
            _view?.SetText(text);
            _view?.SetProgress(0);
            _view?.SetVisible(true);
        }

        public void Hide() => 
            _view?.SetVisible(false);

        public void SetProgress(float value) => 
            _view?.SetProgress(value);

        public void SetText(string text) => 
            _view?.SetText(text);
    }
}