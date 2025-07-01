using UnityEngine;

namespace Gameplay.ObjectPools
{
    public interface IObjectPool<T> where T : Component
    {
        T Get();
        void Return(T item);
        void Preload(int count);
    }
}