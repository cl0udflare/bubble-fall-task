using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UI.Window.Factory
{
    public interface IWindowFactory
    {
        UniTask<WindowBase> CreateWindowByType(WindowType type);
        void SetRoot(Transform root);
    }
}