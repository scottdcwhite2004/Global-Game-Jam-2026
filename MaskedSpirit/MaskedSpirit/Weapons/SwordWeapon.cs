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

            mDamage = 2.5f;
            fireRate = 1.5f;
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
