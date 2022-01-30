using UnityEngine;

namespace _Game.Towers
{
    public class TowerHead : MonoBehaviour
    {
        [SerializeField] private Transform bulletOrigin = default;

        public Transform BulletOrigin => bulletOrigin;
    }
}