using System;
using _Game.DamageSystem;
using Ignita.Utils.ObjectPool;
using UnityEngine;

namespace _Game.Creatures
{
    public class AbstractCreature : MonoBehaviour, IDamageable, IPoolObject
    {
        [SerializeField] protected float startHealth = 100;
        
        public float Health { get; protected set; }

        protected virtual void Awake()
        {
            Initialize();
        }

        protected virtual void OnEnable()
        {
            CreaturesManager.Instance.Add(this);
        }

        protected virtual void OnDisable()
        {
            CreaturesManager.Instance.Remove(this);
        }

        public void Hurt(float amount)
        {
            Health -= amount;
            if (Health <= 0) 
                Dead();
        }

        protected virtual void Dead()
        {
            Global.Dropper.AlertToDrop(transform.position);
            PoolManager.ReturnToPool(this);
        }

        protected virtual void Initialize()
        {
            Health = startHealth;
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