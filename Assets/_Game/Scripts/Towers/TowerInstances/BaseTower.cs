using System.Collections.Generic;

namespace _Game.Towers
{
    public class BaseTower : AbstractTower
    {
        private BaseTowerData currentData;
        private BaseTowerData CurrentData => currentData != null ? currentData : currentData = (BaseTowerData) currentAbstractData;

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

    }
}