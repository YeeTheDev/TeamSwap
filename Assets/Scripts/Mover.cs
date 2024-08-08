using UnityEngine;

namespace TSwap.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class Mover : MonoBehaviour
    {
        [SerializeField] float speed;
        [Range(-1, 1)] [SerializeField] int direction = 1;

        Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void Move(Vector3 inputDirection)
        {
            Vector3 velocity = inputDirection.normalized * direction;
            velocity.y = rb.velocity.y;
            rb.MovePosition(transform.position + velocity * speed * Time.fixedDeltaTime);
        }
    }
}