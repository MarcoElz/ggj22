using _Game.Creatures;
using _Game.Utils;
using Ignita.Utils.Extensions;
using Ignita.Utils.ObjectPool;
using UnityEngine;

namespace _Game.Towers
{
    public class ShooterTower : AbstractTower
    {
        private ShooterData currentData;
        private ShooterData CurrentData => currentData != null ? currentData : currentData = (ShooterData) CurrentAbstractData;

        private float timeBetweenAttacks;
        private float timeOfLastAttack;

        private TowerHead head;

        private TowerHead Head
        {
            get
            {
                if (head == null)
                    head = GetComponentInChildren<TowerHead>();

                return head;
            }
        }
        
        protected override void Awake()
        {
            base.Awake();
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
            head = GetComponentInChildren<TowerHead>();
        }

        public void Shoot()
        {
            var target = CreaturesManager.Instance.Elements.GetClosestElementInRange(transform.position, CurrentData.Range);
            if(target == null)
                return;

            Head.transform.LookAt(target.transform);
            var euler = Head.transform.eulerAngles;
            euler.x = 0f;
            euler.z = 0f;
            Head.transform.eulerAngles = euler;
            
            var bullet = PoolManager.Spawn(CurrentData.BulletPrefab, Head.BulletOrigin.position, head.BulletOrigin.rotation);
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