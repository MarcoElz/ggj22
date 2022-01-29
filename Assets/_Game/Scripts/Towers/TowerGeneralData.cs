using _Game.ShopSystem;
using _Game.UI.Utils;
using UnityEngine;

namespace _Game.Towers
{
    [CreateAssetMenu(fileName = "T_", menuName = "_Game/Towers/Data", order = 1)]
    public class TowerGeneralData : ScriptableObject
    {
        [SerializeField] private AbstractTower towerPrefab = default;
        [SerializeField] private TowerLabel[] labels = default;
        [SerializeField] private float energyCostPerSecond = 1f;
        [SerializeField] private float contaminationPerSecond = 0f;

        [SerializeField] private AbstractSpecificTowerData[] upgradesData = default;

        [SerializeField] private TowerUIData uiData;
        
        public AbstractTower TowerPrefab => towerPrefab;
        public TowerLabel[] Labels => labels;
        public float EnergyCostPerSecond => energyCostPerSecond;
        public float ContaminationPerSecond => contaminationPerSecond;
        public AbstractSpecificTowerData[] UpgradesData => upgradesData;
        public Cost InitialCost => upgradesData[0].Cost;
        
        public TowerUIData UIData => uiData;
    }
}