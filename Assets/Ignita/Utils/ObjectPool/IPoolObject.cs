using CommonUtils.UnityComponents;

namespace Ignita.Utils.ObjectPool
{
    public interface IPoolObject : IUnityComponent
    {
        string PoolId { get; set; }

        void OnSpawnFromPool();
        void OnReturnToPool();
    }
}