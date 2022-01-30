using _Game.GameResources;
using _Game.UI.Utils;
using UnityEngine;

namespace _Game.UI.Inventory
{
    public class ResourceUI : MonoBehaviour
    {
        [SerializeField] private Resource resource = default;
        [SerializeField] private UIBar bar = default;
        
        public Resource Resource => resource;

        public void Set(ResourceContainer container)
        {
            bar.Set(container.Amount, container.MaxCapacity);
        }
    }
}
