using UI.Curtain;
using UnityEngine;

namespace UI.Services
{
    public class CurtainService : MonoBehaviour, ICurtainService
    {
        [SerializeField] private CurtainView _view;

        private float _from;
        private float _to;

        public void Show(string text = "Loading...")
        {
            SetProgress01(0);
            _view?.SetText(text);
            _view?.SetVisible(true);
        }

        public void Hide() => 
            _view?.SetVisible(false);

        public void SetProgress01(float value) => 
            _view?.SetProgress(Mathf.Clamp01(value));

        public void SetText(string text) => 
            _view?.SetText(text);
    }
}