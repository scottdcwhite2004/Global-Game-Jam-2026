using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaskedSpirit.Objects;
using MaskedSpirit.UI;
using MaskedSpirit.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SWGame;
using SWGame.Scenes;

namespace MaskedSpirit.Scenes
{
    internal class TestScene : Scene
    {
        private Player mPlayer;
        List<XP_Pickup> xpPickups = new List<XP_Pickup>();
        SpriteFont mDefaultFont;
        List<Projectile> mProjectiles = new List<Projectile>();
        Texture2D mProjectileSprite;
        ProgressBar mXpBar;
        ProgressBar mHealthBar;

        public override void Draw(GameTime gameTime)
        {
            Core.GraphicsDevice.Clear(Color.CornflowerBlue);
            Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
            Core.SpriteBatch.Draw(mPlayer.mCurrentMaskSprite, mPlayer.mSourceRectangle, Color.White);
            Core.SpriteBatch.DrawString(mDefaultFont, "XP: " + mPlayer.GetCurrentXP().ToString(), new Vector2(10, 10), Color.White);
            Core.SpriteBatch.DrawString(mDefaultFont, "Level: " + mPlayer.GetCurrentLevel().ToString(), new Vector2(10, 30), Color.White);
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
            mXpBar.Draw(Core.SpriteBatch);
            mHealthBar.Draw(Core.SpriteBatch);
            Core.SpriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Initialize()
        {
            base.Initialize();
            mPlayer = new Player(new Vector2(100, 100), false, Vector2.Zero);
           xpPickups.Add(new XP_Pickup(new Vector2(200, 200)));
              xpPickups.Add(new XP_Pickup(new Vector2(300, 150)));
            xpPickups.Add(new XP_Pickup(new Vector2(400, 250)));
            xpPickups.Add(new XP_Pickup(new Vector2(500, 300)));
            mXpBar = new ProgressBar(new Rectangle(10, 50, 200, 20), Color.Green, Color.Black);
            mHealthBar = new ProgressBar(new Rectangle(10, 80, 200, 20), Color.Red, Color.Black);
        }

        public override void LoadContent()
        {
            mProjectileSprite = Content.Load<Texture2D>("Ink-Projectile");
            mDefaultFont = Core.Content.Load<SpriteFont>("Default");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            mPlayer.Update(deltaTime);
            HandleKeyboardInput();
            foreach (XP_Pickup xp in xpPickups)
            {
                if(xp.isCollected)
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
            foreach(Projectile p in mProjectiles)
            {
                if(!p.isActive)
                {
                    continue;
                }
                p.Update(deltaTime);
            }
            for (int i = 0; i < mPlayer.mEquippedWeapons.Length; i++)
            {
                Weapon w = mPlayer.mEquippedWeapons[i];
                {
                    if(w == null)
                    {
                        continue;
                    }
                    w.Update(deltaTime);
                    if (w.canFire)
                    {
                        switch (w.type)
                        {
                            case WeaponType.INK:
                                Projectile newProjectile = new Projectile(mPlayer.mPosition, false, mPlayer.GetForwardVector(), mProjectileSprite);
                                mProjectiles.Add(newProjectile);
                                w.canFire = false;
                                break;

                        }
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



    }
}
