using System;
using _Game.GameResources;
using _Game.InventorySystem;
using Ignita.Utils.Extensions;
using Ignita.Utils.ObjectPool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Game.Creatures
{
    public class EnemyGenerator : MonoBehaviour
    {
        [SerializeField] private Inventory inventory = default;
        [SerializeField] private Resource resource = default;
        [SerializeField] private EnemySpawnData[] enemyCreatures = default;
        [SerializeField] private float timeToCheck = 1f;
        

        private float timeOfLastCheck;
        
        private void Update()
        {
            if (Time.time > timeOfLastCheck + timeToCheck)
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
            inventory.Consume(resource, spawnData.cost);

            var randomPoint = Random.insideUnitCircle * Random.Range(20f, 30f);
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