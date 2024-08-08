using UnityEngine;

namespace TSwap.Controls
{
    public class Controller : MonoBehaviour
    {
        Swapper swapper;

        Vector3 inputDirection;

        private void Awake()
        {
            swapper = GetComponent<Swapper>();
        }

        private void Update()
        {
            if (swapper.Swapping) { return; }
            
            ReadSwapInput();
            ReadDirectionInput();
        }

        private void FixedUpdate()
        {
            swapper.CurrentMover.Move(inputDirection);
        }

        private void ReadSwapInput() { if (Input.GetButton("Swap")) { StartCoroutine(swapper.Swap()); } }

        private void ReadDirectionInput() => inputDirection = transform.right * Input.GetAxisRaw("Horizontal");
    }
}