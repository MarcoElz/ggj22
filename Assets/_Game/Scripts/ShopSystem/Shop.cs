using System;
using _Game.InventorySystem;
using _Game.Towers;
using UnityEngine;

namespace _Game.ShopSystem
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private Inventory inventory = default;

        public event Action onTransactionCompleted;
        public event Action onTransactionFailed;


        public bool CanBuyTower(TowerGeneralData tower)
        {
            var cost = tower.InitialCost;
            return inventory.CanConsumeCost(cost);
        }

        public bool TryBuyTower(TowerGeneralData tower)
        {
            var cost = tower.InitialCost;
            var completed = inventory.TryConsumeResourceCost(cost);

            if (completed)
            {
                //Do thing
            }


            TransactionFeedback(completed);
            return completed;
        }

        public bool CanUpgradeTower(AbstractTower tower)
        {
            if (!tower.HasNextUpgrade) return false;
            var cost = tower.Data.UpgradesData[tower.UpgradeLevel].Cost;
            return inventory.CanConsumeCost(cost);
        }

        public bool TryUpgradeTower(AbstractTower tower)
        {
            if (!tower.HasNextUpgrade) return false;
            var cost = tower.Data.UpgradesData[tower.UpgradeLevel].Cost;
            var completed = inventory.TryConsumeResourceCost(cost);

            if (completed) 
                tower.Upgrade();

            TransactionFeedback(completed);
            return completed;
        }

        public bool CanRepairTower(AbstractTower tower)
        {
            return true;
        }

        public bool TryRepairTower(AbstractTower tower)
        {
            bool completed = true;
            
            TransactionFeedback(completed);
            return completed;
        }


        private void TransactionFeedback(bool completed)
        {
            if(completed)
                onTransactionCompleted?.Invoke();
            else
                onTransactionFailed?.Invoke();
        }
        
    }
}