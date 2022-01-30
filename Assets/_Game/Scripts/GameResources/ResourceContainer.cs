using UnityEngine;

namespace _Game.GameResources
{
    public class ResourceContainer
    {
        public Resource Resource { get; private set; }
        public float Amount { get; private set; }
        public float MaxCapacity { get; private set; }
        public ResourceContainer(Resource resource, float startAmount)
        {
            Resource = resource;
            Amount = startAmount;
        }
        
        public void Add(float amount)
        {
            Amount += amount;
            if (Amount > MaxCapacity)
                Amount = MaxCapacity;
        }

        public void Remove(float amount)
        {
            Amount -= amount;
            if (Amount < 0)
                Amount = 0;
        }

        public void UpgradeCapacity(float newCapacity) => MaxCapacity = newCapacity;
    }
}