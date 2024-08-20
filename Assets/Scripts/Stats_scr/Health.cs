using UnityEngine;
using System;

namespace TSwap.Stats
{
    [Serializable]
    public class Health
    {
        [SerializeField] int maxHealth = 3;

        public int CurrentHealth { get; private set; }

        public Health()
        {
            CurrentHealth = maxHealth;
        }

        public int TakeDamage()
        {
            CurrentHealth =  Mathf.Clamp(CurrentHealth - 1, 0, maxHealth);
            return CurrentHealth;
        }
    }
}