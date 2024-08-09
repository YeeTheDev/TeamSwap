using UnityEngine;

namespace TSwap.Stats
{
    [CreateAssetMenu(fileName = "New Shooting Stats", menuName = "Shooting Stats")]
    public class ShootStats : ScriptableObject
    {
        [SerializeField] int ammo;
        [SerializeField] float fireRate;
        [SerializeField] float reloadTime;

        public int Ammo => ammo;
        public float FireRate => fireRate;
        public float ReloadTime => reloadTime;
    }
}