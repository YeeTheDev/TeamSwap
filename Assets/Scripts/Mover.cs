using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Mover : MonoBehaviour
{
    [SerializeField] float speed;
    [Range(-1, 1)][SerializeField] int direction = 1;

    Vector3 inputDirection;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        inputDirection = transform.right * Input.GetAxisRaw("Horizontal") * direction;

    }

    private void FixedUpdate()
    {
        Vector3 velocity = inputDirection.normalized;
        velocity.y = rb.velocity.y;
        rb.MovePosition(transform.position + velocity * speed * Time.fixedDeltaTime);
    }
}
