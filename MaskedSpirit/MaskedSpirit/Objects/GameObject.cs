using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskedSpirit.Objects
{
    internal class GameObject
    {
        public Vector2 mPosition { get; private set; }

        public GameObject(Vector2 pPosition)
        {
            mPosition = pPosition;
        }

        public void SetPosition(Vector2 pPosition)
        {
            mPosition = pPosition;
        }

        public Vector2 GetPosition()
        {
            return mPosition;
        }
    }
}
