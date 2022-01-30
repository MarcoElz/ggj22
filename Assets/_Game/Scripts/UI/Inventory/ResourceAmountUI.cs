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

        private Color normal = Color.white;
        private Color positive = new Color(0.55f, 1f, 0.46f);
        private Color negative = new Color(1f, 0.56f, 0.56f);
        
        public Resource Resource => resource;

        private void Awake()
        {
            icon.sprite = Resource.Icon;
        }

        public void Init(float amount)
        {
            if (amount > 0)
            {
                label.text = "+" + amount.ToString("F0");
                label.color = positive;
            }
            else if(amount < 0)
            {
                label.text = amount.ToString("F0");
                label.color = negative;
            }
            else // == 0
            {
                label.text = amount.ToString("F0");
                label.color = normal;
            }
        }
    }
}