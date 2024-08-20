using UnityEngine;
using UnityEngine.Events;

public class InteractableTrigger : MonoBehaviour
{
    [SerializeField] string tagThatTrigger = "Right Player";
    [SerializeField] bool destroysTrigger = true;
    [SerializeField] UnityEvent eventToTrigger;
    [SerializeField] EventTrigger[] eventsToDestroy;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(tagThatTrigger) && Input.GetButtonDown("Interact"))
        {
            eventToTrigger.Invoke();

            DestroyEvents();
        }
    }

    private void DestroyEvents()
    {
        if (destroysTrigger) { Destroy(this); }

        foreach (EventTrigger trigger in eventsToDestroy)
        {
            Destroy(trigger.gameObject);
        }
    }
}
