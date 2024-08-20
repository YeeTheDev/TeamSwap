using TSwap.Stats;
using UnityEngine;
using TSwap.Movement;
using Yee.Math;
using System;

namespace TSwap.Collisions.Interactions
{
    public class DamageTaker : MonoBehaviour
    {
        public Action<int> OnTakeDamage;

        [SerializeField] DamageStats stats;
        [SerializeField] Health health;

        float invulnerableTimer;
        PlayerMover mover;

        private void Awake() => mover = GetComponent<PlayerMover>();

        public void TryTakeDamage(Transform other)
        {
            if (Time.timeSinceLevelLoad >= invulnerableTimer)
            {
                int currentHealth = health.TakeDamage();
                OnTakeDamage?.Invoke(currentHealth);

                if (currentHealth <= 0)
                {
                    KillPlayer();
                    return;
                }

                invulnerableTimer = Time.timeSinceLevelLoad + stats.InvulnerabilityTime;
                Push(other);
            }
        }

        private void KillPlayer()
        {
            Debug.Log("You lose!");
            return;
        }

        private void Push(Transform other)
        {
            Vector3 force = (transform.position - other.position).normalized.Multiply(stats.DamageForce);
            force.y *= force.y < 0 ? -1 : 1;
            mover.Push(force, stats.PushTime);
        }
    }
}