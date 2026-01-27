using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SWGame;
using MaskedSpirit.Scenes;

namespace MaskedSpirit
{
    public class Game1 : Core
    {

        public Game1() : base("Mask Theatre", 1920, 1080, true)
        {

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            ChangeScene(new TitleScene());
        }

        protected override void LoadContent()
        {
        }
    }
}
