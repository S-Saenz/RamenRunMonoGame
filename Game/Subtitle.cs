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
using Microsoft.Xna.Framework.Media;

namespace RamenRun
{
    class Subtitle
    {
        public string text;
        //font
        private SpriteFont nikuMaru;
        bool visible = false;

        int counter = 1;
        int limit = 2;
        float countDuration = 0.5f; //every amount of seconds.
        float currentTime = 0f;

        public Subtitle()
        {
            text = "subtitles";
        }

        public void Update(GameTime gameTime)
        {

            currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds; //Time passed since last Update() 

            if (currentTime >= countDuration)
            {
                counter++;
                currentTime -= countDuration; // "use up" the time
                                              //any actions to perform
            }
            if (counter >= limit)
            {
                counter = 0;//Reset the counter;
                this.visible = false;
            }
        }

        public void ChangeTextTo(string str)
        {
            text = str;
            visible = true;
            counter = 0;
            countDuration = (float)str.Length * 0.01f + 0.5f;
        }

        public void Load(ContentManager Content)
        {
            //font
            nikuMaru = Content.Load<SpriteFont>("nikumaru");
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            if(this.visible)spriteBatch.DrawString(nikuMaru, this.text, new Vector2(550, 700), Color.Black);
        }
    }
}
