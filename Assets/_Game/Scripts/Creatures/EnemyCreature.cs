using _Game.DamageSystem;
using _Game.Towers;
using _Game.Utils;
using UnityEngine;

namespace _Game.Creatures
{
    public class EnemyCreature : AbstractCreature
    {
        [SerializeField] private float attackRange = 2f;
        [SerializeField] private float damageRate = 1f;
        [SerializeField] private float speed = 3f;

        private AbstractTower currentTarget;
        private bool isAttacking;

        private void Update()
        {
            var hasValidTarget = CheckTarget();
            if (!hasValidTarget) return;
            
            Move();
            TryAttackCurrentTower();
        }

        private bool CheckTarget()
        {
            var hasValidTarget = true;
            var shouldFindNewTarget = false;

            if (currentTarget == null)
                shouldFindNewTarget = true;
            else if (!currentTarget.IsAlive)
                shouldFindNewTarget = true;

            if (shouldFindNewTarget)
                hasValidTarget = FindNewTarget();

            return hasValidTarget;
        }

        private bool FindNewTarget()
        {
            var target = TowersManager.Instance.GetClosestValidElement(transform.position);
            if(target == null)
                return false;

            currentTarget = target;
            return true;
        }

        private void Move()
        {
            var sqrDistance = Vector3.SqrMagnitude(currentTarget.transform.position - transform.position);
            if (sqrDistance < attackRange * attackRange)
            {
                //Stop moving, you are near enough to attack
                isAttacking = true;
                return;
            }

            isAttacking = false;
            transform.LookAt(currentTarget.transform);
            transform.position += transform.forward * speed * Time.deltaTime;
        }

        private void TryAttackCurrentTower()
        {
            if(!isAttacking)
                return;
            
            var damageThisFrame = damageRate * Time.deltaTime;
            Attack(currentTarget, damageThisFrame);
        }

        private void Attack(IDamageable target, float damageThisFrame)
        {
            damageThisFrame += (Global.Difficult * 0.2f);
            target.Hurt(damageThisFrame);
        }

        private void OnDrawGizmos()
        {
            EasyGizmos.DrawWireDisc(transform.position, Vector3.up, attackRange, Color.red);
        }
    }
}