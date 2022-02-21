using System;
using _Game.GameActions;
using _Game.GameResources;
using _Game.InputHelper;
using _Game.ShopSystem;
using _Game.Towers;
using _Game.UI.Inventory;
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
        [SerializeField] private ResourceAmountUI energyPerTime = default;
        [SerializeField] private ResourceAmountUI contaminationPerTime = default;
        
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
                if(!MouseHelper.Instance.IsOverUI())
                {
                    Hide();
                    isActive = false;
                    currentTower = null;
                }
            }
            else if (!isActive && validTower)
                Init(tower);
            else if (isActive && !currentTower.Equals(tower))
            {
                if(!MouseHelper.Instance.IsOverUI())
                    Init(tower);
            }

            if(isActive && currentTower != null)
                bar.Set(currentTower.Health,currentTower.MaxHealth);
        }

        private void LateUpdate()
        {
            if (!isActive) return;
            if (currentTower == null) return;

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
            
            SetCostPerTime();
            
            foreach (var button in actionButtons) 
                button.OnShow(tower);
            
            var uiData = currentTower.Data.UIData;
            deleteButton.interactable = uiData.canBeDeleted;
            
            specialActionButton.gameObject.SetActive(uiData.hasSpecialAction);
            specialActionButton.SetSprite(uiData.specialActionSprite);
        }

        private void SetCostPerTime()
        {
            var energy = -currentTower.Data.EnergyCostPerSecond;
            var contamination = currentTower.Data.ContaminationPerSecond;

            if (currentTower is IEnergyGenerator generator) 
                energy += generator.EnergyRate;

            if (currentTower is BaseTower baseTower)
                contamination += baseTower.Data.ContaminationPerSecond * Global.Difficult;

            energyPerTime.Init(energy);
            contaminationPerTime.Init(contamination);
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
            if(!action.HasTransaction) return;
            
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