using System;
using _Game.Towers;
using _Game.Utils;
using Ignita.Utils.Extensions;
using UnityEngine;

namespace _Game.Creatures
{
    public class EnemyCreature : AbstractCreature
    {
        [SerializeField] private float attackRange = 2f;
        [SerializeField] private float damageRate = 1f;
        [SerializeField] private float speed = 3f;

        private void Update()
        {
            Move();
            DamageNearTowers();
        }

        private void Move()
        {
            var target = TowersManager.Instance.GetClosestValidElement(transform.position);
            if(target == null)
                return;

            var sqrDistance = Vector3.SqrMagnitude(target.transform.position - transform.position);
            if (sqrDistance < attackRange * attackRange)
            {
                //Stop moving, you are near enough to attack
                return;
            }

            transform.LookAt(target.transform);
            transform.position += transform.forward * speed * Time.deltaTime;
        }

        private void DamageNearTowers()
        {
            var targets = TowersManager.Instance.Elements.GetAllElementInRange(transform.position, attackRange);
            var damageThisFrame = damageRate * Time.deltaTime;
            for (int i = 0; i < targets.Count; i++)
            {
                var target = targets[i];
                target.Hurt(damageThisFrame);
            }
        }

        private void OnDrawGizmos()
        {
            EasyGizmos.DrawWireDisc(transform.position, Vector3.up, attackRange, Color.red);
        }
    }
}