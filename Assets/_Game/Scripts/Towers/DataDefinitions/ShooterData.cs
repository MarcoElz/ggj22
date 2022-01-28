using UnityEngine;

namespace _Game.Towers
{
    [CreateAssetMenu(fileName = "T_Shooter_0", menuName = "_Game/Towers/Specifics/Shooter", order = 1)]
    public class ShooterData : AbstractSpecificTowerData
    {
        [SerializeField] private float range = 1f;
        [SerializeField] private int damage = 10;
        [SerializeField] private float attackRate = 1f;
        [SerializeField] private Bullet bulletPrefab = default;

        public float Range => range;
        public int Damage => damage;
        public float AttackRate => attackRate;

        public Bullet BulletPrefab => bulletPrefab;
    }
}