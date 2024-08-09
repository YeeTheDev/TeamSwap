using TSwap.Controls;
using UnityEngine;

namespace TSwap.UI
{
    public class Swap_UI : MonoBehaviour
    {
        [SerializeField] Animator[] uiAnimators;
        [SerializeField] Swapper swapper;

        private void OnEnable() => swapper.OnSwap += Swap;
        private void OnDisable() => swapper.OnSwap -= Swap;

        private void Swap()
        {
            foreach (Animator animator in uiAnimators)
            {
                animator.SetTrigger("Swap");
            }
        }
    }
}