using UI.Window.Factory;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Window
{
    public class OpenWindowButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private WindowType _type;
        
        private IWindowFactory _windowFactory;

        [Inject]
        private void Construct(IWindowFactory windowFactory)
        {
            _windowFactory = windowFactory;
        }

        private void OnValidate() => 
            _button = GetComponent<Button>();

        private void OnDestroy() => 
            _button.onClick.RemoveAllListeners();
    }
}