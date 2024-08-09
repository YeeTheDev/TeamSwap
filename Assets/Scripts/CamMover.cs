using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TSwap.Controls
{
    public class CamMover : MonoBehaviour
    {
        [SerializeField] Transform rightTarget;
        [SerializeField] Transform leftTarget;
        [SerializeField] Swapper swapper;

        bool swapping;
        Transform currentTarget;

        private void Awake() => currentTarget = rightTarget;
        private void OnEnable() => swapper.OnSwap += SwapTarget;
        private void OnDisable() => swapper.OnSwap += SwapTarget;

        private void LateUpdate() => FollowTarget();

        private void FollowTarget()
        {
            if (swapping) { return; } 

            Vector3 followPosition = transform.position;
            followPosition.x = currentTarget.position.x;
            transform.position = followPosition;
        }

        private void SwapTarget()
        {
            currentTarget = currentTarget == rightTarget ? leftTarget : rightTarget;
            StartCoroutine(LerpToTarget());
        }

        private IEnumerator LerpToTarget()
        {
            swapping = true;

            float timer = 0;
            float initialXPoint = transform.position.x;
            while (timer < 1)
            {
                yield return new WaitForEndOfFrame();

                timer += Time.unscaledDeltaTime;
                Vector3 followPosition = transform.position;
                followPosition.x = Mathf.Lerp(initialXPoint, currentTarget.position.x, timer);
                transform.position = followPosition;
            }

            swapping = false;
        }
    }
}