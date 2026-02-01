using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MaskedSpirit.Enemies;
using MaskedSpirit.Objects;
using MaskedSpirit.UI;
using MaskedSpirit.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ScottGameLibrary;
using ScottGameLibrary.Scenes;

namespace MaskedSpirit.Scenes
{
    internal class TestScene : Scene
    {
        private Player mPlayer;
        List<XP_Pickup> xpPickups = new List<XP_Pickup>();
        SpriteFont mDefaultFont;
        List<Projectile> mProjectiles = new List<Projectile>();
        Texture2D mInkProjectileSprite;
        Texture2D mGobletProjectileSprite;
        Texture2D mCandleProjectileSprite;
        Texture2D mRoseProjectileSprite;
        Texture2D mSwordProjectileSprite;
        Texture2D mSkullProjectileSprite;
        Texture2D mEnemy1Sprite;
        Texture2D mEnemy2Sprite;
        Texture2D mBackgroundTexture;
        ProgressBar mXpBar;
        public float secondsElapsed = 0f;
        public int minutesElapsed = 0;
        EnemySpawner mSpawner = new EnemySpawner();

        public override void Draw(GameTime gameTime)
        {
            Core.GraphicsDevice.Clear(Color.CornflowerBlue);
            Core.SpriteBatch.Begin(samplerState: SamplerState.PointWrap);
            Core.SpriteBatch.Draw(mBackgroundTexture, new Rectangle(0, 0, 1920, 1080), Color.White);
            Core.SpriteBatch.End();
            Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
            mPlayer.Draw(Core.SpriteBatch);
            float textWidth = mDefaultFont.MeasureString("Level " + mPlayer.GetCurrentLevel().ToString()).X;
            Core.SpriteBatch.DrawString(mDefaultFont, "Level " + mPlayer.GetCurrentLevel().ToString(), new Vector2(960 - textWidth/2, 30), Color.White);
            string timerText = string.Format("{0}:{1:00}", minutesElapsed, (int)secondsElapsed);
            Core.SpriteBatch.DrawString(mDefaultFont, timerText, new Vector2(10, 20), Color.White);
            foreach (XP_Pickup xp in xpPickups)
            {
                if(xp.isCollected)
                {
                    continue;
                }
                Core.SpriteBatch.Draw(xp.mTexture, xp.mCollisionRectangle, Color.White);
            }
            foreach(Projectile p in mProjectiles)
            {
                if(!p.isActive)
                {
                    continue;
                }
                Core.SpriteBatch.Draw(p.mProjectileSprite, p.mCollisionRectangle, Color.White);
            }
            foreach (Enemy e in mSpawner.GetEnemies())
            {
                if(!e.isAlive)
                {
                    continue;
                }
                switch(e.GetEnemyType())
                {
                    case EnemyType.CoatStand:
                        Core.SpriteBatch.Draw(mEnemy2Sprite, e.getRectangle(), e.mSpriteColor);
                        break;
                    case EnemyType.CostumeHolder:
                        Core.SpriteBatch.Draw(mEnemy1Sprite, e.getRectangle(), e.mSpriteColor);
                        break;
                }
            }
            mXpBar.Draw(Core.SpriteBatch);
            Core.SpriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Initialize()
        {
            base.Initialize();
            mSpawner.OnEnemyDeathDropXP = (Vector2 pos) =>
            {
                xpPickups.Add(new XP_Pickup(pos));
            };
            mPlayer = new Player(new Vector2(100, 100), false, Vector2.Zero);
            mXpBar = new ProgressBar(new Rectangle(10, 0, 1900, 30), Color.Green, Color.Black);

        }

        public override void LoadContent()
        {
            mInkProjectileSprite = Core.Content.Load<Texture2D>("Ink-Projectile");
            mGobletProjectileSprite = Core.Content.Load<Texture2D>("Goblet-Projectile");
            mCandleProjectileSprite = Core.Content.Load<Texture2D>("Candle-Projectile");
            mRoseProjectileSprite = Core.Content.Load<Texture2D>("Rose-Projectile");
            mSwordProjectileSprite = Core.Content.Load<Texture2D>("Sword-Projectile");
            mSkullProjectileSprite = Core.Content.Load<Texture2D>("Skull-Projectile");
            mDefaultFont = Core.Content.Load<SpriteFont>("Default");
            mEnemy1Sprite = Core.Content.Load<Texture2D>("Enemy-1");
            mEnemy2Sprite = Core.Content.Load<Texture2D>("Enemy-2");
            mBackgroundTexture = Core.Content.Load<Texture2D>("Background");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (mPlayer.isPlayerAlive() == false)
            {
                Core.ChangeScene(new GameOverScene(minutesElapsed,(int)secondsElapsed));
            }
            else
            {
                base.Update(gameTime);
                float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                mSpawner.Update(deltaTime, mPlayer.mPosition);
                Timer(gameTime);
                mPlayer.Update(deltaTime);
                HandleKeyboardInput();
                // Clamp player position to screen bounds
                var playerRect = mPlayer.mSourceRectangle;
                var position = mPlayer.mPosition;

                // Calculate min/max positions based on player size
                float minX = 0;
                float minY = 0;
                float maxX = 1920 - playerRect.Width;
                float maxY = 1080 - playerRect.Height;

                // Clamp position
                position.X = Math.Clamp(position.X, minX, maxX);
                position.Y = Math.Clamp(position.Y, minY, maxY);

                // Update player position
                mPlayer.SetPosition(position);
                foreach (XP_Pickup xp in xpPickups)
                {
                    if (xp.isCollected)
                    {
                        continue;
                    }
                    if (mPlayer.mSourceRectangle.Intersects(xp.mCollisionRectangle))
                    {
                        mPlayer.AddXP(xp.mXPAmount);
                        xp.isCollected = true;
                        mXpBar.SetProgress(mPlayer.GetLevelProgress());
                    }
                }
                foreach (Projectile p in mProjectiles)
                {
                    if (!p.isActive)
                    {
                        continue;
                    }
                    p.Update(deltaTime);
                }
                foreach (Enemy e in mSpawner.GetEnemies())
                {
                    foreach (Projectile p in mProjectiles)
                    {
                        if (!p.isActive)
                        {
                            continue;
                        }
                        p.EnemyCollisionCheck(e);
                    }
                    if (e.isAlive)
                    {
                        mPlayer.enemyCollisionCheck(e);
                    }
                }

                Weapon w = mPlayer.mEquippedWeapon;
                        w.Update(deltaTime);
                        if (w.canFire)
                        {
                            switch (w.type)
                            {
                                case WeaponType.INK:
                                    Projectile newProjectile = new Projectile(mPlayer.mPosition, false, mPlayer.GetForwardVector(), mInkProjectileSprite, 2.0f, w.GetDamage(), 100.0f);
                                    mProjectiles.Add(newProjectile);
                                    w.canFire = false;
                                    break;
                                case WeaponType.GOBLET:
                                    // Implement Goblet firing logic
                                    newProjectile = new Projectile(mPlayer.mPosition, false, mPlayer.GetForwardVector(), mGobletProjectileSprite, 5.0f, w.GetDamage(), 100.0f);
                                    mProjectiles.Add(newProjectile);
                                    w.canFire = false;
                                    break;
                                case WeaponType.CANDLE:
                                    // Implement Candle firing logic
                                    newProjectile = new Projectile(mPlayer.mPosition, false, mPlayer.GetForwardVector(), mCandleProjectileSprite, 3.0f, w.GetDamage(), 100.0f);
                                    mProjectiles.Add(newProjectile);
                                    w.canFire = false;
                                    break;
                                case WeaponType.ROSE:
                                    // Implement Rose firing logic
                                    newProjectile = new Projectile(mPlayer.mPosition, false, mPlayer.GetForwardVector(), mRoseProjectileSprite, 1.5f, w.GetDamage(), 100.0f);
                                    mProjectiles.Add(newProjectile);
                                    w.canFire = false;
                                    break;
                                case WeaponType.SKULL:
                                    // Implement Skull firing logic
                                    newProjectile = new Projectile(mPlayer.mPosition, false, mPlayer.GetForwardVector(), mSkullProjectileSprite, 4.0f, w.GetDamage(), 100.0f);
                                    mProjectiles.Add(newProjectile);
                                    w.canFire = false;
                                    break;
                                case WeaponType.SWORD:
                                    // Implement Sword firing logic
                                    newProjectile = new Projectile(mPlayer.mPosition, false, mPlayer.GetForwardVector(), mSwordProjectileSprite, 1.0f, w.GetDamage(), 100.0f);
                                    mProjectiles.Add(newProjectile);
                                    w.canFire = false;
                                    break;
                            }
                        }
                }
            }

        public void HandleKeyboardInput()
        {
            if (Core.Input.Keyboard.IsKeyDown(Keys.Right))
            {
                mPlayer.move(new Vector2(1, mPlayer.GetVelocity().Y));
                mPlayer.setFacingDirection(facingDirection.RIGHT);
            }

            if (Core.Input.Keyboard.IsKeyDown(Keys.Left))
            {
                mPlayer.move(new Vector2(-1, mPlayer.GetVelocity().Y));
                mPlayer.setFacingDirection(facingDirection.LEFT);
            }

            if (Core.Input.Keyboard.IsKeyDown(Keys.Up))
            {
                mPlayer.move(new Vector2(mPlayer.GetVelocity().X, -1));
                mPlayer.setFacingDirection(facingDirection.UP);
            }

            if (Core.Input.Keyboard.IsKeyDown(Keys.Down))
            {
                mPlayer.move(new Vector2(mPlayer.GetVelocity().X, 1));
                mPlayer.setFacingDirection(facingDirection.DOWN);
            }
        }

        public void Timer(GameTime gameTime)
        {
            secondsElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (secondsElapsed >= 60f)
            {
                minutesElapsed++;
                secondsElapsed = 0f;
            }
        }



    }
}
