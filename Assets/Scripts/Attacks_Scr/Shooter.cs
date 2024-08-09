using UnityEngine;
using Yee.Utility;
using TSwap.Stats;
using TSwap.Data;
using System;

namespace TSwap.Attacks
{
    public class Shooter : MonoBehaviour
    {
        public Action<ShootingData> OnShoot;

        [SerializeField] ShootStats weaponStats;
        [SerializeField] ShootStats transferStats;
        [SerializeField] Transform muzzle;
        [SerializeField] Pooler bulletPooler;
        [SerializeField] Pooler transferPooler;

        ShootingData weaponData;
        ShootingData transferData;

        public int Direction => muzzle.position.x < muzzle.parent.position.x ? -1 : 1;

        private void Start()
        {
            weaponData = new ShootingData(bulletPooler, weaponStats, false);
            transferData = new ShootingData(transferPooler, transferStats, true);
        }

        public void TryShoot(bool shootingTransfer)
        {
            ShootingData dataToUse = shootingTransfer ? transferData : weaponData;
            if (Time.timeSinceLevelLoad >= dataToUse.reloadTimer) { Shoot(dataToUse); }
        }

        private void Shoot(ShootingData data)
        {
            if (Time.timeSinceLevelLoad > data.fireRateTimer && data.pool.TryGetObject(out GameObject bullet))
            {
                data.fireRateTimer = Time.timeSinceLevelLoad + data.stats.FireRate;

                bullet.transform.position = muzzle.position;
                bullet.transform.rotation = Direction < 0 ? Quaternion.Euler(0, 180, 0) : bullet.transform.rotation;
                bullet.SetActive(true);

                UseAmmo(data);
            }
        }

        private void UseAmmo(ShootingData data)
        {
            data.ammo -= 1;
            OnShoot?.Invoke(data);

            if (data.ammo <= 0)
            {
                data.ammo = data.stats.Ammo;
                data.reloadTimer = Time.timeSinceLevelLoad + data.stats.ReloadTime;
            }
        }
    }
}