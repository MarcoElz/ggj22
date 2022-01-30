using System;
using System.Collections.Generic;
using System.Linq;
using _Game.GameResources;
using UnityEngine;

namespace _Game.UI.Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private InventorySystem.Inventory inventory;
        
        private ResourceUI[] resources = default;
        private Dictionary<Resource, ResourceUI> currentResources;

        private void Awake()
        {
            resources = GetComponentsInChildren<ResourceUI>();
            currentResources = resources.ToDictionary(
                (r) => r.Resource,
                (r) => r);
        }

        private void OnEnable() => inventory.onResourceUpdated += OnResourceUpdated;
        private void OnDisable() => inventory.onResourceUpdated -= OnResourceUpdated;

        private void OnResourceUpdated(ResourceContainer container)
        {
            currentResources[container.Resource].Set(container);
        }
    }
}
