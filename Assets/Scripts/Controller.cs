using UnityEngine;
using TSwap.Movement;

namespace TSwap.Controls
{
    public class Controller : MonoBehaviour
    {
        Swapper swapper;
        Vector3 inputDirection;

        private Mover CurrentMover { get => swapper.CurrentMover; }

        private void Awake()
        {
            swapper = GetComponent<Swapper>();
        }

        private void Update()
        {
            if (swapper.Swapping) { return; }
            
            ReadSwapInput();
            ReadDirectionInput();

            if (Input.GetButtonDown("Jump"))
            {
                CurrentMover.Jump();
            }
            else if (Input.GetButtonUp("Jump")) { CurrentMover.HaltJump(); }
        }

        private void FixedUpdate()
        {
            CurrentMover.Move(inputDirection);
        }

        private void ReadSwapInput() { if (Input.GetButton("Swap")) { StartCoroutine(swapper.Swap()); } }

        private void ReadDirectionInput() => inputDirection = transform.right * Input.GetAxisRaw("Horizontal");
    }
}