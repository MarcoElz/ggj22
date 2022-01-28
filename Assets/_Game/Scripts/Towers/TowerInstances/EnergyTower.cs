using UnityEngine;

namespace _Game.Towers
{
    public class EnergyTower : AbstractTower
    {

        public void Generate()
        {
            Debug.Log($"Generating {10f} energy");
        }
    }
}