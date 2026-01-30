using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskedSpirit.Weapons
{
    internal class RoseWeapon : Weapon
    {
        public RoseWeapon()
        {

            mDamage = 1.0f;
            fireRate = 0.2f;
            type = WeaponType.ROSE;
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
