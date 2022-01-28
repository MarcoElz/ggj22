using System;
using UnityEngine;

namespace _Game.Towers
{
    public abstract class AbstractTower : MonoBehaviour
    {
        [SerializeField] private TowerGeneralData generalData = default;

        public int upgradeLevel;
        public bool hasNextUpgrade => upgradeLevel < generalData.UpgradesData.Length -1;

        protected AbstractSpecificTowerData currentAbstractData => generalData.UpgradesData[upgradeLevel];

        public event Action onTurnedOn;
        public event Action onTurnedOff;
        public event Action onUpgraded;

        public virtual void TurnOn()
        {
            onTurnedOn?.Invoke();
        }

        public virtual void TurnOff()
        {
            onTurnedOff?.Invoke();
        }

        public void Upgrade()
        {
            upgradeLevel++;
        }
    }
}