using System;
using UnityEngine;

namespace Gameplay.Services.Inputs
{
    public interface IInputService
    {
        event Action<Vector2> OnPointerDown;
        event Action<Vector2> OnPointerDrag;
        event Action<Vector2> OnPointerUp;
    }
}