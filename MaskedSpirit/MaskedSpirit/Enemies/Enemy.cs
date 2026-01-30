using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MaskedSpirit.Enemies
{
    internal class Enemy
    {
        Rectangle mCollisionRectangle;
        protected float mMaxHealth = 100f;
        float mCurrentHealth;
        protected float mMovementSpeed = 2f;
        protected float mDamage = 10f;
        public Color mSpriteColor = Color.White;
        public bool isAlive = true;
        float mDamageCooldown = 1.0f;
        float mTimeSinceLastDamage = 0f;

        public Enemy(Rectangle pCollisionRectangle)
        {
            mCollisionRectangle = pCollisionRectangle;
            mCurrentHealth = mMaxHealth;
        }

        public bool isColliding(Rectangle pOtherRectangle)
        {
            return mCollisionRectangle.Intersects(pOtherRectangle);
        }

        public virtual void Update(float pDeltaTime, Vector2 playerPosition)
        {
            // Basic enemy logic can be implemented here
            mTimeSinceLastDamage += pDeltaTime;
            if(mSpriteColor == Color.Red)
            {
                // Reset color after damage flash
                mSpriteColor = Color.White;
            }
            steerTowards(playerPosition, pDeltaTime);
        }

        public void TakeDamage(float pDamage)
        {
            if(mTimeSinceLastDamage < mDamageCooldown)
            {
                return; // Still in cooldown, ignore damage
            }
            mCurrentHealth -= pDamage;
            mSpriteColor = Color.Red; // Flash red on damage
            if (mCurrentHealth <= 0)
            {
                Die();
            }
        }
        protected void Die()
        {
            isAlive = false;
            // Additional death logic can be implemented here
        }

        public void steerTowards(Vector2 pTargetPosition, float pDeltaTime)
        {
            Vector2 direction = pTargetPosition - new Vector2(mCollisionRectangle.X, mCollisionRectangle.Y);
            if (direction != Vector2.Zero)
            {
                direction.Normalize();
                Vector2 movement = direction * mMovementSpeed * pDeltaTime;
                mCollisionRectangle.X += (int)movement.X;
                mCollisionRectangle.Y += (int)movement.Y;
            }
        }

        public bool GetIsAlive()
        {
            return isAlive;
        }

        public Rectangle getRectangle()
        {
            return mCollisionRectangle;
        }
    }
}
