using _Game.ShopSystem;
using UnityEngine;

namespace _Game.Towers
{
    public abstract class AbstractSpecificTowerData : ScriptableObject
    {
        [Header("General Settings")]
        [SerializeField] private Transaction transaction = default;
        [SerializeField] private int maxHealth = 100;

        public Transaction Transaction => transaction;

        public int MaxHealth => maxHealth;
    }
}