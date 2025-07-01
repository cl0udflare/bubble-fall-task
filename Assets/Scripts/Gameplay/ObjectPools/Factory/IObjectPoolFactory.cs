using UnityEngine;

namespace Gameplay.ObjectPools.Factory
{
    public interface IObjectPoolFactory
    {
        IObjectPool<T> GetOrCreatePool<T>(T prefab, Transform parent = null) where T : Component;
        void Clear<T>() where T : Component;
        void ClearAll();
    }
}