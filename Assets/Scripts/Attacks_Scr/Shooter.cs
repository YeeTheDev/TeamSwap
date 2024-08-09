using UnityEngine;
using Yee.Utility;

namespace TSwap.Attacks
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] float fireRate = 0.25f;
        [SerializeField] Transform rightMuzzle;
        [SerializeField] Transform leftMuzzle;
        [SerializeField] Pooler bulletPooler;
        [SerializeField] Pooler transferPooler;

        float fireRateTimer;
        Transform currentMuzzle;

        public int Direction => currentMuzzle.position.x < currentMuzzle.parent.position.x ? -1 : 1;

        private void Awake() => currentMuzzle = rightMuzzle;

        public void SwapMuzzles(bool inDefaultView) => currentMuzzle = inDefaultView ? rightMuzzle : leftMuzzle;

        public void Shoot()
        {
            if (Time.timeSinceLevelLoad > fireRateTimer && bulletPooler.TryGetObject(out GameObject bullet))
            {
                fireRateTimer = Time.timeSinceLevelLoad + fireRate;

                bullet.transform.position = currentMuzzle.position;
                bullet.SetActive(true);
            }
        }
    }
}