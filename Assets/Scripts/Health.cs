using UnityEngine;

namespace TSwap.Stats
{
    public class Health : MonoBehaviour
    {
        [SerializeField] int maxHealth = 3;

        public int CurrentHealth { get; private set; }

        private void Awake() => CurrentHealth = maxHealth;

        public int TakeDamage()
        {
            CurrentHealth =  Mathf.Clamp(CurrentHealth - 1, 0, maxHealth);
            return CurrentHealth;
        }
    }
}