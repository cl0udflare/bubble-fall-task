using UnityEngine;
using UnityEngine.UI;

namespace UI.Window
{
    public class WindowBase : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;

        private void Awake() => 
            OnAwake();

        private void Start()
        {
            Initialize();
            SubscribeUpdates();
        }

        private void OnDestroy()
        {
            Cleanup();
            _closeButton?.onClick.RemoveAllListeners();
        }

        protected virtual void OnAwake() => 
            _closeButton?.onClick.AddListener(call: () => Destroy(gameObject));

        protected virtual void Initialize(){}
        protected virtual void SubscribeUpdates(){}
        protected virtual void Cleanup(){}
    }
}