using UnityEngine;
using System.Collections;
using TSwap.Movement;
using System;

namespace TSwap.Controls
{
    public class Swapper : MonoBehaviour
    {
        public Action OnSwap;

        [SerializeField] float swapTime = 1;
        [SerializeField] PlayerMover rightPlayer;
        [SerializeField] PlayerMover leftPlayer;

        bool inDefaultView = true;
        Animator cameraAnimator;

        public bool Swapping { get; private set; }
        public PlayerMover CurrentMover { get; private set; }

        public bool GetDefaultView => inDefaultView;

        private void Awake()
        {
            CurrentMover = rightPlayer;
            cameraAnimator = Camera.main.transform.GetComponentInParent<Animator>();
        }

        public IEnumerator Swap()
        {
            Swapping = true;

            OnSwap?.Invoke();

            inDefaultView = !inDefaultView;

            SetActivePlayer(false);

            cameraAnimator.SetTrigger("Swap");
            yield return new WaitForSecondsRealtime(swapTime);

            SetActivePlayer(true);
            StopPlayers();

            CurrentMover = inDefaultView ? rightPlayer : leftPlayer;

            Swapping = false;
        }

        private void SetActivePlayer(bool enable)
        {
            Time.timeScale = enable ? 1 : 0;

            leftPlayer.enabled = inDefaultView ? false : enable ? true : false;
            rightPlayer.enabled = !inDefaultView ? false : enable ? true : false;
        }

        private void StopPlayers()
        {
            rightPlayer.Stop();
            leftPlayer.Stop();
        }
    }
}