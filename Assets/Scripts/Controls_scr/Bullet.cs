using UnityEngine;
using Yee.Utility;

namespace TSwap.Controls
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] float speed;
        [SerializeField] float enqueueOffset = 0.1f;

        Pooler pooler;
        Rigidbody rb;

        public int Direction { get; set; }

        private void Awake()
        {
            pooler = GetComponentInParent<Pooler>();
            rb = GetComponent<Rigidbody>();
        }

        private void OnDisable()
        {
            rb.velocity = Vector3.zero;
            gameObject.SetActive(false);
            pooler.Enqueue(gameObject);
            transform.position = pooler.transform.position;
            transform.rotation = Quaternion.identity;
        }

        private void FixedUpdate()
        {
            rb.velocity = transform.right * speed;

            CheckIfInCameraBound();
        }

        private void CheckIfInCameraBound()
        {
            Vector3 checkPosition = Camera.main.WorldToViewportPoint(transform.position);
            if (checkPosition.x > 1 + enqueueOffset || checkPosition.x < -enqueueOffset)
            {
                OnDisable();
            }
        }
    }
}
