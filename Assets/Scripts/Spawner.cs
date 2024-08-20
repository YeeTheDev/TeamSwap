using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] int totalSpawns;
    [SerializeField] float timeBeforeFirstSpawn;
    [SerializeField] float timeBetweenSpawns;
    [SerializeField] bool spawnsInLeftSide;
    [SerializeField] Enemy enemyToSpawn;

    int currentSpawned;
    Animator animator;

    private void Awake() => animator = GetComponent<Animator>();

    public void StartSpawn()
    {
        animator.SetTrigger("Portal");
        InvokeRepeating("Spawn", timeBeforeFirstSpawn, timeBetweenSpawns);
    }

    private void Spawn()
    {
        Enemy enemy = Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
        if (spawnsInLeftSide) { enemy.SetTargetToLeft(); }

        currentSpawned++;

        if (currentSpawned >= totalSpawns)
        {
            CancelInvoke();
            animator.SetTrigger("Portal");
        }
    }
}
