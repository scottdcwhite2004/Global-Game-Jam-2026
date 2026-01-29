using MaskedSpirit.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SWGame;
using MaskedSpirit.UI;

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

        ProgressBar mHealthBar;
        facingDirection mFacingDirection = facingDirection.DOWN;
        float mSpeed = 5f;
        public Rectangle mSourceRectangle;
        public Texture2D mCurrentMaskSprite;
        Rectangle mHealthBarRect;
        Texture2D mMaskUp;
        Texture2D mMaskDown;
        Texture2D mMaskLeft;
        Texture2D mMaskRight;
        float mCurrentXP = 0f;
        int mCurrentLevel = 1;
        float mXPToNextLevel = 10f;
        float mLevelProgress;
        public Weapon[] mEquippedWeapons = new Weapon[4];
        float mMaxHealth = 100f;
        float mCurrentHealth = 100f;


        public Player(Vector2 pPosition, bool pIsGravityEnable, Vector2 pAcceleration) : base(pPosition, pIsGravityEnable, pAcceleration)
        {
            mSourceRectangle = new Rectangle((int)pPosition.X, (int)pPosition.Y, 64, 64);
            mMaskUp = Core.Content.Load<Texture2D>("Mask_Back");
            mMaskDown = Core.Content.Load<Texture2D>("Mask");
            mMaskLeft = Core.Content.Load<Texture2D>("Mask_Left");
            mMaskRight = Core.Content.Load<Texture2D>("Mask_Right");
            mEquippedWeapons[0] = new InkWeapon();
            mLevelProgress = mCurrentXP / mXPToNextLevel;
            mHealthBarRect = new Rectangle(mSourceRectangle.X, mSourceRectangle.Y - 12, 61, 10);
            mHealthBar = new ProgressBar(mHealthBarRect, Color.Red, Color.Black);
            mHealthBar.SetProgress(mCurrentHealth / mMaxHealth);
        }

        public override void Update(float pDeltaTime)
        {
            mSourceRectangle.Location = GetPosition().ToPoint();
            mHealthBarRect.Location = new Point(mSourceRectangle.X, mSourceRectangle.Y - 12);
            mHealthBar.UpdatePosition(mHealthBarRect);
            setMaskSprite();
            base.Update(pDeltaTime);
            foreach(Weapon w in mEquippedWeapons)
            {
                if(w != null)
                {
                    w.Update(pDeltaTime);
                }
            }
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
            mLevelProgress = mCurrentXP / mXPToNextLevel;
            if (mCurrentXP >= mXPToNextLevel)
            {
                LevelUp();
            }
        }

        public void LevelUp()
        {
            mCurrentLevel++;
            mCurrentXP = mCurrentXP - mXPToNextLevel;
            mXPToNextLevel *= 1.5f;
            mLevelProgress = mCurrentXP / mXPToNextLevel;
        }

        public int GetCurrentLevel()
        {
            return mCurrentLevel;
        }

        public float GetCurrentXP()
        {
            return mCurrentXP;
        }

        public facingDirection GetFacingDirection()
        {
            return mFacingDirection;
        }

        public Vector2 GetForwardVector()
        {
            return mFacingDirection switch
            {
                facingDirection.UP => new Vector2(0, -1),
                facingDirection.DOWN => new Vector2(0, 1),
                facingDirection.LEFT => new Vector2(-1, 0),
                facingDirection.RIGHT => new Vector2(1, 0),
                _ => Vector2.Zero,
            };
        }

        public float GetLevelProgress()
        {
            return mLevelProgress;
        }

        public void Draw(SpriteBatch pSpriteBatch)
        {
            pSpriteBatch.Draw(mCurrentMaskSprite, mSourceRectangle, Color.White);
            mHealthBar.Draw(pSpriteBatch);
        }

    }
}
