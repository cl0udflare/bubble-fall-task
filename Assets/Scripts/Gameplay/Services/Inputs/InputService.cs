using System;
using UnityEngine;
using Zenject;

namespace Gameplay.Services.Inputs
{
    public class InputService : IInputService, ITickable
    {
        public event Action<Vector2> OnPointerDown;
        public event Action<Vector2> OnPointerDrag;
        public event Action<Vector2> OnPointerUp;

        private bool _isDragging;

        public void Tick() => HandleMouse();

        private void HandleMouse()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isDragging = true;
                OnPointerDown?.Invoke(Input.mousePosition);
            }
            else if (Input.GetMouseButton(0) && _isDragging)
            {
                OnPointerDrag?.Invoke(Input.mousePosition);
            }
            else if (Input.GetMouseButtonUp(0) && _isDragging)
            {
                _isDragging = false;
                OnPointerUp?.Invoke(Input.mousePosition);
            }
        }
    }
}