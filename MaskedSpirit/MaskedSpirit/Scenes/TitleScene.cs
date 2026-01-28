using MaskedSpirit.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SWGame;
using SWGame.Input;
using SWGame.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskedSpirit.Scenes
{
    internal class TitleScene : Scene
    {
    
        private Texture2D mTitleImage;

        public override void Draw(GameTime gameTime)
        {
            Core.GraphicsDevice.Clear(Color.Black);
            Core.SpriteBatch.Begin();
            Core.SpriteBatch.Draw(mTitleImage, new Rectangle(0, 0, Core.GraphicsDevice.Viewport.Width, Core.GraphicsDevice.Viewport.Height), Color.White);
            Core.SpriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void LoadContent()
        {
            mTitleImage = Content.Load<Texture2D>("TitleScreenBackground");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if(Core.Input.Keyboard.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Enter))
            {
                Core.ChangeScene(new TestScene());
            }
            base.Update(gameTime);
        }
    }
}
