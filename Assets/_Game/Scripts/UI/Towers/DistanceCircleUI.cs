using _Game.UI.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.UI.Towers
{
    public class DistanceCircleUI : FaderUI
    {
        [SerializeField] private Image image;
        [SerializeField] private Sprite small;
        [SerializeField] private Sprite medium;
        [SerializeField] private Sprite large;
        
        public void Init(Vector3 position, float range)
        {
            if (range > 20) image.sprite = large;
            else if (range > 9) image.sprite = medium;
            else image.sprite = small;

            transform.localScale = Vector3.one * range;

            position.y = transform.position.y;
            transform.position = position;
            
            base.Show();
        }
    }
}
