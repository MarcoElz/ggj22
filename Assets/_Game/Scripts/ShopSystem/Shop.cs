using System;
using _Game.InventorySystem;
using UnityEngine;

namespace _Game.ShopSystem
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private Inventory inventory = default;

        public event Action onTransactionCompleted;
        public event Action onTransactionFailed;

        public bool CanDoTransaction(Transaction transaction) => inventory.CanProcessTransaction(transaction);

        public bool TryTransaction(Transaction transaction)
        {
            var completed = inventory.TryProcessTransaction(transaction);
            TransactionFeedback(completed);
            return completed;
        }

        private void TransactionFeedback(bool completed)
        {
            if(completed)
                onTransactionCompleted?.Invoke();
            else
                onTransactionFailed?.Invoke();
        }
        
    }
}