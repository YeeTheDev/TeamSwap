using TSwap.Stats;
using UnityEngine;
using TSwap.Movement;
using Yee.Math;
using System;

namespace TSwap.Collisions
{
    public class Collisioner : MonoBehaviour
    {
        public Action<int> OnTakeDamage;

        [SerializeField] DamageStats stats;

        float invulnerableTimer;
        Health health;
        PlayerMover mover;

        private void Awake()
        {
            health = GetComponent<Health>();
            mover = GetComponent<PlayerMover>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(stats.DamagerTag) && Time.timeSinceLevelLoad >= invulnerableTimer)
            {
                int currentHealth = health.TakeDamage();
                OnTakeDamage?.Invoke(currentHealth);

                if (currentHealth <= 0)
                {
                    Debug.Log("You lose!");
                    return;
                }


                invulnerableTimer = Time.timeSinceLevelLoad + stats.InvulnerabilityTime;
                Vector3 force = (transform.position - other.transform.position).normalized.Multiply(stats.DamageForce);
                force.y *= force.y < 0 ? -1 : 1;
                mover.Push(force, stats.PushTime);
            }
        }
    }
}