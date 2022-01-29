using System;
using _Game.ShopSystem;
using _Game.Towers;
using UnityEngine;

namespace _Game.DebugMode
{
    public class DebugShop : MonoBehaviour
    {
        [SerializeField] private TowerBuilder builder = default;
        [SerializeField] private BuyTowerKey[] towers = default;
        
        private void Update()
        {
            foreach (var buyTowerKey in towers)
            {
                if (Input.GetKeyDown(buyTowerKey.keyCode)) 
                    builder.TryBuildMode(buyTowerKey.towerData);
            }
        }
    }

    [Serializable]
    public struct BuyTowerKey
    {
        public KeyCode keyCode;
        public TowerGeneralData towerData;
    }
}