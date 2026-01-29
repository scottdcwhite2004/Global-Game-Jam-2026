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
        
        public float mXPAmount = 2f;
        public Rectangle mCollisionRectangle;
        public Texture2D mTexture;
        public bool isCollected = false;

        public XP_Pickup(Vector2 pPosition) : base(pPosition)
        {
            mCollisionRectangle = new Rectangle((int)pPosition.X, (int)pPosition.Y, 32, 32);
            mTexture = Core.Content.Load<Texture2D>("XP-Orb");
        }

        public void Collect(Player pPlayer)
        {
            pPlayer.AddXP(mXPAmount);
        }


    }
}
