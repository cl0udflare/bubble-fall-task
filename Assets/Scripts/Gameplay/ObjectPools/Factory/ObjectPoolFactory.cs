using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.ObjectPools.Factory
{
    public class ObjectPoolFactory : IObjectPoolFactory
    {
        private readonly Dictionary<Type, object> _pools = new();

        public IObjectPool<T> GetOrCreatePool<T>(T prefab, Transform parent = null) where T : Component
        {
            Type type = typeof(T);

            if (_pools.TryGetValue(type, out object pool))
                return (IObjectPool<T>)pool;

            ObjectPool<T> newPool = new ObjectPool<T>(prefab, parent);
            _pools[type] = newPool;
            
            return newPool;
        }

        public void Clear<T>() where T : Component
        {
            if (_pools.TryGetValue(typeof(T), out object pool) && pool is IClearablePool clearable)
            {
                clearable.Clear();
                _pools.Remove(typeof(T));
            }
        }

        public void ClearAll()
        {
            foreach (object pool in _pools.Values)
            {
                if (pool is IClearablePool clearable)
                    clearable.Clear();
            }

            _pools.Clear();
        }
    }
}