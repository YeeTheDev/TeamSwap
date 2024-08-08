using UnityEngine;
using System.Collections;
using TSwap.Movement;

namespace TSwap.Controls
{
    public class Swapper : MonoBehaviour
    {
        [SerializeField] float swapTime = 1;
        [SerializeField] Mover rightPlayer;
        [SerializeField] Mover leftPlayer;

        bool inDefaultView = true;
        Animator cameraAnimator;

        public bool Swapping { get; private set; }
        public Mover CurrentMover { get; private set; }

        private void Awake()
        {
            CurrentMover = rightPlayer;
            cameraAnimator = Camera.main.transform.GetComponentInParent<Animator>();
        }

        public IEnumerator Swap()
        {
            Swapping = true;

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