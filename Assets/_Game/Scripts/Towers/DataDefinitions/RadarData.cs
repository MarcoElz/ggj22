using UnityEngine;

namespace _Game.Towers
{
    [CreateAssetMenu(fileName = "T_Radar_0", menuName = "_Game/Towers/Specifics/Radar", order = 1)]
    public class RadarData : AbstractSpecificTowerData
    {
        [SerializeField] private float checkRange = 1f;

        public override float Range => checkRange;
    }
}