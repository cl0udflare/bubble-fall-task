using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UI.Windows.Factory
{
    public interface IWindowFactory
    {
        UniTask<WindowBase> CreateWindowByType(WindowType type);
        void SetRoot(Transform root);
    }
}