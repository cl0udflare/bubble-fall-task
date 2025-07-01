using System.Collections.Generic;
using UnityEngine;

namespace UI.Window.StaticData
{
    [CreateAssetMenu(fileName = "WindowConfig", menuName = "Fear Of Height/Window")]
    public class WindowConfig : ScriptableObject
    {
        [SerializeField] private List<WindowStaticData> _windows;

        public List<WindowStaticData> Windows => _windows;
    }
}