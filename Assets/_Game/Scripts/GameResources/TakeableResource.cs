using System;
using _Game.InputHelper;
using Ignita.Utils.ObjectPool;
using UnityEngine;

namespace _Game.GameResources
{
    public class TakeableResource : MonoBehaviour, IPoolObject
    {
        [SerializeField] private Resource resource = default;
        [SerializeField] private float distanceToTake = 0.5f;
        
        private float currentValue;
        private float sqrDistanceToTake => distanceToTake * distanceToTake;
        
        public void Init(float value)
        {
            currentValue = value;
        }

        private void Take()
        {
            Global.Inventory.Add(resource, currentValue);
            PoolManager.ReturnToPool(this);
        }

        private void Update()
        {
            var position = MouseHelper.Instance.WorldPoint;
            var sqrDistance = Vector3.Magnitude(position - transform.position);
            if (sqrDistance < distanceToTake) 
                Take();
        }

        public string PoolId { get; set; }
        public void OnSpawnFromPool()
        { }

        public void OnReturnToPool()
        { }
    }
}