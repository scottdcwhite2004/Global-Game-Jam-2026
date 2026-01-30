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
        private Vector2 mPosition;

        public Enemy(Rectangle pCollisionRectangle)
        {
            mCollisionRectangle = pCollisionRectangle;
            mCurrentHealth = mMaxHealth;
            mPosition = new Vector2(pCollisionRectangle.X, pCollisionRectangle.Y);
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
            mTimeSinceLastDamage = 0f;
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
            Vector2 enemyCenter = mPosition + new Vector2(mCollisionRectangle.Width / 2f, mCollisionRectangle.Height / 2f);
            Vector2 direction = pTargetPosition - enemyCenter;
            if (direction != Vector2.Zero)
            {
                direction.Normalize();
                Vector2 movement = direction * mMovementSpeed * pDeltaTime;
                mPosition += movement;
                mCollisionRectangle.X = (int)mPosition.X;
                mCollisionRectangle.Y = (int)mPosition.Y;
            }
        }

        public void ApplySeparation(List<Enemy> allEnemies, float separationDistance, float separationStrength)
        {
            Vector2 enemyCenter = new Vector2(mCollisionRectangle.X + mCollisionRectangle.Width / 2f, mCollisionRectangle.Y + mCollisionRectangle.Height / 2f);
            Vector2 repulse = Vector2.Zero;
            int neighborCount = 0;

            foreach (var other in allEnemies)
            {
                if (other == this || !other.isAlive)
                    continue;

                Vector2 otherCenter = new Vector2(other.mCollisionRectangle.X + other.mCollisionRectangle.Width / 2f, other.mCollisionRectangle.Y + other.mCollisionRectangle.Height / 2f);
                float dist = Vector2.Distance(enemyCenter, otherCenter);

                if (dist < separationDistance && dist > 0)
                {
                    Vector2 away = enemyCenter - otherCenter;
                    away.Normalize();
                    repulse += away / dist; // Stronger repulsion when closer
                    neighborCount++;
                }
            }

            if (neighborCount > 0)
            {
                repulse /= neighborCount;
                repulse *= separationStrength;
                // Apply to position
                mCollisionRectangle.X += (int)repulse.X;
                mCollisionRectangle.Y += (int)repulse.Y;
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
