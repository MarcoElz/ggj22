using System;
using _Game.GameActions;
using _Game.InputHelper;
using _Game.ShopSystem;
using _Game.Towers;
using _Game.UI.Utils;
using Ignita.Utils.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.UI.Towers
{
    public class TowerUI : FaderUI
    {
        [Header("Tower Ui Settings")]
        [SerializeField] private float range = 5f;

        [Header("References")] 
        [SerializeField] private TransactionUi transaction = default;
        [SerializeField] private DistanceCircleUI rangeUI = default;
        [SerializeField] private SpecialActionButton specialActionButton = default;
        [SerializeField] private Button deleteButton = default;
        [SerializeField] private UIBar bar = default;
        
        private Camera cam;
        private Camera Cam => cam == null ? cam = Camera.main : cam;

        private RectTransform rectTransform;
        private ActionButton[] actionButtons;
        
        private AbstractTower currentTower;
        private bool isActive;

        protected override void Awake()
        {
            base.Awake();
            rectTransform = GetComponent<RectTransform>();
            actionButtons = GetComponentsInChildren<ActionButton>();
        }

        private void OnEnable()
        {
            foreach (var button in actionButtons)
            {
                button.onEntered += OnActionButtonEntered;
                button.onExited += OnActionButtonExited;
                button.onPressed += OnActionButtonPressed;
            }
        }

        private void OnDisable()
        {
            foreach (var button in actionButtons)
            {
                button.onEntered -= OnActionButtonEntered;
                button.onExited -= OnActionButtonExited;
                button.onPressed -= OnActionButtonPressed;
            }
        }

        //TODO: Move to other script, and refactor...
        private void Update()
        {
            var position = MouseHelper.Instance.WorldPoint;
            //position += (Vector3.forward + Vector3.right) * 1.0f;
            var tower = TowersManager.Instance.Elements.GetClosestElementInRange(position, range);

            var validTower = tower != null && tower.TimeSinceSpawn > 1.0f;

            if (isActive && !validTower)
            {
                Hide();
                isActive = false;
                currentTower = null;
            }
            else if (!isActive && validTower)
                Init(tower);
            else if(isActive && !currentTower.Equals(tower)) 
                Init(tower);

            if(isActive && currentTower != null)
                bar.Set(currentTower.Health,currentTower.MaxHealth);
        }

        private void LateUpdate()
        {
            if(!isActive) return;

            var position = Cam.WorldToScreenPoint(currentTower.transform.position);
            rectTransform.position = position;
        }

        public void Init(AbstractTower tower)
        {
            var builder = FindObjectOfType<TowerBuilder>();
            if (builder.IsBuilding) return;
            
            currentTower = tower;
            base.Show();
            isActive = true;
            
            foreach (var button in actionButtons) 
                button.OnShow(tower);
            
            var uiData = currentTower.Data.UIData;
            deleteButton.interactable = uiData.canBeDeleted;
            
            specialActionButton.gameObject.SetActive(uiData.hasSpecialAction);
            specialActionButton.SetSprite(uiData.specialActionSprite);
        }

        protected override void OnShow()
        {
            base.OnShow();
            rangeUI.Init(currentTower.transform.position, currentTower.CurrentAbstractData.Range);
        }

        protected override void OnHide()
        {
            base.OnHide();
            rangeUI.Hide();
        }

        private void OnActionButtonEntered(AbstractAction action)
        {
            
            
            transaction.Init(currentTower, action);
        }

        private void OnActionButtonExited()
        {
            transaction.Hide();
        }

        private void OnActionButtonPressed(AbstractAction action)
        {
            action.TryTransaction(currentTower);
        }

    }
}