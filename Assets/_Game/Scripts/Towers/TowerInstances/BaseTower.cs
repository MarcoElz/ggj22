using System.Collections.Generic;
using UnityEngine;

namespace _Game.Towers
{
    public class BaseTower : AbstractTower
    {
        private BaseTowerData currentData;
        //private BaseTowerData CurrentData => currentData != null ? currentData : currentData = (BaseTowerData) CurrentAbstractData;
        private BaseTowerData CurrentData => (BaseTowerData) CurrentAbstractData;

        public List<TowerGeneralData> GetAlUnlockedTowers()
        {
            var towers = new List<TowerGeneralData>();

            var allUpgradesData = Data.UpgradesData;

            for (int i = 0; i < allUpgradesData.Length; i++)
            {
                if(i > UpgradeLevel) break;
                var baseTowerData = (BaseTowerData)allUpgradesData[i];
                towers.AddRange(baseTowerData.UnlockedTowers);
                
                if(Global.IsSpecialTowersUnlocked)
                    towers.AddRange(baseTowerData.SpecialTowers);
            }

            return towers;
        }

        protected override void OnUpgraded()
        {
            Debug.Log($"Upgraded start {UpgradeLevel}");
            base.OnUpgraded();

            var upgrades = CurrentData.UpgradedCapacity;
            foreach (var upgrade in upgrades) 
                Global.Inventory.UpgradeCapacity(upgrade.resource, upgrade.amount);

            Debug.Log($"Upgraded end {UpgradeLevel}");
        }

        protected override void Dead()
        {
            base.Dead();
            Global.Instance.GameOver();
        }
    }
}