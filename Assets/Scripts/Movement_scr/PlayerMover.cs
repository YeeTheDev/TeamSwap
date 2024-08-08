using UnityEngine;
using TSwap.Stats;

namespace TSwap.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMover : MonoBehaviour
    {
        [Range(-1, 1)] [SerializeField] int direction = 1;
        [SerializeField] MoveStats stats;
        [SerializeField] Transform groundChecker;

        Rigidbody rb;

        public bool TouchingGround => Physics.CheckSphere(groundChecker.position, stats.CheckerRadius, stats.GroundMask);

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void Move(Vector3 inputDirection)
        {
            Vector3 velocity = inputDirection.normalized * direction * stats.Speed * Time.fixedDeltaTime;
            rb.MovePosition(transform.position + velocity);
        }

        public void Jump()
        {
            Vector3 jumpForce = rb.velocity;
            jumpForce.y = Mathf.Sqrt(stats.JumpHeight * 2 * Physics.gravity.y * -1);

            rb.velocity = jumpForce;
        }

        public void HaltJump()
        {
            if (rb.velocity.y > 0)
            {
                Vector3 jumpVelocity = rb.velocity * 0.5f;
                jumpVelocity.y *= stats.JumpHaltSpeed;

                rb.velocity = jumpVelocity;
            }
        }

        public void Stop() => rb.velocity = Vector3.zero;

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(groundChecker.position, stats.CheckerRadius);
        }
#endif
    }
}