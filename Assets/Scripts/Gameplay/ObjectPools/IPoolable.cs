namespace Gameplay.ObjectPools
{
    public interface IPoolable
    {
        void OnSpawned();
        void OnDespawned();
    }
}