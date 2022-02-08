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
    //TODO: Should inherit ActionButton
    public class BuildTowerButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TMP_Text nameLabel = default;
        [SerializeField] private Image image = default;
        [SerializeField] private Button button = default;
        [SerializeField] private TransactionUi transactionUi = default;
        [SerializeField] protected AbstractAction action = default;
        [SerializeField] private TMP_Text descriptionLabel = default;


        private TowerGeneralData currentTower;

        public void Init(TowerGeneralData tower)
        {
            currentTower = tower;
            nameLabel.text = tower.DisplayName;
            descriptionLabel.text = tower.ShortDescription;
            image.sprite = tower.Sprite;
            button.onClick.AddListener(OnPressed);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            //base.OnPointerEnter(eventData);
            transactionUi.Init(currentTower.TowerPrefab, action);
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            //base.OnPointerExit(eventData);
            transactionUi.Hide();
        }

        private void OnPressed()
        {
            var builder = FindObjectOfType<TowerBuilder>();
            builder.TryBuildMode(currentTower);
        }
        
        
    }
}