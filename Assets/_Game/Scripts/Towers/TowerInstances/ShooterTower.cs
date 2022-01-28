using _Game.Creatures;
using Ignita.Utils.Extensions;

namespace _Game.Towers
{
    public class ShooterTower : AbstractTower
    {
        
        public void Attack()
        {
            var target = CreaturesManager.Instance.Elements.GetClosestElementInRange(transform.position, 10f);
            if(target == null)
                return;
            
            
        }
        
    }
}