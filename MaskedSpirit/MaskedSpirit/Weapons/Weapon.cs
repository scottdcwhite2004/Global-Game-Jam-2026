using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskedSpirit.Weapons
{
    enum WeaponType
    {
        INK,
        CANDLE,
        GOBLET,
        ROSE,
        SKULL,
        SWORD
    }


    internal class Weapon
    {
    
        protected float fireRate = 0.5f;
        float timeSinceLastShot = 0f;
        public bool canFire = false;
        protected float mDamage = 10f;
        public WeaponType type;

        public Weapon()
        {
        

        
        }

        public virtual void Update(float pDeltaTime)
        {
            timeSinceLastShot += pDeltaTime;
            if(timeSinceLastShot >= fireRate)
            {
                canFire = true;
                Fire();
            }
        }

        public virtual void Fire()
        {
            if(!canFire)
            {
                return;
            }
            timeSinceLastShot = 0f;
        }

        public float GetDamage()
        {
            return mDamage;
        }

        public void LevelUp()
        {
            mDamage += 0.5f;
            fireRate = Math.Max(0.1f, fireRate - 0.1f);
        }

    }
}
