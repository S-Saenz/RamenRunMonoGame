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
    class Human
    {
        Vector2 pos;
        Texture2D texture;
        bool hasHit;
        bool visible;
        private List<string> sfxTxt = new List<string>();
        private Random numGen = new Random();

        public Human()
        {
            sfxTxt.Add("squish");
            sfxTxt.Add("splat");
            sfxTxt.Add("crunch");
            pos = new Vector2(2000, 580);
            visible = true;
        }

        public void Load(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("human");
        }

        public void Update()
        {
            if (pos.X > -200)
            {
                pos.X -= 6;

            }
            else
            {
                pos.X = 2000;
                hasHit = false;
                this.visible = true;
            }
        }

        public void CheckCollision(Cart cart, Driver driver, AudioPlayer audioPlayer, Subtitle subtitle)
        {
            if (pos.X <= cart.cartPos.X+500 && !hasHit && driver.pos == -1)
            {
                hasHit = true;
                this.visible = false;
                audioPlayer.HumanPushSFX(subtitle);

            } else if (pos.X <= cart.cartPos.X + 500 && !hasHit)
            {
                hasHit = true;
                this.visible = false;
                cart.health--;
                audioPlayer.HumanHitSFX();
                subtitle.ChangeTextTo(sfxTxt[numGen.Next(0,3)]);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            if (this.visible) spriteBatch.Draw(texture, pos, null, Color.White, 0f, Vector2.Zero, 0.4f, SpriteEffects.None, 0.5f);
            
        }
    }
}
