using System;
using _Game.GameActions;
using _Game.InputHelper;
using _Game.Towers;
using Unity.Mathematics;
using UnityEngine;

namespace _Game.ShopSystem
{
    public class TowerBuilder : MonoBehaviour
    {
        [SerializeField] private Transform placeholder = default;
        [SerializeField] private BuyAction buyAction = default;
        [SerializeField] private DeleteAction deleteAction = default;
        
        public event Action<AbstractTower> onTowerCreated;
        public event Action<AbstractTower> onTowerDestroyed;
        
        public bool IsBuilding { get; private set; }

        private TowerGeneralData currentBuildingData;

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
            
            if(Input.GetMouseButtonDown(0))
                Build(currentBuildingData);
                
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
            placeholder.gameObject.SetActive(true);
        }

        public void StopBuilding()
        {
            IsBuilding = false;
            currentBuildingData = null;
            placeholder.gameObject.SetActive(false);
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