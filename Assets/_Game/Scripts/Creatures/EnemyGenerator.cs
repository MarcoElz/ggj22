using System;
using _Game.GameResources;
using _Game.InventorySystem;
using _Game.ShopSystem;
using Ignita.Utils.Extensions;
using Ignita.Utils.ObjectPool;
using MEC;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Game.Creatures
{
    public class EnemyGenerator : MonoBehaviour
    {
        [SerializeField] private Inventory inventory = default;
        [SerializeField] private Resource resource = default;
        [SerializeField] private EnemySpawnData tutorialCreature = default;
        [SerializeField] private EnemySpawnData[] enemyCreatures = default;
        [SerializeField] private float timeToCheck = 1f;
        [SerializeField] private TowerBuilder builder = default;
        
        private float timeOfLastCheck;

        private void Start()
        {
            Timing.CallDelayed(1f, () => Spawn(tutorialCreature));
        }

        private void Update()
        {
            if(Global.IsGameOver) return; //TODO: clean this code

            var currentAmount = inventory.GetCurrentAmount(resource);
            var toCheck = timeToCheck / currentAmount;
            if (Time.time > timeOfLastCheck + toCheck)
            {
                Check();
                timeOfLastCheck = Time.time;
            }
        }

        private void Check()
        {
            var currentAmount = inventory.GetCurrentAmount(resource);

            var randomEnemy = enemyCreatures.GetRandomElement();
            if (randomEnemy.cost < currentAmount)
            {
                Spawn(randomEnemy);
            }

        }

        private void Spawn(EnemySpawnData spawnData)
        {
            inventory.Consume(resource, spawnData.cost* 0.70f);

            var minDistance = 20f;
            var maxDistance = 30f;

            if (builder.Antennas.Count > 0)
            {
                var anttenna = builder.Antennas.GetRandomElement();
                var extraDistance = Vector3.Distance(anttenna.transform.position, Global.MainTower.transform.position);

                minDistance += extraDistance;
                maxDistance += extraDistance;
            }
            
            var randomPoint = Random.insideUnitCircle * Random.Range(minDistance, maxDistance);
            var position = new Vector3(randomPoint.x, 0f, randomPoint.y);
            
            
            PoolManager.Spawn(spawnData.prefab, position, Quaternion.identity);
        }
    }

    [Serializable]
    public struct EnemySpawnData
    {
        public EnemyCreature prefab;
        public float cost;
    }
}