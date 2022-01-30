using System;
using _Game.GameActions;
using _Game.InputHelper;
using _Game.Towers;
using _Game.UI.Towers;
using _Game.UI.Utils;
using Ignita.Utils.Extensions;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Game.ShopSystem
{
    public class TowerBuilder : MonoBehaviour
    {
        //[SerializeField] private Transform placeholder = default;
        [SerializeField] private Material placeholderMaterialPositive = default;
        [SerializeField] private Material placeholderMaterialNegative = default;
        [SerializeField] private BuyAction buyAction = default;
        [SerializeField] private DeleteAction deleteAction = default;
        [SerializeField] private DistanceCircleUI rangeUI = default;
        
        public event Action<AbstractTower> onTowerCreated;
        public event Action<AbstractTower> onTowerDestroyed;
        
        public bool IsBuilding { get; private set; }

        private TowerGeneralData currentBuildingData;
        private Transform placeholder;

        private bool canBuildInPositionLastFrame;

        private void Awake()
        {
            StopBuilding();
            deleteAction.Init(this);
        }

        private void Update()
        {
            if(!IsBuilding) return;

            var position = MouseHelper.Instance.WorldPoint;
            placeholder.position = position;

            var canBuildInPosition = CanBuildInPosition(position);
            
            //TODO: Change color placeholder
            if(canBuildInPositionLastFrame != canBuildInPosition)
                SetMaterialToPlaceholder(canBuildInPosition);
            
            if(canBuildInPosition && Input.GetMouseButtonDown(0))
                Build(currentBuildingData);

            canBuildInPositionLastFrame = canBuildInPosition;
        }

        private bool CanBuildInPosition(Vector3 position)
        {
            if (IsOverUI()) return false;
            if (!IsInsideBaseRange(position)) return false;
            if (IsCollidingWithOtherTower(position)) return false;

            return true;
        }
        
        private bool IsOverUI() =>EventSystem.current.IsPointerOverGameObject();

        private bool IsInsideBaseRange(Vector3 position)
        {
            var sqrDistanceToBase = Vector3.SqrMagnitude(position - Global.MainTower.transform.position);
            var range = Global.MainTower.CurrentAbstractData.Range;
            var isInsideBaseRange = sqrDistanceToBase < range * range;
            return isInsideBaseRange;
        }

        private bool IsCollidingWithOtherTower(Vector3 position)
        {
            var closestTower = TowersManager.Instance.Elements.GetClosestElementInRange(position, 2f);
            var isOtherTowerTooNear = closestTower != null;

            return isOtherTowerTooNear;
        }

        public void TryBuildMode(TowerGeneralData towerData)
        {
            if (towerData.Equals(currentBuildingData))
            {
                StopBuilding();
                return;
            }
            
            var valid = buyAction.CanDoAction(towerData.TowerPrefab);
            if(!valid) return;
            
            currentBuildingData = towerData;
            StartBuilding();
        }

        private void StartBuilding()
        {
            IsBuilding = true;
            //placeholder.gameObject.SetActive(true);
            rangeUI.Init(Global.MainTower.transform.position, Global.MainTower.CurrentAbstractData.Range);

            //TODO: Improve this...
            var newPlaceholder = Instantiate(currentBuildingData.TowerPrefab.CurrentAbstractData.NewPrefab);
            placeholder = newPlaceholder.transform;
            SetMaterialToPlaceholder(true);
        }

        private void SetMaterialToPlaceholder(bool canBuild)
        {
            var material = canBuild ? placeholderMaterialPositive : placeholderMaterialNegative;
            
            var renderers = placeholder.GetComponentsInChildren<Renderer>(true);
            foreach (var render in renderers)
            {
                render.sharedMaterial = material;
                for (int i = 0; i < render.sharedMaterials.Length; i++) 
                    render.sharedMaterials[i] = material;
            }
        }

        public void StopBuilding()
        {
            IsBuilding = false;
            currentBuildingData = null;
            //placeholder.gameObject.SetActive(false);
            if(placeholder != null)
                Destroy(placeholder.gameObject);
            rangeUI.Hide();
        }

        public void RemoveTower(AbstractTower tower)
        {
            tower.TurnOff();
            Destroy(tower.gameObject);
            onTowerDestroyed?.Invoke(tower);
        }

        private void Build(TowerGeneralData data)
        {
            var valid = buyAction.TryTransaction(data.TowerPrefab);
            if(!valid) return;
            
            var tower = Instantiate(data.TowerPrefab, placeholder.transform.position, quaternion.identity);
            tower.TurnOn();
            
            onTowerCreated?.Invoke(tower);
            
            StopBuilding();
        }
    }
}