using TSwap.Stats;
using UnityEngine;
using TSwap.Movement;

namespace TSwap.Collisions
{
    public class Collisioner : MonoBehaviour
    {
        [SerializeField] Vector3 damageForce;
        [SerializeField] float pushTime = 0.5f;
        [SerializeField] string damagerTag;
        [SerializeField] string itemTag;

        Health health;
        PlayerMover mover;

        private void Awake()
        {
            health = GetComponent<Health>();
            mover = GetComponent<PlayerMover>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(damagerTag))
            {
                if (health.TakeDamage() <= 0)
                {
                    Debug.Log("You lose!");
                    return;
                }

                Vector3 force = (transform.position - other.transform.position).normalized * damageForce.x;
                force.y = damageForce.y;
                mover.Push(force, pushTime);
            }
        }
    }
}