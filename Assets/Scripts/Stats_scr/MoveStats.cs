using UnityEngine;

namespace TSwap.Stats
{
    [CreateAssetMenu(fileName = "New Movement Stats", menuName = "Move Stats")]
    public class MoveStats : ScriptableObject
    {
        [SerializeField] float speed;
        [SerializeField] float jumpHeight;
        [SerializeField] float jumpHaltSpeed = 0.5f;
        [SerializeField] float checkerRadius;
        [SerializeField] LayerMask groundMask;

        public float Speed => speed;
        public float JumpHeight => jumpHeight;
        public float JumpHaltSpeed => jumpHaltSpeed;
        public float CheckerRadius => checkerRadius;
        public LayerMask GroundMask => groundMask;
    }
}