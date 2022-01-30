using _Game.ShopSystem;
using _Game.Towers;
using UnityEngine;

namespace _Game.GameActions
{
    [CreateAssetMenu(fileName = "Upgrade", menuName = "_Game/Actions/Upgrade", order = 1)]
    public class UpgradeAction : AbstractAction
    {
        public override Transaction GetTransaction(AbstractTower tower)
        {
            return tower.Data.UpgradesData[tower.UpgradeLevel].Transaction;
        }

        public override bool CanDoAction(AbstractTower tower)
        {
            if (!tower.HasNextUpgrade) return false;
            return base.CanDoAction(tower);
        }

        protected override void DoAction(AbstractTower tower)
        {
            tower.Upgrade();
        }

        // public override bool CanDoAction(AbstractTower tower)
        // {
        //     if (!tower.HasNextUpgrade) return false;
        //     var cost = tower.Data.UpgradesData[tower.UpgradeLevel].Cost;
        //     return inventory.CanConsumeCost(cost);
        // }

    }
}