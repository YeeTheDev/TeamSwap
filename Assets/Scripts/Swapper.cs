using System.Collections;
using UnityEngine;

public class Swapper : MonoBehaviour
{
    [SerializeField] float animationTime = 0.5f;
    [SerializeField] Mover rightPlayer;
    [SerializeField] Mover leftPlayer;

    bool swapping;
    bool inDefaultView = true;
    Animator animator;

    private void Awake() => animator = GetComponent<Animator>();

    //TODO set someplace else? Maybe?
    private void Update()
    {
        if (Input.GetButton("Swap") && !swapping) { StartCoroutine(Swap()); }
    }

    private IEnumerator Swap()
    {
        swapping = true;

        inDefaultView = !inDefaultView;
        ActivateControls(false);

        animator.SetTrigger("Swap");
        yield return new WaitForSecondsRealtime(animationTime);

        ActivateControls(true);

        swapping = false;
    }

    private void ActivateControls(bool enable)
    {
        Time.timeScale = enable ? 1 : 0;

        leftPlayer.enabled = inDefaultView ? false : enable ? true : false;
        rightPlayer.enabled = !inDefaultView ? false : enable ? true : false;
    }
}
