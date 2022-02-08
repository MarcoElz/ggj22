using System.Collections.Generic;
using System.Linq;
using _Game.GameResources;
using _Game.ShopSystem;
using _Game.UI.Utils;
using UnityEngine;

namespace _Game.Towers
{
    [CreateAssetMenu(fileName = "T_", menuName = "_Game/Towers/Data", order = 1)]
    public class TowerGeneralData : ScriptableObject
    {
        [Header("Builder UI")]
        [SerializeField] private string displayName = "";
        [SerializeField] private Sprite sprite = default;
        [SerializeField] private string shortDescription = "";
        
        [Header("General")]
        [SerializeField] private AbstractTower towerPrefab = default;
        [SerializeField] private TowerLabel[] labels = default;
        [SerializeField] private float energyCostPerSecond = 1f; //TODO: Move to specific tower data
        [SerializeField] private float contaminationPerSecond = 0f; //TODO: Move to specific tower data

        [SerializeField] private AbstractSpecificTowerData[] upgradesData = default;

        [SerializeField] private TowerUIData uiData;

        public string DisplayName => displayName;
        public string ShortDescription => shortDescription;
        public Sprite Sprite => sprite;
        public AbstractTower TowerPrefab => towerPrefab;
        public TowerLabel[] Labels => labels;
        public float EnergyCostPerSecond => energyCostPerSecond;
        public float ContaminationPerSecond => contaminationPerSecond;
        public AbstractSpecificTowerData[] UpgradesData => upgradesData;
        public Transaction InitialTransaction => upgradesData[0].Transaction;
        
        public TowerUIData UIData => uiData;
        
        private Dictionary<Resource, float> initialCostPerResource;

        private Dictionary<Resource, float> InitialCostPerResource
        {
            get
            {
                if(initialCostPerResource == null)
                    initialCostPerResource = InitialTransaction.resourceCosts.ToDictionary(
                        r => r.resource,
                        r => r.amount);
                return initialCostPerResource;
            }
        }

        public float GetInitialCostFor(Resource resource)
        {
            if (!InitialCostPerResource.ContainsKey(resource))
                return 0f;
            
            return InitialCostPerResource[resource];
        }
    }
}