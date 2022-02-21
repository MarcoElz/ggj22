using System;
using System.Collections.Generic;
using System.Linq;
using _Game.GameResources;
using _Game.ShopSystem;
using Ignita.Utils.Common;
using Unity.Mathematics;
using UnityEngine;

namespace _Game.InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private ResourceAmount[] startResources = default;
        [SerializeField] private ResourceAmount[] initialCapacity = default;
        
        public event Action<ResourceContainer> onResourceUpdated;

        private Dictionary<Resource, ResourceContainer> currentResources;

        private void Awake()
        {
            //Create Dictionary
            currentResources = startResources.ToDictionary(
                (r) => r.resource,
                (r) => new ResourceContainer(r.resource, 0));
        }

        private void Start()
        {
            //Set Initial Capacity
            foreach (var resourceAmount in initialCapacity)
                UpgradeCapacity(resourceAmount.resource, resourceAmount.amount);

            //Add Initial resources
            foreach (var resourceData in startResources) 
                Add(resourceData.resource, resourceData.amount);
        }
        
        public void UpgradeCapacity(Resource resource, float amount)
        {
            var container = currentResources[resource];
            container.UpgradeCapacity(amount);
            onResourceUpdated?.Invoke(container);
        }

        public float GetCurrentAmount(Resource resource) => currentResources[resource].Amount;

        public void Add(Resource resource, float amount)
        {
            var container = currentResources[resource];
            container.Add(amount);
            onResourceUpdated?.Invoke(container);
        }

        public void Consume(Resource resource, float amount)
        {
            var container = currentResources[resource];
            var amountToRemove = Mathf.Abs(amount);
            container.Remove(amountToRemove);
            onResourceUpdated?.Invoke(container);
        }

        public bool TryProcessTransaction(Transaction transaction)
        {
            if (!CanProcessTransaction(transaction))
                return false;
            
            ProcessTransaction(transaction);
            return true;
        }
        
        public bool CanProcessTransaction(Transaction transaction)
        {
            if (transaction.resourceCosts == null)
                return true;
            
            for (int i = 0; i < transaction.resourceCosts.Length; i++)
            {
                var resourceAmount = transaction.resourceCosts[i];
                if(resourceAmount.amount >= 0f) //Free or positive
                    continue;
                
                //TODO: Mmm.. the problem of using floats. For some reason, it is removing a few decimals
                // I have to round it, and use -Matfh.Epsilon to make it work correctly
                var currentResource = currentResources[resourceAmount.resource];
                var currentAmount = Mathf.Round(currentResource.Amount);
                
                var resourceAfterTransaction = currentAmount + resourceAmount.amount;

                if (resourceAfterTransaction < -Mathf.Epsilon) //Negative inventory, can not proceed
                    return false;
            }

            return true;
        }

        private void ProcessTransaction(Transaction transaction)
        {
            for (int i = 0; i < transaction.resourceCosts.Length; i++)
            {
                var resourceAmount = transaction.resourceCosts[i];
                if (CompareUtil.IsEqual(resourceAmount.amount, 0f)) //If zero, skip
                    continue;

                if (resourceAmount.amount > 0f) //Positive. Add it
                    Add(resourceAmount.resource, resourceAmount.amount);
                else //Negative, consume it
                {
                    Consume(resourceAmount.resource, resourceAmount.amount);
                }
            }
        }
        
    }
}