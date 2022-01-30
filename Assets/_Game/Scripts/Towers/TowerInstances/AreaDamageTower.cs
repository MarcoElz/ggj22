using System;
using _Game.Creatures;
using _Game.Utils;
using Ignita.Utils.Extensions;
using UnityEngine;

namespace _Game.Towers
{
    public class AreaDamageTower : AbstractTower
    {
        private AreaDamageData currentData;
        //private AreaDamageData CurrentData => currentData != null ? currentData : currentData = (AreaDamageData) CurrentAbstractData;
        private AreaDamageData CurrentData => (AreaDamageData) CurrentAbstractData;

        private float timeBetweenAttacks;
        private float timeOfLastAttack;

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
                Attack();
                timeOfLastAttack = Time.time;
            }
        }

        public void Attack()
        {
            var targets = CreaturesManager.Instance.Elements.GetAllElementInRange(transform.position, CurrentData.Range);
            if(targets.Count == 0) return;

            foreach (var target in targets)
            {
                target.Hurt(CurrentData.Damage);
            }
        }

        protected override void OnUpgraded()
        {
            base.OnUpgraded();
            RefreshStats();
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