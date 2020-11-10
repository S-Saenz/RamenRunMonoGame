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
    class Bird
    {


        Vector2 pos;
        Texture2D texture;
        bool hasHit;
        bool visible;

        public Bird()
        {
            pos = new Vector2(1500, 500);
            visible = true;
        }

        public void Load(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("bird");
        }

        public void Update()
        {
            if (pos.X > -200)
            {
                pos.X -= 7;

            }
            else
            {
                pos.X = 1500;
                hasHit = false;
                this.visible = true;
            }
        }

        public void CheckCollision(Cart cart, Driver driver, AudioPlayer audioPlayer)
        {
            if (pos.X <= cart.cartPos.X + 500 && !hasHit && driver.pos == 1)
            {
                hasHit = true;
                this.visible = false;
                audioPlayer.BirdPushSFX();
            }
            else if (pos.X <= cart.cartPos.X + 500 && !hasHit)
            {
                hasHit = true;
                this.visible = false;
                cart.health--;
                audioPlayer.BirdHitSFX();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            if (this.visible) spriteBatch.Draw(texture, pos, null, Color.White, 0f, Vector2.Zero, 0.4f, SpriteEffects.None, 0f);
            
        }
    }
}
