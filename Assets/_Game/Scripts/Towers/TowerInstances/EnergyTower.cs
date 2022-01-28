using UnityEngine;

namespace _Game.Towers
{
    public class EnergyTower : AbstractTower
    {
        private EnergyData currentData;
        private EnergyData CurrentData => currentData != null ? currentData : currentData = (EnergyData) currentAbstractData;

        public float EnergyRate => CurrentData.EnergyRate;
        
        
    }
}