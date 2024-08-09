using UnityEngine;
using Yee.Math;

namespace TSwap.Controls
{
    public class Transferable : MonoBehaviour
    {
        [SerializeField] Vector3 transferPoint;
        [SerializeField] BoxCollider box;
        [SerializeField] LayerMask obstacleMask;

        Vector3 defaultPosition;
        Animator animator;

        private Vector3 TransferPoint => transferPoint + transform.position;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            defaultPosition = transform.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Transfer Bullet"))
            {
                other.gameObject.SetActive(false);

                if (CheckIfOccupied()) { animator.SetTrigger("FailSwap"); return; }

                transform.position = transform.position == defaultPosition ? TransferPoint : defaultPosition;
            }
        }

        private bool CheckIfOccupied()
        {
            Vector3 pointToCheck = transform.position == defaultPosition ? TransferPoint : defaultPosition;
            return Physics.CheckBox(pointToCheck, box.size, Quaternion.identity, obstacleMask);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Vector3 pointToCheck = TransferPoint;
            if (Application.isPlaying && transform.position != defaultPosition) { pointToCheck = defaultPosition; }
            Gizmos.DrawWireCube(pointToCheck, box.size.Multiply(transform.localScale));
        }
#endif
    }
}