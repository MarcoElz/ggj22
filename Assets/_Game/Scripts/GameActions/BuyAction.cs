using _Game.ShopSystem;
using _Game.Towers;
using UnityEngine;

namespace _Game.GameActions
{
    [CreateAssetMenu(fileName = "Buy", menuName = "_Game/Actions/Buy", order = 1)]
    public class BuyAction : AbstractAction
    {
        public override Transaction GetTransaction(AbstractTower tower) => tower.Data.InitialTransaction;
        protected override void DoAction(AbstractTower tower)
        {
        }
    }
}