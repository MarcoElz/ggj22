using System.Collections.Generic;
using System.Linq;
using _Game.GameActions;
using _Game.GameResources;
using _Game.ShopSystem;
using _Game.Towers;
using _Game.UI.Inventory;
using _Game.UI.Utils;
using TMPro;
using UnityEngine;

namespace _Game.UI.Towers
{
    public class TransactionUi : FaderUI
    {
        [SerializeField] private TMP_Text actionLabel = default;
        
        private ResourceAmountUI[] resourceCostUis;
        
        private Dictionary<Resource, ResourceAmountUI> currentResources;

        private AbstractTower currentTower;
        private AbstractAction currentAction;

        protected override void Awake()
        {
            base.Awake();
            resourceCostUis = GetComponentsInChildren<ResourceAmountUI>();
            
            //Create Dictionary
            currentResources = resourceCostUis.ToDictionary(
                (r) => r.Resource,
                (r) => r);
        }

        public void Init(AbstractTower tower, AbstractAction action)
        {
            //if(!action.CanDoAction(tower)) return;
            
            if (currentTower != null)
                currentTower.onHealthChanged -= OnHealthChanged;
            
            currentTower = tower;
            currentAction = action;

            currentTower.onHealthChanged += OnHealthChanged;

            var transaction = currentAction.GetTransaction(currentTower);
            RefreshTransactionUi(transaction);
            
            actionLabel.text = action.name;
            Show();
        }

        private void OnHealthChanged(float current, float max)
        {
            var transaction = currentAction.GetTransaction(currentTower);
            RefreshTransactionUi(transaction);
        }

        private void RefreshTransactionUi(Transaction transaction)
        {
            foreach (var resourceCost in transaction.resourceCosts)
                currentResources[resourceCost.resource].Init(resourceCost.amount);
        }
        
    }
}
