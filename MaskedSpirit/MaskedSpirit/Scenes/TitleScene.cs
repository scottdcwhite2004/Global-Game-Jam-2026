using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ScottGameLibrary;
using ScottGameLibrary.Scenes;
using MonoGameGum;
using Gum.Forms.Controls;
using MonoGameGum.GueDeriving;
using System;


namespace MaskedSpirit.Scenes
{
    internal class TitleScene : Scene
    {
    
        private Texture2D mTitleImage;
        private Panel _titleScreenButtonsPanel;


        public override void Draw(GameTime gameTime)
        {
            Core.GraphicsDevice.Clear(Color.Black);
            Core.SpriteBatch.Begin();
            Core.SpriteBatch.Draw(mTitleImage, new Rectangle(0, 0, Core.GraphicsDevice.Viewport.Width, Core.GraphicsDevice.Viewport.Height), Color.White);
            if(_titleScreenButtonsPanel.IsVisible)
            {



            }
            Core.SpriteBatch.End();
            GumService.Default.Draw();
            base.Draw(gameTime);
        }

        public override void Initialize()
        {
            base.Initialize();
            InitializeUI();
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
            GumService.Default.Update(gameTime);
        }

        private void CreateTitlePanel()
        {
            _titleScreenButtonsPanel = new Panel();
            _titleScreenButtonsPanel.Dock(Gum.Wireframe.Dock.Fill);
            _titleScreenButtonsPanel.AddToRoot();

            var startButton = new Button();
            startButton.Anchor(Gum.Wireframe.Anchor.BottomLeft);
            startButton.X = 50;
            startButton.Y = -12;
            startButton.Width = 70;
            startButton.Text = "Start";
            startButton.Click += HandleStartClicked;
            _titleScreenButtonsPanel.AddChild(startButton);

            var exitButton = new Button();
            exitButton.Anchor(Gum.Wireframe.Anchor.BottomRight);
            exitButton.X = -50;
            exitButton.Y = -12;
            exitButton.Width = 70;
            exitButton.Text = "Exit";
            exitButton.Click += HandleExitClicked;
            _titleScreenButtonsPanel.AddChild(exitButton);
        }

        private void HandleStartClicked(object sender, EventArgs e)
        {
            Core.ChangeScene(new TestScene());
        }

        private void HandleExitClicked(object sender, EventArgs e)
        {
            
        }

        private void InitializeUI()
        {
            GumService.Default.Root.Children.Clear();
            CreateTitlePanel();
        }
    }
}
