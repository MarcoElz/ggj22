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
        [SerializeField] private Resource energy = default;
        [SerializeField] private Resource contamination = default;

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
            EnergyGeneration();
            ContaminationGeneration();
        }

        private void EnergyGeneration()
        {
            var energyGenerated = CalculateTotalGeneratedEnergyRate();
            var energyConsumed = CalculateTotalUsedEnergyRate();
            var totalEnergy = energyGenerated - energyConsumed;
            var totalEnergyThisFrame = totalEnergy * Time.deltaTime;
            
            if (totalEnergyThisFrame > 0f)
                inventory.Add(energy, totalEnergyThisFrame);
            else 
                inventory.Consume(energy, totalEnergyThisFrame);

            var currentEnergy = inventory.GetCurrentAmount(energy);
            if (currentEnergy <= Mathf.Epsilon)
                OnOutOfEnergy();
        }

        private void ContaminationGeneration()
        {
            var totalContamination = CalculateGeneratedContamination();
            var totalContaminationThisFrame = totalContamination * Time.deltaTime;
            
            if (totalContaminationThisFrame > 0f)
                inventory.Add(contamination, totalContaminationThisFrame);
            
        }

        private float CalculateTotalGeneratedEnergyRate()
        {
            var totalEnergy = 0f;
            for (int i = 0; i < generators.Length; i++)
            {
                var generator = generators[i];
                if(!generator.IsGenerating) continue;
                
                totalEnergy += generator.EnergyRate;
            }

            return totalEnergy;
        }

        private float CalculateTotalUsedEnergyRate()
        {
            var totalEnergy = 0f;
            var towers = TowersManager.Instance.Elements;
            for (int i = 0; i < towers.Length; i++)
            {
                var consumer = towers[i];
                if (!consumer.IsOn) continue;

                if (consumer.Data.EnergyCostPerSecond > 0f)
                    totalEnergy += consumer.Data.EnergyCostPerSecond;
            }

            return totalEnergy;
        }

        private void OnOutOfEnergy()
        {
            var towers = TowersManager.Instance.Elements;
            for (int i = 0; i < towers.Length; i++)
            {
                var tower = towers[i];
                if (!tower.IsOn) continue;
                
                if (tower.Data.EnergyCostPerSecond > 0f)
                    tower.TurnOff();
            }
        }

        private float CalculateGeneratedContamination()
        {
            var total = 0f;
            var towers = TowersManager.Instance.Elements;
            for (int i = 0; i < towers.Length; i++)
            {
                var tower = towers[i];
                if (!tower.IsOn) continue;

                if (tower.Data.ContaminationPerSecond > 0f)
                    total += tower.Data.ContaminationPerSecond;
            }

            return total;
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