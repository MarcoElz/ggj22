using System;
using _Game.DamageSystem;
using UnityEngine;

namespace _Game.Towers
{
    public abstract class AbstractTower : MonoBehaviour, IDamageable
    {
        [SerializeField] private TowerGeneralData generalData = default;

        public TowerGeneralData Data => generalData;
        public int UpgradeLevel { get; private set; }
        public bool HasNextUpgrade => UpgradeLevel < generalData.UpgradesData.Length -1;
        public bool IsOn { get; private set; }

        public int Health { get; private set; }
        public int MaxHealth => currentAbstractData.MaxHealth;
        public float HealthPercentage => Health / currentAbstractData.MaxHealth;

        public float TimeSinceSpawn => Time.time - timeOfSpawn;
        
        public event Action onTurnedOn;
        public event Action onTurnedOff;
        public event Action onUpgraded;
        public event Action<int, int> onHealthChanged;

        protected AbstractSpecificTowerData currentAbstractData => generalData.UpgradesData[UpgradeLevel];

        private float timeOfSpawn;

        protected virtual void Awake()
        {
            timeOfSpawn = Time.time;
            RestoreHealth();
        }

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
        
        public void Hurt(int amount)
        {
            Health -= amount;

            if (Health <= 0)
            {
                Dead();
            }
            
            onHealthChanged?.Invoke(Health, currentAbstractData.MaxHealth);
        }

        public void RestoreHealth()
        {
            Health = currentAbstractData.MaxHealth;
            onHealthChanged?.Invoke(Health, currentAbstractData.MaxHealth);
        }
        

        public virtual void SpecialAction() { }

        protected virtual void OnUpgraded()
        {
            RestoreHealth();
        }

        private void Dead()
        {
            
        }
    }
}