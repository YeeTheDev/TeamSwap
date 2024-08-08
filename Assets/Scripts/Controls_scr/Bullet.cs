using TSwap.Attacks;
using UnityEngine;
using Yee.Utility;

namespace TSwap.Controls
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] float speed;

        Pooler pooler;
        Rigidbody rb;
        Shooter shooter;

        public int Direction { get; set; }

        private void Awake()
        {
            pooler = GetComponentInParent<Pooler>();
            rb = GetComponent<Rigidbody>();

            shooter = GameObject.FindGameObjectWithTag("Player").GetComponent<Shooter>();
        }

        private void FixedUpdate()
        {
            rb.velocity = transform.right * speed * shooter.Direction;
        }

        private void OnBecameInvisible() => pooler.Enqueue(gameObject);
    }
}
