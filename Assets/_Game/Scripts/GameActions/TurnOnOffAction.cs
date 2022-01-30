using _Game.ShopSystem;
using _Game.Towers;
using UnityEngine;

namespace _Game.GameActions
{
    [CreateAssetMenu(fileName = "TurnOnOff", menuName = "_Game/Actions/TurnOnOff", order = 1)]
    public class TurnOnOffAction : AbstractAction
    {
        public override bool HasTransaction => false;

        public override Transaction GetTransaction(AbstractTower tower) => default;

        public override bool CanDoAction(AbstractTower tower) => tower.Data.UIData.canBeTurnOnOff;

        public override bool TryTransaction(AbstractTower tower)
        {
            DoAction(tower);
            return true;
        }

        protected override void DoAction(AbstractTower tower)
        {
            if(tower.IsOn)
                tower.TurnOff();
            else
                tower.TurnOn();
        }
        

    }
}