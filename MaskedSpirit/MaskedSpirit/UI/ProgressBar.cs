using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ScottGameLibrary;

namespace MaskedSpirit.UI
{
    internal class ProgressBar
    {
        Texture2D mBackgroundTexture = Core.Content.Load<Texture2D>("ProgressBarBackground");
        Texture2D mForegroundTexture = Core.Content.Load<Texture2D>("ProgressBarForeground");
        Rectangle mPosition;
        float mProgress; // 0.0 to 1.0
        Color mForegroundColor;
        Color mBackgroundColor;

        public ProgressBar(Rectangle pPosition, Color pForegroundColor, Color pBackgroundColor)
        {
            mPosition = pPosition;
            mForegroundColor = pForegroundColor;
            mBackgroundColor = pBackgroundColor;
            mProgress = 0.0f;
        }

        public void SetProgress(float pProgress)
        {
            mProgress = MathHelper.Clamp(pProgress, 0.0f, 1.0f);
        }

        public void Draw(SpriteBatch pSpriteBatch)
        {
            // Draw background
            pSpriteBatch.Draw(mBackgroundTexture, mPosition, mBackgroundColor);
            // Draw foreground based on progress
            Rectangle foregroundRect = new Rectangle(mPosition.X, mPosition.Y, (int)(mPosition.Width * mProgress), mPosition.Height);
            pSpriteBatch.Draw(mForegroundTexture, foregroundRect, mForegroundColor);
        }

        public void UpdatePosition(Rectangle pNewPosition)
        {
            mPosition = pNewPosition;
        }

    }
}
