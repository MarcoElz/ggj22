using System;
using _Game.GameActions;
using _Game.InputHelper;
using _Game.Towers;
using _Game.UI.Utils;
using Ignita.Utils.Extensions;
using UnityEngine;

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
        [SerializeField] private GameObject deleteButton = default;
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
            var tower = TowersManager.Instance.Elements.GetClosestElementInRange(position, range);

            var validTower = tower != null && tower.TimeSinceSpawn > 1.5f;

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
            currentTower = tower;
            base.Show();
            isActive = true;

            var uiData = currentTower.Data.UIData;
            deleteButton.gameObject.SetActive(uiData.canBeDeleted);
            
            specialActionButton.gameObject.SetActive(uiData.hasSpecialAction);
            specialActionButton.SetSprite(uiData.specialActionSprite);
            
            foreach (var button in actionButtons) 
                button.OnShow(tower);
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
            if (!action.HasTransaction || !action.CanDoAction(currentTower))
                return;
            
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