using System;
using _Game.DamageSystem;
using DG.Tweening;
using Ignita.Utils.ObjectPool;
using UnityEngine;

namespace _Game.Creatures
{
    public class AbstractCreature : MonoBehaviour, IDamageable, IPoolObject
    {
        [SerializeField] protected float startHealth = 100;
        [SerializeField] protected TimeParticles deadParticles = default;
        [SerializeField] protected TimeParticles hurtParticles = default;
            
        public float Health { get; protected set; }

        protected virtual void Start()
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
            else
                PoolManager.Spawn(hurtParticles, transform.position, Quaternion.identity);
        }

        protected virtual void Dead()
        {
            PoolManager.Spawn(deadParticles, transform.position, Quaternion.identity);
            Global.Dropper.AlertToDrop(transform.position);
            PoolManager.ReturnToPool(this);
        }

        protected virtual void Initialize()
        {
            Health = startHealth  + (45f * (Global.Difficult - 1f));
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, 0.5f);
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