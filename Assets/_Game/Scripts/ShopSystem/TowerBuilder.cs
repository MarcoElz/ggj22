using System;
using _Game.Towers;
using Unity.Mathematics;
using UnityEngine;

namespace _Game.ShopSystem
{
    public class TowerBuilder : MonoBehaviour
    {
        [SerializeField] private Shop shop = default;
        [SerializeField] private Transform placeholder = default;
        [SerializeField] private LayerMask layerMask = default;
        
        public event Action<AbstractTower> onTowerCreated;
        public event Action<AbstractTower> onTowerDestroyed;
        
        public bool IsBuilding { get; private set; }

        private TowerGeneralData currentBuildingData;

        private void Awake()
        {
            StopBuilding();
        }

        private void Update()
        {
            if(!IsBuilding) return;

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, layerMask))
            {
                placeholder.position = hit.point;
            }
            
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
            
            var valid = shop.CanBuyTower(towerData);
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
            //TODO: Cambio de recursos
            tower.TurnOff();
            Destroy(tower);
            onTowerDestroyed?.Invoke(tower);
        }

        private void Build(TowerGeneralData data)
        {
            var valid = shop.TryBuyTower(data);
            if(!valid) return;
            
            var tower = Instantiate(data.TowerPrefab, placeholder.transform.position, quaternion.identity);
            tower.TurnOn();
            
            onTowerCreated?.Invoke(tower);
            
            StopBuilding();
        }
    }
}