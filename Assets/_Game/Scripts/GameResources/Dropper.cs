using Ignita.Utils.Extensions;
using Ignita.Utils.ObjectPool;
using UnityEngine;

namespace _Game.GameResources
{
    //TODO: What did I just code? Figure out a better way to drop stuff. WTF.
    public class Dropper : MonoBehaviour
    {
        [SerializeField] private TakeableResource[] knowledgePrefabs = default;
        [SerializeField] private TakeableResource[] metalPrefabs = default;
        [SerializeField] private float randomDistance = 1f;
        
        public void AlertToDrop(Vector3 position)
        {
            var probability = Random.Range(1f, 100f);
            int difficult = Global.Difficult;

            probability += difficult * 5f;

            int knowledgeDrop = Mathf.RoundToInt(probability / 75f);
            int metalDrop = Mathf.RoundToInt(probability / 50f);

            knowledgeDrop = Mathf.Clamp(knowledgeDrop, 0, 2);
            metalDrop = Mathf.Clamp(metalDrop, 1, 2);
            
            Drop(knowledgePrefabs.GetRandomElement(), knowledgeDrop,position, 1f);
            Drop(metalPrefabs.GetRandomElement(), metalDrop, position, 2f);
        }

        private void Drop(TakeableResource dropPrefab, int count, Vector3 position, float multiplier)
        {
            for (int i = 0; i < count; i++) 
                Drop(dropPrefab, position, multiplier);
        }

        private void Drop(TakeableResource dropPrefab, Vector3 position, float multiplier)
        {
            var randomCircle = Random.insideUnitCircle * randomDistance;
            position.x += randomCircle.x;
            position.z += randomCircle.y;
            
            var value = Random.Range(1, 1 + Global.Difficult * multiplier);
            var drop = PoolManager.Spawn(dropPrefab, position);
            drop.Init(value);
        }
    }
}