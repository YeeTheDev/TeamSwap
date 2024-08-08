using UnityEngine;

namespace TSwap.Animations
{
    public class PlayerAnimator : MonoBehaviour
    {
        [Range(-1, 0)][SerializeField] int direction;

        bool facingLeft;

        private void Awake() => facingLeft = direction < 0 ? true : false;

        public void TryFlip(float direction)
        {
            if (facingLeft && direction > 0) { Flip(); }
            else if (!facingLeft && direction < 0) { Flip(); }
        }

        private void Flip()
        {
            facingLeft = !facingLeft;

            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
        }
    }
}