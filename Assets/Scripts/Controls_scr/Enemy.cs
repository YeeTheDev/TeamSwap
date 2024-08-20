using System.Collections;
using TSwap.Animations;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float attackRange;
    [SerializeField] float attackTime = 1f;
    [SerializeField] int maxHealth;
    [SerializeField] float stunTime = 0.5f;

    bool attacking;
    bool takingDamage;
    Coroutine takeDamage;
    int currentHealth;
    Transform rightTarget;
    Transform leftTarget;
    Rigidbody rb;
    Animator animator;
    PlayerAnimator playerAnimator;

    private Transform CurrentTarget { get; set; }
    private float SqrAttackRange => attackRange * attackRange;
    public void SetTargetToLeft() { CurrentTarget = leftTarget; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerAnimator = GetComponent<PlayerAnimator>();

        rightTarget = GameObject.FindGameObjectWithTag("Right Player").transform;
        leftTarget = GameObject.FindGameObjectWithTag("Left Player").transform;
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;
        CurrentTarget = rightTarget;
    }

    private void FixedUpdate()
    {
        if (takingDamage || attacking) { return; }

        if ((transform.position - CurrentTarget.position).sqrMagnitude <= SqrAttackRange)
        {
            StartCoroutine(Attack());
            rb.velocity = Vector3.zero;

            return;
        }

        Vector3 direction = Vector3.zero;
        direction.x = CurrentTarget.position.x - transform.position.x;
        playerAnimator.TryFlip(Mathf.Sign(direction.x));
        rb.MovePosition(transform.position + direction.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    private IEnumerator Attack()
    {
        attacking = true;

        rb.velocity = Vector3.zero;
        Debug.Log("Attack!");
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(attackTime);

        attacking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player Bullet"))
        {
            other.gameObject.SetActive(false);
            if (takeDamage != null) { StopCoroutine(takeDamage); }
            takeDamage = StartCoroutine(TakeDamage());
        }

        if (other.CompareTag("Transfer Bullet"))
        {
            other.gameObject.SetActive(false);

            CurrentTarget = CurrentTarget == rightTarget ? leftTarget : rightTarget;

            Vector3 mirrorPosition = transform.position;
            mirrorPosition.z *= -1;
            transform.position = mirrorPosition;
        }
    }

    private IEnumerator TakeDamage()
    {
        takingDamage = true;

        rb.velocity = Vector3.zero;

        currentHealth--;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            yield break;
        }

        animator.SetTrigger("TakeDamage");
        yield return new WaitForSeconds(stunTime);

        takingDamage = false;
    }
}
