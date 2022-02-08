using _Game.ShopSystem;
using _Game.Utils;
using UnityEngine;

namespace _Game.Towers
{
    public class TowersManager : AbstractGenericListManager<AbstractTower>
    {
        [SerializeField] private TowerBuilder builder = default;
        protected override bool IsValidElement(AbstractTower element) => element.IsAlive;

        public void OnDeadTower(AbstractTower tower)
        {
            builder.RemoveTower(tower);
        }
    }
}