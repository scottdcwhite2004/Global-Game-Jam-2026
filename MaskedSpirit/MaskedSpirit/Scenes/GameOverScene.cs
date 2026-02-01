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
    internal class GameOverScene : Scene
    {


        private Texture2D mTitleImage;
        private Panel _titleScreenButtonsPanel;
        int mMinutesSurvived;
        int mSecondsSurvived;
        SpriteFont mFont;

        public GameOverScene(int minutesSurvived, int secondsSurvived)
        {
            mMinutesSurvived = minutesSurvived;
            mSecondsSurvived = secondsSurvived;
        }

        public override void Draw(GameTime gameTime)
        {
            Core.GraphicsDevice.Clear(Color.Black);
            Core.SpriteBatch.Begin();
            Core.SpriteBatch.Draw(mTitleImage, new Rectangle(0, 0, Core.GraphicsDevice.Viewport.Width, Core.GraphicsDevice.Viewport.Height), Color.White);
            if (_titleScreenButtonsPanel.IsVisible)
            {



            }
            Core.SpriteBatch.DrawString(mFont, $"You Survived: {mMinutesSurvived} Minutes and {mSecondsSurvived} Seconds", new Vector2(300, 350), Color.White);
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
            mFont = Content.Load<SpriteFont>("Default");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            GumService.Default.Update(gameTime);
        }

        private void CreateTitlePanel()
        {
            _titleScreenButtonsPanel = new Panel();
            _titleScreenButtonsPanel.Dock(Gum.Wireframe.Dock.Fill);
            _titleScreenButtonsPanel.AddToRoot();

            var startButton = new Button();
            startButton.Anchor(Gum.Wireframe.Anchor.Bottom);
            startButton.X = 0;
            startButton.Y = -50;
            startButton.Width = 300;
            startButton.Text = "Return To Main Menu";
            startButton.Click += HandleStartClicked;
            _titleScreenButtonsPanel.AddChild(startButton);

        }

        private void HandleStartClicked(object sender, EventArgs e)
        {
            Core.ChangeScene(new TitleScene());
        }

        private void InitializeUI()
        {
            GumService.Default.Root.Children.Clear();
            CreateTitlePanel();
        }

    }
}
