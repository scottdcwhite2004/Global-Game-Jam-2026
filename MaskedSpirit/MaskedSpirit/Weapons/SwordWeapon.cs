using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskedSpirit.Weapons
{
    internal class SwordWeapon : Weapon
    {
        public SwordWeapon()
        {

            mDamage = 15.0f;
            fireRate = 1.0f;
            type = WeaponType.SWORD;
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
