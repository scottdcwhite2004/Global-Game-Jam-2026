using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskedSpirit.Weapons
{
    internal class GobletWeapon : Weapon
    {
        public GobletWeapon()
        {

            mDamage = 50.0f;
            fireRate = 10.0f;
            type = WeaponType.GOBLET;
        
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
