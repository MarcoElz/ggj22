using _Game.Creatures;
using _Game.Utils;
using Ignita.Utils.Extensions;
using Ignita.Utils.ObjectPool;
using UnityEngine;

namespace _Game.Towers
{
    public class ShooterTower : AbstractTower
    {
        [SerializeField] private Transform head = default;
        [SerializeField] private Transform bulletOrigin = default;
        
        private ShooterData currentData;
        private ShooterData CurrentData => currentData != null ? currentData : currentData = (ShooterData) currentAbstractData;

        private float timeBetweenAttacks;
        private float timeOfLastAttack;
        
        private void Awake()
        {
            RefreshStats();
        }

        protected override void Update()
        {
            if(!IsOn) return;
            
            base.Update();

            if (Time.time > timeOfLastAttack + timeBetweenAttacks)
            {
                Shoot();
                timeOfLastAttack = Time.time;
            }
        }
        
        protected override void OnUpgraded()
        {
            base.OnUpgraded();
            RefreshStats();
        }

        public void Shoot()
        {
            var target = CreaturesManager.Instance.Elements.GetClosestElementInRange(transform.position, CurrentData.Range);
            if(target == null)
                return;

            head.LookAt(target.transform);
            
            var bullet = PoolManager.Spawn(CurrentData.BulletPrefab, bulletOrigin.position, bulletOrigin.rotation);
            bullet.Init(target, CurrentData.Damage);
        }

        private void RefreshStats()
        {
            timeBetweenAttacks = 1f / CurrentData.AttackRate;
        }


        private void OnDrawGizmosSelected()
        {
            EasyGizmos.DrawWireDisc(transform.position, Vector3.up, CurrentData.Range, Color.red);
        }
        
    }
}