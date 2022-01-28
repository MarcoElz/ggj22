using _Game.DamageSystem;
using UnityEngine;

namespace _Game.Creatures
{
    public class AbstractCreature : MonoBehaviour, IDamageable
    {
        private int health;
        
        public void Hurt(int amount)
        {
            health -= amount;
            if (health <= 0) 
                Dead();
        }

        private void Dead()
        {
            Destroy(gameObject);
        }
    }
}