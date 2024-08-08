using UnityEngine;

namespace TSwap.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class Mover : MonoBehaviour
    {
        [SerializeField] float speed;
        [SerializeField] float jumpHeight;
        [SerializeField] float jumpHaltSpeed = 0.5f;
        [Range(-1, 1)] [SerializeField] int direction = 1;

        Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void Move(Vector3 inputDirection)
        {
            Vector3 velocity = inputDirection.normalized * direction * speed * Time.fixedDeltaTime;
            rb.MovePosition(transform.position + velocity);
        }

        public void Jump()
        {
            Vector3 jumpForce = rb.velocity;
            jumpForce.y = Mathf.Sqrt(jumpHeight * 2 * Physics.gravity.y * -1);

            rb.velocity = jumpForce; 
        }

        public void HaltJump()
        {
            if (rb.velocity.y > 0)
            {
                Vector3 jumpVelocity = rb.velocity * 0.5f;
                jumpVelocity.y *= jumpHaltSpeed;

                rb.velocity = jumpVelocity;
            }
        }
    }
}