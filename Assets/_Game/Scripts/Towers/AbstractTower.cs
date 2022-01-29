using System;
using UnityEngine;

namespace _Game.Towers
{
    public abstract class AbstractTower : MonoBehaviour
    {
        [SerializeField] private TowerGeneralData generalData = default;

        public TowerGeneralData Data => generalData;
        public int UpgradeLevel { get; private set; }
        public bool HasNextUpgrade => UpgradeLevel < generalData.UpgradesData.Length -1;
        public bool IsOn { get; private set; }
        
        public event Action onTurnedOn;
        public event Action onTurnedOff;
        public event Action onUpgraded;

        protected AbstractSpecificTowerData currentAbstractData => generalData.UpgradesData[UpgradeLevel];

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
            UpgradeLevel++;
            onUpgraded();
            onUpgraded?.Invoke();
        }

        protected virtual void OnUpgraded() { }
    }
}