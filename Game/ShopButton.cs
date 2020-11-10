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
    class ShopButton
    {

        int buttonX { get; set; }
        int buttonY { get; set; }
        public int price { get; set; }
        public Texture2D texture { get; set; }
        public Texture2D normTexture { get; set; }
        Texture2D hoverTexture { get; set; }
        public Texture2D soldTexture { get; set; }
        Rectangle rect;
        public bool clicked = false;
        public int sizeDiv = 2;
        public int index;


        public ShopButton(Texture2D texture, Texture2D hoverTexture, Texture2D soldTexture, int buttonX, int buttonY, int indx, int price)
        {
            index = indx;
            this.texture = texture;
            this.normTexture = texture;
            this.hoverTexture = hoverTexture;
            this.soldTexture = soldTexture;
            this.buttonX = buttonX;
            this.buttonY = buttonY;
            this.price = price;

            rect = new Rectangle(buttonX, buttonY, this.texture.Width / sizeDiv, this.texture.Height / sizeDiv);
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
            buttonY = delta;
            rect.Y = delta;
        }

        public void changeX(int delta)
        {
            buttonX = delta;
            rect.X = delta;
        }

        /**
         * @return true: If a player enters the button with mouse
         */
        public bool ButtonClicked(MouseState mouseState, int cash)
        {
            var mousePoint = new Point(mouseState.X, mouseState.Y);

            if (rect.Contains(mousePoint))
            {
                if(this.texture != this.soldTexture)this.texture = hoverTexture;
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if(cash < this.price) return true;
                    if (this.texture != this.soldTexture) this.texture = soldTexture;
                    return true;
                }
            }
            else
            {
                if(this.texture != this.soldTexture)this.texture = normTexture;
            }
            return false;
        }

        public void Reset()
        {
            this.texture = normTexture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(texture, new Vector2(buttonX, buttonY), null, Color.White, 0f, Vector2.Zero, 1.0f / (float)sizeDiv, SpriteEffects.None, 0f);

        }

    }

}
