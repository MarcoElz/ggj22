using _Game.GameResources;
using _Game.ShopSystem;
using _Game.Towers;
using UnityEngine;

namespace _Game.GameActions
{
    [CreateAssetMenu(fileName = "Repair", menuName = "_Game/Actions/Repair", order = 1)]
    public class RepairAction : AbstractAction
    {
        public override Transaction GetTransaction(AbstractTower tower)
        {
            var multiplier = 1f;
            if (tower.UpgradeLevel > 0) 
                multiplier += tower.UpgradeLevel;


            var transaction = new Transaction {resourceCosts = new ResourceAmount[3]};
            transaction.resourceCosts[0] = calculateTransaction(tower, Global.Energy, (tower.HealthPercentage * 0.25f) * multiplier, false);
            transaction.resourceCosts[1] = calculateTransaction(tower, Global.Metal, ((1.0f - tower.HealthPercentage) * 0.8f) * multiplier, false);
            transaction.resourceCosts[2] = calculateTransaction(tower, Global.Knowledge, 0f);
            return transaction;
        }

        protected override void DoAction(AbstractTower tower)
        {
            tower.RestoreHealth();
        }
    }
}