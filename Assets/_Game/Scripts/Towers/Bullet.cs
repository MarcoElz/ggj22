using System;
using _Game.Creatures;
using _Game.DamageSystem;
using Ignita.Utils.ObjectPool;
using UnityEngine;

namespace _Game.Towers
{
    public class Bullet : MonoBehaviour, IPoolObject
    {
        [SerializeField] private float speed = 10f;

        private float distanceToDamage = 0.5f;
        private float sqrDistanceToDamage => distanceToDamage * distanceToDamage;
        private int damagePower;

        private Vector3 startPosition;
        private float startDistance;
        private float timeToArrive;
        private float timeRatioToArrive;
        private float currentTime;
        
        private AbstractCreature target;
        
        public void Init(AbstractCreature newTarget, int damage)
        {
            target = newTarget;
            damagePower = damage;
            startPosition = transform.position;
            startDistance = Vector3.Distance(target.transform.position, startPosition);
            timeToArrive = startDistance / speed;
            timeRatioToArrive = 1f / timeToArrive;
            currentTime = 0f;
        }

        private void Update()
        {
            MoveToTarget();
        }

        private void MoveToTarget()
        {
            currentTime += timeRatioToArrive * Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, target.transform.position, currentTime);
            CheckIfNearToDamage();
        }

        private void CheckIfNearToDamage()
        {
            var sqrDistance = Vector3.SqrMagnitude(target.transform.position - transform.position);
            if(sqrDistance < sqrDistanceToDamage)
                Damage(target);
        }

        private void Damage(IDamageable damageable)
        {
            damageable.Hurt(damagePower);
            Dead();
        }

        private void Dead()
        {
            //TODO: VFX
            PoolManager.ReturnToPool(this);
        }

        public string PoolId { get; set; }

        public void OnSpawnFromPool()
        {
        }

        public void OnReturnToPool()
        {
        }
    }
}