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

        Rectangle mCollisionRectangle;
        Texture2D mProjectileSprite;

        public Projectile(Vector2 pPosition, bool pIsGravityEnable, Vector2 pAcceleration, Texture2D pProjectileSprite) : base(pPosition, pIsGravityEnable, pAcceleration)
        {
            mProjectileSprite = pProjectileSprite;
            mCollisionRectangle = new Rectangle((int)pPosition.X, (int)pPosition.Y, mProjectileSprite.Width, mProjectileSprite.Height);
        }

        public override void Update(float pDeltaTime)
        {
            base.Update(pDeltaTime);
        }
    }
}
