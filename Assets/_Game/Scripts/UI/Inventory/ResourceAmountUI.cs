using _Game.GameResources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.UI.Inventory
{
    public class ResourceAmountUI : MonoBehaviour
    {
        [SerializeField] private Resource resource = default;
        [SerializeField] private Image icon = default;
        [SerializeField] private TMP_Text label = default;

        public Resource Resource => resource;

        private void Awake()
        {
            icon.sprite = Resource.Icon;
        }

        public void Init(float amount)
        {
            label.text = amount.ToString("F0");
        }
    }
}