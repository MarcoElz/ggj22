using _Game.GameResources;
using UnityEngine;

namespace _Game.Towers
{
    public class EnergyTower : AbstractTower, IEnergyGenerator
    {
        private EnergyData currentData;
        private EnergyData CurrentData => currentData != null ? currentData : currentData = (EnergyData) currentAbstractData;

        public float EnergyRate => CurrentData.EnergyRate;
        public bool IsGenerating => IsOn;


    }
}