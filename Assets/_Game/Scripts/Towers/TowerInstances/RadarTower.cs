using System.Collections.Generic;
using _Game.Creatures;
using Ignita.Utils.Extensions;
using UnityEngine;

namespace _Game.Towers
{
    public class RadarTower : AbstractTower
    {
        [SerializeField] private float timeBetweenChecks = 0.5f;
        
        private RadarData currentData;
        //private RadarData CurrentData => currentData != null ? currentData : currentData = (RadarData) CurrentAbstractData;
        private RadarData CurrentData => (RadarData) CurrentAbstractData;

        private float timeOfLastCheck;

        private bool isRadarWarning;

        private List<AbstractTower> towersInRange;

        protected override void Awake()
        {
            base.Awake();
            towersInRange = new List<AbstractTower>();
        }

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
            towersInRange = TowersManager.Instance.Elements.GetAllElementInRange(transform.position, CurrentData.Range, towersInRange);
            for (int i = 0; i < towersInRange.Count; i++)
            {
                var tower = towersInRange[i];
                if(tower.Equals(this)) continue;
                if (!tower.Data.UIData.canBeTurnOnOff) continue;
                if (!HasLabel(tower)) continue;
                
                tower.TurnOff();
            }
        }

        private void TurnAllOn()
        {
            towersInRange = TowersManager.Instance.Elements.GetAllElementInRange(transform.position, CurrentData.Range, towersInRange);
            for (int i = 0; i < towersInRange.Count; i++)
            {
                var tower = towersInRange[i];
                if(tower.Equals(this)) continue;
                if (!tower.Data.UIData.canBeTurnOnOff) continue;
                if (!HasLabel(tower)) continue;
                
                tower.TurnOn();
            }
        }


        private bool HasLabel(AbstractTower tower)
        {
            var label = CurrentData.ControlLabel;
            var labels = tower.Data.Labels;
            for (int i = 0; i < labels.Length; i++)
            {
                var towerLabel = labels[i];
                if (towerLabel.Equals(label))
                    return true;
            }

            return false;
        }
    }
}