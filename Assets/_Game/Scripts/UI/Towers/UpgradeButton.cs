using _Game.Towers;

namespace _Game.UI.Towers
{
    public class UpgradeButton : ActionButton
    {
        public override void OnShow(AbstractTower tower)
        {
            var canUpgrade = tower.HasNextUpgrade;
            button.interactable = canUpgrade;
        }
    }
}