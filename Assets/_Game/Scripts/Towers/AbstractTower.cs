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

        public float Health { get; private set; }
        public float MaxHealth => CurrentAbstractData.MaxHealth;
        public float HealthPercentage => Health / CurrentAbstractData.MaxHealth;
        public bool IsAlive => Health > 0f;

        public float TimeSinceSpawn => Time.time - timeOfSpawn;
        
        public event Action onTurnedOn;
        public event Action onTurnedOff;
        public event Action onUpgraded;
        public event Action<float, float> onHealthChanged;

        public AbstractSpecificTowerData CurrentAbstractData => generalData.UpgradesData[UpgradeLevel];

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
            if (!IsAlive) return;
            
            IsOn = true;
            var animator = GetComponentInChildren<Animator>();
            if(animator != null) animator.enabled = true;
            onTurnedOn?.Invoke();
        }

        public virtual void TurnOff()
        {
            IsOn = false;
            var animator = GetComponentInChildren<Animator>();
            if(animator != null) animator.enabled = false;
            onTurnedOff?.Invoke();
        }

        public void Upgrade()
        {
            UpgradeLevel++;
            onUpgraded();
            onUpgraded?.Invoke();
        }
        
        public void Hurt(float amount)
        {
            Health -= amount;

            if (Health <= 0)
            {
                Dead();
            }
            
            onHealthChanged?.Invoke(Health, CurrentAbstractData.MaxHealth);
        }

        public void RestoreHealth()
        {
            Health = CurrentAbstractData.MaxHealth;
            onHealthChanged?.Invoke(Health, CurrentAbstractData.MaxHealth);
        }
        

        public virtual void SpecialAction() { }

        protected virtual void OnUpgraded()
        {
            RestoreHealth();

            if (CurrentAbstractData.NewPrefab != null)
            {
                Destroy(transform.GetChild(0));
                Instantiate(CurrentAbstractData.NewPrefab, transform);
            }
        }

        private void Dead()
        {
            TurnOff();
        }
    }
}