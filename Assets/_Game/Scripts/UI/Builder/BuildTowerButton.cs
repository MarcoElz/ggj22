using System;
using _Game.GameActions;
using _Game.ShopSystem;
using _Game.Towers;
using _Game.UI.Towers;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Game.UI.Builder
{
    public class BuildTowerButton : ActionButton
    {
        [SerializeField] private TMP_Text nameLabel = default;
        [SerializeField] private Image image = default;
        [SerializeField] private Button button = default;
        [SerializeField] private TransactionUi transactionUi = default;

        private TowerGeneralData currentTower;

        public void Init(TowerGeneralData tower)
        {
            currentTower = tower;
            nameLabel.text = tower.DisplayName;
            image.sprite = tower.Sprite;
            button.onClick.AddListener(OnPressed);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            transactionUi.Init(currentTower.TowerPrefab, action);
        }
        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            transactionUi.Hide();
        }

        private void OnPressed()
        {
            var builder = FindObjectOfType<TowerBuilder>();
            builder.TryBuildMode(currentTower);
        }
        
        
    }
}