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
    enum facingDirection
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }

    enum playerState
    { 
        IDLE,
        WALKING,
    }


    internal class Player : PhysicsObject
    {

        facingDirection mFacingDirection = facingDirection.DOWN;
        float mSpeed = 5f;
        public Rectangle mSourceRectangle;
        public Texture2D mCurrentMaskSprite;
        Texture2D mMaskUp;
        Texture2D mMaskDown;
        Texture2D mMaskLeft;
        Texture2D mMaskRight;
        float mCurrentXP = 0f;
        int mCurrentLevel = 1;
        float mXPToNextLevel = 100f;

        public Player(Vector2 pPosition, bool pIsGravityEnable, Vector2 pAcceleration) : base(pPosition, pIsGravityEnable, pAcceleration)
        {
            mSourceRectangle = new Rectangle((int)pPosition.X, (int)pPosition.Y, 64, 64);
            mMaskUp = Core.Content.Load<Texture2D>("Mask_Back");
            mMaskDown = Core.Content.Load<Texture2D>("Mask");
            mMaskLeft = Core.Content.Load<Texture2D>("Mask_Left");
            mMaskRight = Core.Content.Load<Texture2D>("Mask_Right");
        }

        public override void Update(float pDeltaTime)
        {
            mSourceRectangle.Location = GetPosition().ToPoint();
            setMaskSprite();
            base.Update(pDeltaTime);
        }

        public void ResetVelocity()
        {
            SetVelocity(Vector2.Zero);
        }

        public void move(Vector2 pDirection)
        {
            Vector2 normalizedDirection = Vector2.Normalize(pDirection);
            SetPosition(GetPosition() + normalizedDirection * mSpeed);
        }

        public void setFacingDirection(facingDirection pDirection)
        {
            mFacingDirection = pDirection;
        }

        public void setMaskSprite()
        {
            switch (mFacingDirection)
            {
                case facingDirection.UP:
                    mCurrentMaskSprite = mMaskUp;
                    break;
                case facingDirection.DOWN:
                    mCurrentMaskSprite = mMaskDown;
                    break;
                case facingDirection.LEFT:
                    mCurrentMaskSprite = mMaskLeft;
                    break;
                case facingDirection.RIGHT:
                    mCurrentMaskSprite = mMaskRight;
                    break;
            }
        }

        public void AddXP(float pXPAmount)
        {
            mCurrentXP += pXPAmount;
            if (mCurrentXP >= mXPToNextLevel)
            {
                LevelUp();
            }
        }

        public void LevelUp()
        {
            mCurrentLevel++;
            mCurrentXP = mCurrentXP - mXPToNextLevel;
            mXPToNextLevel *= 1.5f; // Increase XP needed for next level
            // Additional level-up logic (e.g., increase stats) can be added here
        }

        public int GetCurrentLevel()
        {
            return mCurrentLevel;
        }

        public float GetCurrentXP()
        {
            return mCurrentXP;
        }

    }
}
