using System;
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
        [SerializeField] private BaseTower mainBase = default;

        private void OnEnable() => mainBase.onUpgraded += ForceRefresh;

        private void OnDisable() => mainBase.onUpgraded -= ForceRefresh;

        protected override void OnShow()
        {
            base.OnShow();
            ForceRefresh();
        }

        private void ForceRefresh()
        {
            var towers = mainBase.GetAlUnlockedTowers();
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