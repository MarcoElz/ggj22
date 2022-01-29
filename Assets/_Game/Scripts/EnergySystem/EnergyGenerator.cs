using System.Collections.Generic;
using _Game.GameResources;
using _Game.InventorySystem;
using _Game.ShopSystem;
using _Game.Towers;
using UnityEngine;

namespace _Game.EnergySystem
{
    public class EnergyGenerator : MonoBehaviour
    {
        [SerializeField] private TowerBuilder builder = default;
        [SerializeField] private Inventory inventory = default;
        [SerializeField] private Resource resource = default;

        private List<IEnergyGenerator> generatorsList;
        private IEnergyGenerator[] generators;

        private void Awake()
        {
            generatorsList = new List<IEnergyGenerator>();
            RefreshArray();
        }

        private void OnEnable()
        {
            builder.onTowerCreated += OnNewTowerCreated;
            builder.onTowerDestroyed += OnTowerDestroyed;
        }

        private void OnDisable()
        {
            builder.onTowerCreated -= OnNewTowerCreated;
            builder.onTowerDestroyed -= OnTowerDestroyed;
        }

        private void Update()
        {
            var totalEnergy = 0f;
            for (int i = 0; i < generators.Length; i++)
            {
                var generator = generators[i];
                if(!generator.IsGenerating) continue;
                
                totalEnergy += generator.EnergyRate;
            }

            var totalEnergyThisFrame = totalEnergy * Time.deltaTime;
            if(totalEnergyThisFrame > 0f)
                inventory.Add(resource, totalEnergyThisFrame);
        }

        private void OnNewTowerCreated(AbstractTower tower)
        {
            if (tower is IEnergyGenerator energyTower) 
                Add(energyTower);
        }
        
        private void OnTowerDestroyed(AbstractTower tower)
        {
            if (tower is IEnergyGenerator energyTower) 
                Remove(energyTower);
        }

        private void Add(IEnergyGenerator generator)
        {
            generatorsList.Add(generator);
            RefreshArray();
        }

        private void Remove(IEnergyGenerator generator)
        {
            generatorsList.Remove(generator);
            RefreshArray();
        }
        
        private void RefreshArray() => generators = generatorsList.ToArray();
        
    }
}