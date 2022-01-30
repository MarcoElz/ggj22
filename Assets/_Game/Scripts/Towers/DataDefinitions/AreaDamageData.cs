using UnityEngine;

namespace _Game.Towers
{
    [CreateAssetMenu(fileName = "T_AreaDamage_0", menuName = "_Game/Towers/Specifics/AreaDamage", order = 1)]
    public class AreaDamageData : AbstractSpecificTowerData
    {
        [SerializeField] private float range = 1f;
        [SerializeField] private int damage = 10;
        [SerializeField] private float attackRate = 1f;

        public override float Range => range;
        public int Damage => damage;
        public float AttackRate => attackRate;
    }
}