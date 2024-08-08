using TSwap.Attacks;
using TSwap.Movement;
using UnityEngine;

namespace TSwap.Controls
{
    [RequireComponent(typeof(Swapper))]
    [RequireComponent(typeof(Shooter))]
    public class Controller : MonoBehaviour
    {
        Swapper swapper;
        Shooter shooter;
        Vector3 inputDirection;

        private PlayerMover CurrentMover { get => swapper.CurrentMover; }

        private void Awake()
        {
            swapper = GetComponent<Swapper>();
            shooter = GetComponent<Shooter>();
        }

        private void Update()
        {
            if (swapper.Swapping) { return; }

            SwapInput();
            DirectionInput();
            JumpInput();
            ShootInput();
        }

        private void FixedUpdate()
        {
            CurrentMover.Move(inputDirection);
        }

        private void SwapInput()
        {
            if (CurrentMover.TouchingGround && Input.GetButton("Swap"))
            {
                StartCoroutine(swapper.Swap());
                shooter.SwapMuzzles(swapper.GetDefaultView);
            }
        }

        private void DirectionInput() => inputDirection = transform.right * Input.GetAxisRaw("Horizontal");

        private void JumpInput()
        {
            if (CurrentMover.TouchingGround && Input.GetButtonDown("Jump")) { CurrentMover.Jump(); }
            else if (Input.GetButtonUp("Jump")) { CurrentMover.HaltJump(); }
        }

        private void ShootInput()
        {
            if (Input.GetButtonDown("Shoot")) { shooter.Shoot(); }
        }
    }
}