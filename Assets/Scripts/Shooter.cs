using UnityEngine;
using Yee.Utility;

namespace TSwap.Attacks
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] Transform rightMuzzle;
        [SerializeField] Transform leftMuzzle;
        [SerializeField] Pooler pooler;

        Transform currentMuzzle;

        public int Direction => currentMuzzle.position.x < currentMuzzle.parent.position.x ? -1 : 1;

        private void Awake() => currentMuzzle = rightMuzzle;

        public void SwapMuzzles(bool inDefaultView) => currentMuzzle = inDefaultView ? rightMuzzle : leftMuzzle;

        public void Shoot()
        {
            if (pooler.TryGetObject(out GameObject bullet))
            {
                bullet.transform.position = currentMuzzle.position;
                bullet.SetActive(true);
            }
        }
    }
}