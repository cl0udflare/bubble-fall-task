using System;
using UnityEngine;

namespace Gameplay.Core
{
    [RequireComponent(typeof(Collider))]
    public class TriggerObserver : MonoBehaviour
    {
        public event Action<Collider> TriggerEntered;
        
        private void OnTriggerEnter(Collider other) => 
            TriggerEntered?.Invoke(other);
    }
}