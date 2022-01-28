using _Game.Creatures;
using Ignita.Utils.Extensions;

namespace _Game.Towers
{
    public class AreaDamageTower : AbstractTower
    {

        public void Attack()
        {
            var targets = CreaturesManager.Instance.Elements.GetAllElementInRange(transform.position, 5f);
            if(targets.Count == 0) return;

            foreach (var target in targets)
            {
                target.Hurt(10);
            }
        }
    }
}