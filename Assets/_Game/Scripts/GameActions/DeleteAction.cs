using _Game.GameResources;
using _Game.ShopSystem;
using _Game.Towers;
using UnityEngine;

namespace _Game.GameActions
{
    [CreateAssetMenu(fileName = "Delete", menuName = "_Game/Actions/Delete", order = 1)]
    public class DeleteAction : AbstractAction
    {
        private TowerBuilder towerBuilder;
        
        public void Init(TowerBuilder builder)
        {
            towerBuilder = builder;
        }
        
        public override Transaction GetTransaction(AbstractTower tower)
        {
            var transaction = new Transaction {resourceCosts = new ResourceAmount[3]};
            transaction.resourceCosts[0] = calculateTransaction(tower, Global.Energy, 0.2f, false);
            transaction.resourceCosts[1] = calculateTransaction(tower, Global.Metal, tower.HealthPercentage);
            transaction.resourceCosts[2] = calculateTransaction(tower, Global.Knowledge, tower.HealthPercentage);
            return transaction;
        }

        protected override void DoAction(AbstractTower tower)
        {
            towerBuilder.RemoveTower(tower);
        }
    }
}