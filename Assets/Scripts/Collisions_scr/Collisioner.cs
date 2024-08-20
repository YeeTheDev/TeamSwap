using TSwap.Collisions.Interactions;
using UnityEngine;

namespace TSwap.Collisions
{
    [RequireComponent(typeof(DamageTaker))]
    public class Collisioner : MonoBehaviour
    {
        [SerializeField] string meleeDamageTag;

        DamageTaker damageTaker;

        private void Awake()
        {
            damageTaker = GetComponent<DamageTaker>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag(meleeDamageTag))
                { damageTaker.TryTakeDamage(collision.transform); }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(meleeDamageTag))
            { damageTaker.TryTakeDamage(other.transform); }
        }
    }
}