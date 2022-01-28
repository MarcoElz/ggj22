using System;
using _Game.DamageSystem;
using Ignita.Utils.ObjectPool;
using UnityEngine;

namespace _Game.Creatures
{
    public class AbstractCreature : MonoBehaviour, IDamageable, IPoolObject
    {
        [SerializeField] private int startHealth = 100;
        
        private int health;

        private void Awake()
        {
            Initialize();
        }

        private void OnEnable()
        {
            CreaturesManager.Instance.Add(this);
        }

        private void OnDisable()
        {
            CreaturesManager.Instance.Remove(this);
        }

        public void Hurt(int amount)
        {
            health -= amount;
            if (health <= 0) 
                Dead();
        }

        private void Dead()
        {
            Destroy(gameObject);
        }

        private void Initialize()
        {
            health = startHealth;
        }

        public string PoolId { get; set; }
        public void OnSpawnFromPool()
        {
            Initialize();
        }

        public void OnReturnToPool()
        {
        }
    }
}