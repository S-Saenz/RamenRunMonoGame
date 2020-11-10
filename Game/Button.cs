using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RamenRun
{
    class Button
    {

        int buttonX { get; set; }
        int buttonY { get; set; }
        Texture2D texture { get; set; }
        Texture2D normTexture { get; set; }
        Texture2D hoverTexture { get; set; }
        Rectangle rect;
        public bool clicked = false;
        public int sizeDiv = 4;


        public Button(Texture2D texture, Texture2D hoverTexture, int buttonX, int buttonY)
        {
            this.texture = texture;
            this.normTexture = texture;
            this.hoverTexture = hoverTexture;
            this.buttonX = buttonX;
            this.buttonY = buttonY;

            rect = new Rectangle(buttonX, buttonY, this.texture.Width/sizeDiv, this.texture.Height/sizeDiv);
        }
        

        public int ButtonX
        {
            get
            {
                return buttonX;
            }
        }

        public int ButtonY
        {
            get
            {
                return buttonY;
            }
        }

        public void changeY(int delta)
        {
            buttonY += delta;
        }

        public void changeX(int delta)
        {
            buttonX += delta;
        }

        /**
         * @return true: If a player enters the button with mouse
         */
        public bool EnterButton(MouseState mouseState)
        {
            var mousePoint = new Point(mouseState.X, mouseState.Y);

            if (rect.Contains(mousePoint))
            {
                this.texture = hoverTexture;
                return true;
            }
            else
            {
                this.texture = normTexture;
            }
            return false;
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(texture, new Vector2(buttonX, buttonY), null, Color.White, 0f, Vector2.Zero, 1.0f/(float)sizeDiv, SpriteEffects.None, 0f);
            
        }

    }
    
}
