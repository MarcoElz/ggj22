using System;
using System.Collections.Generic;
using System.Linq;
using _Game.GameResources;
using _Game.ShopSystem;
using UnityEngine;

namespace _Game.InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private ResourceAmount[] startResources = default;

        public event Action<ResourceContainer> onResourceUpdated;

        private Dictionary<Resource, ResourceContainer> currentResources;

        private void Awake()
        {
            currentResources = startResources.ToDictionary(
                (r) => r.resource,
                (r) => new ResourceContainer(r.resource, 0));

            foreach (var resourceData in startResources) 
                Add(resourceData.resource, resourceData.amount);
        }

        public void Add(Resource resource, float amount)
        {
            var container = currentResources[resource];
            container.Add(amount);
            onResourceUpdated?.Invoke(container);
        }

        public bool TryConsume(Resource resource, float requiredAmount)
        {
            var container = currentResources[resource];
            if (container.Amount < requiredAmount)
                return false;
            
            Consume(container, requiredAmount);
            return true;
        }

        public bool TryConsumeResourceCost(Cost cost)
        {
            if (!CanConsumeCost(cost))
                return false;
            
            ConsumeCost(cost);
            return true;
        }

        public bool CanConsumeCost(Cost cost)
        {
            for (int i = 0; i < cost.resourceCosts.Length; i++)
            {
                var resourceCost = cost.resourceCosts[i];
                var currentResource = currentResources[resourceCost.resource];
                if (currentResource.Amount < resourceCost.amount)
                    return false;
            }

            return true;
        }

        private void ConsumeCost(Cost cost)
        {
            for (int i = 0; i < cost.resourceCosts.Length; i++)
            {
                var resourceCost = cost.resourceCosts[i];
                var valid = TryConsume(resourceCost.resource, resourceCost.amount);
                if(!valid)
                    Debug.LogError("Something went wrong consuming cost");
            }
        }

        private void Consume(Resource resource, float amount)
        {
            var container = currentResources[resource];
            Consume(container, amount);
        }

        private void Consume(ResourceContainer container, float amount)
        {
            container.Remove(amount);
            onResourceUpdated?.Invoke(container);
        }
    }
}