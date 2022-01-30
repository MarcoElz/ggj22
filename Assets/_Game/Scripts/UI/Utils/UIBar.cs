using TMPro;
using UnityEngine;

namespace _Game.UI.Utils
{
    public class UIBar : MonoBehaviour
    {
        [SerializeField] private TMP_Text label = default;
        [SerializeField] private RectTransform background = default;
        [SerializeField] private RectTransform fill = default;
        [SerializeField] private bool showMaxCapacity = true;
        [SerializeField] private string format = "F1";

        public float MaxWidth => background.rect.width;
        public float MaxHeight => background.rect.height;

        public void Set(float current, float max)
        {
            var currentString = current.ToString(format);
            if (showMaxCapacity)
                label.text = $"{currentString} / {max}";
            else
                label.text = currentString;

            
            var percentage = current / max;
            var currentWidth = MaxWidth * percentage;
            fill.sizeDelta = new Vector2(currentWidth, MaxHeight);
        }
    }
}