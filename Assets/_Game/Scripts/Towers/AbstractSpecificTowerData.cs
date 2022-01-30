using _Game.ShopSystem;
using UnityEngine;

namespace _Game.Towers
{
    public abstract class AbstractSpecificTowerData : ScriptableObject
    {
        [Header("General Settings")]
        [SerializeField] private Transaction transaction = default;
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private GameObject newPrefab = default;
        
        public Transaction Transaction => transaction;

        public int MaxHealth => maxHealth;

        public GameObject NewPrefab => newPrefab;
        public virtual float Range => 0f;
    }
}