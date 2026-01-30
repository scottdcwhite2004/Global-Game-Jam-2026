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
        
            
        
        }

        public override void Update(float pDeltaTime, Vector2 p)
        {
            base.Update(pDeltaTime,p);
        }
    }
}
