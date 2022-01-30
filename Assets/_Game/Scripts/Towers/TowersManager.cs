using _Game.Utils;
using UnityEngine;

namespace _Game.Towers
{
    public class TowersManager : AbstractGenericListManager<AbstractTower>
    {
        protected override bool IsValidElement(AbstractTower element) => element.IsAlive;
    }
}