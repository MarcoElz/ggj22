using _Game.Towers;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.UI.Towers
{
    public class TurnOnOffButton : ActionButton
    {
        [SerializeField] private Image image = default;

        [SerializeField] private Sprite onSprite = default;
        [SerializeField] private Sprite offSprite = default;

        private AbstractTower currentTower;
        
        public override void OnShow(AbstractTower tower)
        {
            var canBeTurnOnoff = tower.Data.UIData.canBeTurnOnOff;
            button.interactable = canBeTurnOnoff;

            currentTower = tower;
            base.OnShow(tower);

            var sprite = currentTower.IsOn ? onSprite : offSprite;
            image.sprite = sprite;
        }

        protected override void PressAction()
        {
            base.PressAction();
            var sprite = currentTower.IsOn ? onSprite : offSprite;
            image.sprite = sprite;
        }
    }
}