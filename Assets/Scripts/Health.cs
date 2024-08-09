using UnityEngine;

namespace TSwap.Stats
{
    public class Health : MonoBehaviour
    {
        [SerializeField] int maxHealth = 3;

        int currentHealth;

        private void Awake() => currentHealth = maxHealth;

        public int TakeDamage()
        {
            currentHealth--;
            return currentHealth;
        }
    }
}