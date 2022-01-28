using _Game.ShopSystem;
using UnityEngine;

namespace _Game.Towers
{
    public abstract class AbstractSpecificTowerData : ScriptableObject
    {
        [SerializeField] private Cost cost = default;

        public Cost Cost => cost;
    }
}