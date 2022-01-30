using _Game.Creatures;
using Ignita.Utils.Extensions;
using UnityEngine;

namespace _Game.Towers
{
    public class RadarTower : AbstractTower
    {
        [SerializeField] private float timeBetweenChecks = 0.5f;
        
        private RadarData currentData;
        private RadarData CurrentData => currentData != null ? currentData : currentData = (RadarData) CurrentAbstractData;

        private float timeOfLastCheck;

        private bool isRadarWarning;
        
        protected override void Update()
        {
            if(!IsOn) return;
            
            base.Update();

            if (Time.time > timeOfLastCheck + timeBetweenChecks)
            {
                RadarCheck();
                timeOfLastCheck = Time.time;
            }
        }

        public override void TurnOn()
        {
            base.TurnOn();
            RefreshCheck();
        }

        private void RadarCheck()
        {
            var target = CreaturesManager.Instance.Elements.GetClosestElementInRange(transform.position, CurrentData.Range);
            
            var enemiesNear = target != null;

            if (isRadarWarning && !enemiesNear)
            {
                isRadarWarning = false;
                TurnAllOff();
            }
            else if(!isRadarWarning && enemiesNear)
            {
                isRadarWarning = true;
                TurnAllOn();
            }
        }

        private void RefreshCheck()
        {
            if (isRadarWarning)
            {
                isRadarWarning = false;
                TurnAllOff();
            }
            else if(!isRadarWarning)
            {
                isRadarWarning = true;
                TurnAllOn();
            }
        }

        private void TurnAllOff()
        {
            var towersInRange =
                TowersManager.Instance.Elements.GetAllElementInRange(transform.position, CurrentData.Range);
            for (int i = 0; i < towersInRange.Count; i++)
            {
                var tower = towersInRange[i];
                if(tower.Equals(this)) continue;
                if (!tower.Data.UIData.canBeTurnOnOff) continue;
                
                tower.TurnOff();
            }
        }

        private void TurnAllOn()
        {
            var towersInRange =
                TowersManager.Instance.Elements.GetAllElementInRange(transform.position, CurrentData.Range);
            for (int i = 0; i < towersInRange.Count; i++)
            {
                var tower = towersInRange[i];
                if(tower.Equals(this)) continue;
                if (!tower.Data.UIData.canBeTurnOnOff) continue;
                
                tower.TurnOn();
            }
        }
    }
}