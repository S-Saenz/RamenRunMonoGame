using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

namespace RamenRun
{
    class Driver
    {
        public int pos;
        int posAdd;
        Texture2D texture, textureUp, textureMid, textureDown, textureDeliver;
        Vector2 vec, upVec, downVec, midVec;
        private float depth;


        public Driver()
        {
            vec = new Vector2(335, 515);
            upVec = new Vector2(355, 470);
            downVec = new Vector2(350, 570);
            midVec = new Vector2(335, 515);
        }

        public void resetPos(KeyboardState keyState, KeyboardState oldKeyState,int phase)
        {
            int limit = 1;
            if (phase == 3) limit = 2;

            if (oldKeyState.IsKeyUp(Keys.Up) && keyState.IsKeyDown(Keys.Up) && pos < limit)
            {
                this.posAdd = 1;
            }
            else if (oldKeyState.IsKeyUp(Keys.Down) && keyState.IsKeyDown(Keys.Down) && pos > -1)
            {
                this.posAdd = -1;
            }
            if (keyState.IsKeyUp(Keys.Down) && keyState.IsKeyUp(Keys.Up))
            {
                this.pos += posAdd;
                this.posAdd = 0;
                if (pos == -1)
                {
                    texture = textureDown;
                    vec = downVec;
                    depth = 0.1f;

                }else if (pos == 0)
                {
                    texture = textureMid;
                    vec = midVec;
                    depth = 0.9f;
                }
                else if(pos == 1)
                {
                    texture = textureUp;
                    vec = upVec;
                    depth = 0.9f;
                }
                else
                {
                    texture = textureDeliver;
                    vec = upVec;
                    depth = 0.9f;

                }
            }

            /*
            if (!this.isFiring)
            {
                if (keyUP.isDown && this.pos < 2)
                {
                    this.posAdd = 1;
                }
                else if (keyDOWN.isDown && this.pos > 0)
                {
                    this.posAdd = -1;
                }
                if (keyUP.isUp && keyDOWN.isUp)
                {
                    this.y -= this.posAdd * 50;
                    console.log();
                    this.pos += this.posAdd;
                    this.posAdd = 0;
                }
            }
            */
        }
        public void Load(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("chefMid");
            textureUp = Content.Load<Texture2D>("chefHigh");
            textureDown = Content.Load<Texture2D>("chefLow");
            textureMid = Content.Load<Texture2D>("chefMid");
            textureDeliver = Content.Load<Texture2D>("chefDelivery");
        }


        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(texture, vec, null, Color.White, 0f, Vector2.Zero, 0.4f, SpriteEffects.None, depth);
            
        }

    }
}
