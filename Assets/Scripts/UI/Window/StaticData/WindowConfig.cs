using System.Collections.Generic;
using UnityEngine;

namespace UI.Window.StaticData
{
    [CreateAssetMenu(fileName = "WindowConfig", menuName = "Bubble Fall/Window")]
    public class WindowConfig : ScriptableObject
    {
        [SerializeField] private List<WindowConfigData> _windows;

        public List<WindowConfigData> Windows => _windows;
    }
}