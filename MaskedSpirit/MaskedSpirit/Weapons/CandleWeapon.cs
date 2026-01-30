using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskedSpirit.Weapons
{
    internal class CandleWeapon : Weapon
    {
        public CandleWeapon()
        {

            mDamage = 5.0f;
            fireRate = 3.0f;
            type = WeaponType.CANDLE;
        }

        public override void Fire()
        {
            base.Fire();
        }

        public override void Update(float pDeltaTime)
        {
            base.Update(pDeltaTime);
        }
    }
}
