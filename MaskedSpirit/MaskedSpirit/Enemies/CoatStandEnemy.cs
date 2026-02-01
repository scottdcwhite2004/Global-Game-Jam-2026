using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MaskedSpirit.Enemies
{
    internal class CoatStandEnemy : Enemy
    {
        public CoatStandEnemy(Rectangle pCollisionRectangle) : base(pCollisionRectangle)
        {
            mDamage = 10.0f;
            mMovementSpeed = 50.0f;
            mMaxHealth = 25.0f;
            mEnemyType = EnemyType.CoatStand;
        }

        public override void Update(float pDeltaTime, Vector2 p)
        {
            base.Update(pDeltaTime,p);
        }
    }
}
