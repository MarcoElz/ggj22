using _Game.GameResources;

namespace _Game.Towers
{
    public class EnergyTower : AbstractTower, IEnergyGenerator
    {
        private EnergyData currentData;
        //private EnergyData CurrentData => currentData != null ? currentData : currentData = (EnergyData) CurrentAbstractData;
        private EnergyData CurrentData => (EnergyData) CurrentAbstractData;

        public float EnergyRate => CurrentData.EnergyRate;
        public bool IsGenerating => IsOn;


    }
}