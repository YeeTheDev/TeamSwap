using TSwap.Stats;
using Yee.Utility;

namespace TSwap.Data
{
    public class ShootingData
    {
        public int ammo;
        public float fireRateTimer;
        public float reloadTimer;
        public Pooler pool;
        public ShootStats stats;
        public bool isTransfer;

        public ShootingData(Pooler pool, ShootStats stats, bool isTransfer)
        {
            this.pool = pool;
            this.stats = stats;
            this.isTransfer = isTransfer;
            ammo = stats.Ammo;
        }
    }
}