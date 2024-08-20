using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour
{
    [SerializeField] string tagThatTrigger = "Player";
    [SerializeField] bool destroysTrigger = true;
    [SerializeField] UnityEvent eventToTrigger;
    [SerializeField] EventTrigger[] eventsToDestroy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagThatTrigger))
        {
            eventToTrigger.Invoke();

            DestroyEvents();
        }
    }

    private void DestroyEvents()
    {
        if (destroysTrigger) { Destroy(gameObject); }

        foreach (EventTrigger trigger in eventsToDestroy)
        {
            Destroy(trigger.gameObject);
        }
    }
}
