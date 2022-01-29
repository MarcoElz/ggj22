using UnityEngine;

namespace _Game.GameResources
{
    public class ResourceContainer
    {
        public Resource Resource { get; private set; }
        public float Amount { get; private set; }

        public ResourceContainer(Resource resource, float startAmount)
        {
            Resource = resource;
            Amount = startAmount;
        }
        
        public void Add(float amount) => this.Amount += amount;
        public void Remove(float amount) => this.Amount -= amount;
    }
}