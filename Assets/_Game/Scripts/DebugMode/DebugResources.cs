using System;
using _Game.GameResources;
using _Game.InventorySystem;
using UnityEngine;

namespace _Game.DebugMode
{
    public class DebugResources : MonoBehaviour
    {
        [SerializeField] private Inventory inventory = default;

        private void OnEnable() => inventory.onResourceUpdated += LogResourceUpdate;
        private void OnDisable() => inventory.onResourceUpdated -= LogResourceUpdate;

        private void LogResourceUpdate(ResourceContainer resourceContainer)
        {
            Debug.Log($"{resourceContainer.Resource.name}: {resourceContainer.Amount}");
        }
    }
}