using UnityEngine;

namespace _Game.Towers
{
    [CreateAssetMenu(fileName = "T_Antenna_0", menuName = "_Game/Towers/Specifics/Antenna", order = 1)]
    public class AntennaData : AbstractSpecificTowerData
    {
        [SerializeField] private float extraRange = 1f;

        public override float Range => extraRange;
    }
}