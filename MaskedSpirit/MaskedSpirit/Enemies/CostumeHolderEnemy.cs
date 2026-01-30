using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MaskedSpirit.Enemies
{
    internal class CostumeHolderEnemy : Enemy
    {
        public CostumeHolderEnemy(Rectangle pCollisionRectangle) : base(pCollisionRectangle)
        {

            mDamage = 15f;
            mMovementSpeed = 25.0f;
            mMaxHealth = 50.0f;

        }

        public override void Update(float pDeltaTime, Vector2 p)
        {
            base.Update(pDeltaTime,p);
        }
    }
}
