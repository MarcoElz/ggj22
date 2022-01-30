using Ignita.Utils.ObjectPool;
using UnityEngine;

namespace _Game
{
    public class TimeParticles : MonoBehaviour, IPoolObject
    {
        [SerializeField] private float timeToDestroy = 2f;

        public string PoolId { get; set; }

        public void OnSpawnFromPool()
        {
            PoolManager.Instance.ReturnToPoolDelayed(timeToDestroy, this);
        }

        public void OnReturnToPool()
        {
        }
    }
}