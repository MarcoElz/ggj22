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

        private void Update()
        {
            var targets = TowersManager.Instance.Elements.GetAllElementInRange(transform.position, attackRange);
            var damageThisFrame = damageRate * Time.deltaTime;
            foreach (var target in targets)
            {
                target.Hurt(damageThisFrame);
            }
        }

        private void OnDrawGizmos()
        {
            EasyGizmos.DrawWireDisc(transform.position, Vector3.up, attackRange, Color.red);
        }
    }
}