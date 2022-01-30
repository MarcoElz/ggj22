using _Game.GameResources;
using _Game.ShopSystem;
using _Game.Towers;
using UnityEngine;

namespace _Game.GameActions
{
    public abstract class AbstractAction : ScriptableObject
    {
        private bool hasTransaction = true;
        public virtual bool HasTransaction => hasTransaction;

        public abstract Transaction GetTransaction(AbstractTower tower);
        //public abstract bool CanDoAction(AbstractTower tower);

        public virtual bool CanDoAction(AbstractTower tower)
        {
            if (!HasTransaction) return true;

            return Global.Shop.CanDoTransaction(GetTransaction(tower));
        }
        
        public virtual bool TryTransaction(AbstractTower tower)
        {
            var completed = Global.Shop.TryTransaction(GetTransaction(tower));
            
            if(completed)
                DoAction(tower);

            return completed;
        }

        protected abstract void DoAction(AbstractTower tower);

        protected ResourceAmount calculateTransaction(AbstractTower tower, Resource resource, float percentage, bool isPositive = true)
        {
            var resourceAmount = new ResourceAmount();
            resourceAmount.resource = resource;
            resourceAmount.amount = percentage * tower.Data.GetInitialCostFor(resource);

            resourceAmount.amount = Mathf.Abs(resourceAmount.amount);
            if (!isPositive)
                resourceAmount.amount = -resourceAmount.amount;
                
            
            return resourceAmount;
        }
    }
}