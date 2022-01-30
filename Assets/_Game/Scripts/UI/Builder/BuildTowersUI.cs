using System.Collections.Generic;
using _Game.Towers;
using _Game.UI.Utils;
using UnityEngine;

namespace _Game.UI.Builder
{
    public class BuildTowersUI : FaderUI
    {
        [SerializeField] private Transform parent = default;
        [SerializeField] private BuildTowerButton prefab = default;
        
        protected override void OnShow()
        {
            base.OnShow();
            var towers = Global.MainTower.GetAlUnlockedTowers();
            RefreshUI(towers.ToArray());
        }

        private void RefreshUI(TowerGeneralData[] towers)
        {
            Clean();
            for (int i = 0; i < towers.Length; i++)
            {
                var tower = towers[i];
                var towerButton = Instantiate(prefab, parent);
                towerButton.Init(tower);
            }
        }

        private void Clean()
        {
            for (int i = 0; i < parent.childCount; i++) 
                Destroy(parent.GetChild(i).gameObject);
        }
    }
}