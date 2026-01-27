using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SWGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskedSpirit.Objects
{
    internal class XP_Pickup : GameObject
    {
        
        public float mXPAmount = 10f;
        Rectangle mCollisionRectangle;
        Texture2D mTexture;

        public XP_Pickup(Vector2 pPosition) : base(pPosition)
        {
            mCollisionRectangle = new Rectangle((int)pPosition.X, (int)pPosition.Y, 32, 32);
            mTexture = new Texture2D(Core.GraphicsDevice, 1, 1);
        }

        public void Collect(Player pPlayer)
        {
            pPlayer.AddXP(mXPAmount);
        }
    }
}
