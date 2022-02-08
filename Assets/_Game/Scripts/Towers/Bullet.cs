using System;
using _Game.Creatures;
using _Game.DamageSystem;
using Ignita.Utils.ObjectPool;
using UnityEngine;

namespace _Game.Towers
{
    //Optimized bullet that does not use any physics.
    //TODO: Consider using physics if required
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
            if(target != null)
                MoveToTarget();
            else
                MoveForward();
        }

        private void MoveToTarget()
        {
            currentTime += timeRatioToArrive * Time.deltaTime;
            var targetPosition = target.transform.position;
            targetPosition.y = transform.position.y;
            transform.position = Vector3.Lerp(startPosition, targetPosition, currentTime);
            CheckIfNearToDamage();
        }

        private void MoveForward()
        {
            //TODO:Continue moving if the target is gone
            Dead();
        }

        private void CheckIfNearToDamage()
        {
            //TODO: Optimize check to only use X and Z
            var targetPosition = target.transform.position;
            targetPosition.y = transform.position.y;
            var sqrDistance = Vector3.SqrMagnitude(targetPosition - transform.position);
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