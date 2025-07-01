using System;
using UnityEngine;

namespace UI.Window.StaticData
{
    [Serializable]
    public class WindowConfigData
    {
        [SerializeField] private WindowType _type;
        [SerializeField] private WindowBase _prefab;

        public WindowType Type => _type;
        public WindowBase Prefab => _prefab;
    }
}