using MaskedSpirit.Enemies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskedSpirit.Objects
{
    internal class Projectile : PhysicsObject
    {

        public Rectangle mCollisionRectangle;
        public Texture2D mProjectileSprite;
        float lifetime = 2.0f;
        float mSpeed = 500f;
        float mDamage = 10f;
        float timeAlive = 0f;
        public bool isActive = true;

        public Projectile(Vector2 pPosition, bool pIsGravityEnable, Vector2 pAcceleration, Texture2D pProjectileSprite, float pLifetime, float pDamage, float pSpeed) : base(pPosition, pIsGravityEnable, pAcceleration)
        {
            mProjectileSprite = pProjectileSprite;
            mCollisionRectangle = new Rectangle((int)pPosition.X, (int)pPosition.Y, mProjectileSprite.Width, mProjectileSprite.Height);
            lifetime = pLifetime;
            mDamage = pDamage;
            mSpeed = pSpeed;
        }

        public override void Update(float pDeltaTime)
        {
            SetPosition(GetPosition() + GetAcceleration() * pDeltaTime * mSpeed);
            mCollisionRectangle.Location = GetPosition().ToPoint();
            if (!isActive)
            {
                return;
            }
            timeAlive += pDeltaTime;
            if(timeAlive >= lifetime)
            {
                isActive = false;
            }
            base.Update(pDeltaTime);
        }

        public void EnemyCollisionCheck(Enemy pEnemy)
        {
            if(pEnemy.isColliding(mCollisionRectangle))
            {
                pEnemy.TakeDamage(mDamage, true);
                isActive = false;
            }
        }
    }
}
