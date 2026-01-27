using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaskedSpirit.Objects;
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

        public override void Draw(GameTime gameTime)
        {
            Core.GraphicsDevice.Clear(Color.CornflowerBlue);
            Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
            Core.SpriteBatch.Draw(mPlayer.mCurrentMaskSprite, mPlayer.mSourceRectangle, Color.White);
            Core.SpriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Initialize()
        {
            base.Initialize();
            mPlayer = new Player(new Vector2(100, 100), false, Vector2.Zero);
        }

        public override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            mPlayer.Update(deltaTime);
            HandleKeyboardInput();
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
