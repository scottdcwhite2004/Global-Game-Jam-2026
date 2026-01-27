using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskedSpirit.Objects
{
    internal class PhysicsObject : GameObject
    {
        private const float GRAVITY = 9.81f;
        Vector2 mVelocity;
        Vector2 mAcceleration;
        bool mIsGravityEnabled;

        public PhysicsObject(Vector2 pPosition, bool pIsGravityEnable, Vector2 pAcceleration) : base(pPosition)
        {
            mIsGravityEnabled = pIsGravityEnable;
            mAcceleration = pAcceleration;
        }

        public virtual void Update(float pDeltaTime)
        {
            mVelocity += mAcceleration * pDeltaTime;
            if(mIsGravityEnabled)
            {
                mVelocity += new Vector2(0, GRAVITY) * pDeltaTime;
            }
        }

        public void SetVelocity(Vector2 pVelocity)
        {
            mVelocity = pVelocity;
        }

        public Vector2 GetVelocity()
        {
            return mVelocity;
        }

        public void SetAcceleration(Vector2 pAcceleration)
        {
            mAcceleration = pAcceleration;
        }

        public Vector2 GetAcceleration()
        {
            return mAcceleration;
        }

        public Vector2 AddForce(Vector2 pForce)
        {
            mAcceleration += pForce;
            return mAcceleration;
        }
    }
}
