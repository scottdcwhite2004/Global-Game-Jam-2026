using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskedSpirit.Weapons
{
    internal class SkullWeapon : Weapon
    {
        public SkullWeapon()
        {

            mDamage = 20.0f;
            fireRate = 2.0f;
            type = WeaponType.SKULL;
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
