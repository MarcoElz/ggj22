using UnityEngine;

namespace _Game.Towers
{
    [CreateAssetMenu(fileName = "T_Energy_0", menuName = "_Game/Towers/Specifics/Energy", order = 1)]
    public class EnergyData : AbstractSpecificTowerData
    {
        [SerializeField] private float energyRate = 1f;

        public float EnergyRate => energyRate;
    }
}