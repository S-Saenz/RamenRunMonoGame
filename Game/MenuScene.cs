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
    class MenuScene
    {
        private Texture2D logo, play, playHover, credits, creditsHover;
        private Button playButton, creditsButton;
        private AudioPlayer audioPlayer;

        public MenuScene(AudioPlayer audioP, Texture2D pl, Texture2D plH)
        {
            this.audioPlayer = audioP;
            play = pl;
            playHover = plH;
        }

        public void Load(ContentManager Content)
        {
            logo = Content.Load<Texture2D>("logo");
            credits = Content.Load<Texture2D>("credits");
            creditsHover = Content.Load<Texture2D>("creditsHover");

            playButton = new Button(play, playHover, 500, 550);
            creditsButton = new Button(credits, creditsHover, 500, 650);
        }

        public string Update(MouseState mouseState)
        {
            if(playButton.EnterButton(mouseState) && mouseState.LeftButton == ButtonState.Pressed)
            {
                audioPlayer.DeliverSFX();
                return "play";
            } else if (creditsButton.EnterButton(mouseState) && mouseState.LeftButton == ButtonState.Pressed)
            {
                audioPlayer.DeliverSFX();
                return "market";
            }
            return "menu";
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(logo, new Vector2(200, 50), null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
            playButton.Draw(spriteBatch);
            creditsButton.Draw(spriteBatch);
        }
    }
}
