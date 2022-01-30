using _Game.ShopSystem;
using _Game.Towers;
using Ignita.Utils.ObjectPool;
using UnityEngine;

namespace _Game.GameActions
{
    [CreateAssetMenu(fileName = "Upgrade", menuName = "_Game/Actions/Upgrade", order = 1)]
    public class UpgradeAction : AbstractAction
    {
        [SerializeField] private TimeParticles upgradeParticles = default;
        public override Transaction GetTransaction(AbstractTower tower)
        {
            if (!tower.HasNextUpgrade) return default;
            return tower.Data.UpgradesData[tower.UpgradeLevel+1].Transaction;
        }

        public override bool CanDoAction(AbstractTower tower)
        {
            if (!tower.HasNextUpgrade) return false;
            return base.CanDoAction(tower);
        }

        protected override void DoAction(AbstractTower tower)
        {
            tower.Upgrade();
            PoolManager.Spawn(upgradeParticles, tower.transform.position, Quaternion.identity);
        }

        // public override bool CanDoAction(AbstractTower tower)
        // {
        //     if (!tower.HasNextUpgrade) return false;
        //     var cost = tower.Data.UpgradesData[tower.UpgradeLevel].Cost;
        //     return inventory.CanConsumeCost(cost);
        // }

    }
}