namespace _Game.Towers
{
    public class AntennaTower : AbstractTower
    {
        private AntennaData currentData;
       //private AntennaData CurrentData => currentData != null ? currentData : currentData = (AntennaData) CurrentAbstractData;
       private AntennaData CurrentData => (AntennaData) CurrentAbstractData;
    }
}