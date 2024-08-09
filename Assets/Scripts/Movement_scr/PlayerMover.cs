using UnityEngine;
using TSwap.Stats;
using TSwap.Animations;

namespace TSwap.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMover : MonoBehaviour
    {
        [Range(-1, 1)] [SerializeField] int direction = 1;
        [SerializeField] MoveStats stats;
        [SerializeField] Transform groundChecker;

        float pushTimer;
        Rigidbody rb;
        PlayerAnimator animator;

        public bool IsPushed => Time.timeSinceLevelLoad < pushTimer;
        public bool TouchingGround => Physics.CheckSphere(groundChecker.position, stats.CheckerRadius, stats.GroundMask);

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            animator = GetComponent<PlayerAnimator>();
        }

        public void Move(Vector3 inputDirection)
        {
            if (IsPushed) { return; }

            Vector3 velocity = inputDirection.normalized * direction * stats.Speed * Time.fixedDeltaTime;
            rb.MovePosition(transform.position + velocity);

            animator.TryFlip(inputDirection.x);
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

        public void Push(Vector3 force, float pushTime)
        {
            pushTimer = Time.timeSinceLevelLoad + pushTime;
            rb.AddForce(force, ForceMode.Impulse);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(groundChecker.position, stats.CheckerRadius);
        }
#endif
    }
}