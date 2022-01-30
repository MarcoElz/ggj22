using Ignita.Utils.ObjectPool;
using UnityEngine;

namespace _Game.GameResources
{
    //TODO: What did I just code? Figure out a better way to drop stuff. WTF.
    public class Dropper : MonoBehaviour
    {
        [SerializeField] private TakeableResource knowledgePrefab = default;
        [SerializeField] private TakeableResource metalPrefab = default;
        [SerializeField] private float randomDistance = 1f;
        
        public void AlertToDrop(Vector3 position)
        {
            var probability = Random.Range(1f, 100f);
            int difficult = Global.Difficult;

            probability += difficult * 10f;

            int knowledgeDrop = Mathf.RoundToInt(probability / 50f);
            int metalDrop = Mathf.RoundToInt(probability / 20f);
            
            Drop(knowledgePrefab, knowledgeDrop,position);
            Drop(metalPrefab, metalDrop, position);
        }

        private void Drop(TakeableResource dropPrefab, int count, Vector3 position)
        {
            for (int i = 0; i < count; i++) 
                Drop(dropPrefab, position);
        }

        private void Drop(TakeableResource dropPrefab, Vector3 position)
        {
            var randomCircle = Random.insideUnitCircle * randomDistance;
            position.x += randomCircle.x;
            position.z += randomCircle.y;
            
            var value = Random.Range(1, 1 + Global.Difficult);
            var drop = PoolManager.Spawn(dropPrefab, position);
            drop.Init(value);
        }
    }
}