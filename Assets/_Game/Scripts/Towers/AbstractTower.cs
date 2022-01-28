using System;
using UnityEngine;

namespace _Game.Towers
{
    public abstract class AbstractTower : MonoBehaviour
    {
        [SerializeField] private TowerGeneralData generalData = default;

        private int upgradeLevel;
        public bool HasNextUpgrade => upgradeLevel < generalData.UpgradesData.Length -1;
        public bool IsOn { get; private set; }
        
        public event Action onTurnedOn;
        public event Action onTurnedOff;
        public event Action onUpgraded;

        protected AbstractSpecificTowerData currentAbstractData => generalData.UpgradesData[upgradeLevel];

        protected virtual void OnEnable()
        {
            TowersManager.Instance.Add(this);
        }

        protected virtual void OnDisable()
        {
            TowersManager.Instance.Remove(this);
        }
        
        protected virtual void Update() { }
        
        public virtual void TurnOn()
        {
            IsOn = true;
            onTurnedOn?.Invoke();
        }

        public virtual void TurnOff()
        {
            IsOn = false;
            onTurnedOff?.Invoke();
        }

        public void Upgrade()
        {
            upgradeLevel++;
            onUpgraded();
            onUpgraded?.Invoke();
        }

        protected virtual void OnUpgraded() { }
    }
}