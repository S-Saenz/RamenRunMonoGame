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
    class MarketScene
    {
        public int cash = 2000;
        private Texture2D bg, merchant, speechBubble, contTexture, contHover;
        private Button contButton;
        private AudioPlayer audioPlayer;
        Cart cart { get; set; }
        private MouseState oldMState;

        // =============== shop buttons ================
        private List<ShopButton> shopButtons = new List<ShopButton>();
        private int clickedButtonIndex = -1;
        private List<ShopButton> shopOpts;
        private List<Texture2D> shopButtonTextures, shopButtonHovers, shopButtonSolds;
        private Texture2D blueWave, cyber, goth, ita, pride;
        private Texture2D blueWaveH, cyberH, gothH, itaH, prideH;
        private Texture2D blueWaveSold, cyberSold, gothSold, itaSold, prideSold;
        private int[] shopPrices = new int[5];
        private int[] shopButtonXs = new int[3];
        private string[] merchantLines = new string[]
        {
                "Wave, huh?  A classic design!",
                "Cyber, huh? That neon's gonna cost ya!",
                "Goth, huh?  You got black makeup to go with that?",
                "Ita, huh?  God why do I even have this...",
                "Pride, huh?  Gotta love that colorful aesthetic!",
                "Jeez, someone did a number on your car!  I could fix it for you, buuuut… pay up first.",
                "Yo, I knew you were gonna come in today!  How much money are you givin’ me this time?",
                "C’mon kid I ain’t got all day, make up your mind!",
                "You can’t afford it?  Don’t bother me unless you actually have money to give me."
        };
        private string merchantLine = "hello";
        private SpriteFont nikuMaruBig, nikuMaru;
        public int mindChanges = 0;


        public MarketScene(AudioPlayer audioP, Cart cart)
        {
            this.cart = cart;
            shopButtonXs[0] = 60;
            shopButtonXs[1] = 450;
            shopButtonXs[2] = 840;
            this.audioPlayer = audioP;
            shopButtonTextures = new List<Texture2D>();
            shopButtonHovers = new List<Texture2D>();
            shopButtonSolds = new List<Texture2D>();
            shopPrices[0] = 3000;
            shopPrices[1] = 4000;
            shopPrices[2] = 1000;
            shopPrices[3] = 5000;
            shopPrices[4] = 2000;
        }

        public void Load(ContentManager Content)
        {
            blueWave = Content.Load<Texture2D>("wave");
            cyber = Content.Load<Texture2D>("cyber");
            goth = Content.Load<Texture2D>("goth");
            ita = Content.Load<Texture2D>("ita");
            pride = Content.Load<Texture2D>("pride");

            shopButtonTextures.Add(blueWave);
            shopButtonTextures.Add(cyber);
            shopButtonTextures.Add(goth);
            shopButtonTextures.Add(ita);
            shopButtonTextures.Add(pride);

            blueWaveH = Content.Load<Texture2D>("waveHover");
            cyberH = Content.Load<Texture2D>("cyberHover");
            gothH = Content.Load<Texture2D>("gothHover");
            itaH = Content.Load<Texture2D>("itaHover");
            prideH = Content.Load<Texture2D>("prideHover");


            shopButtonHovers.Add(blueWaveH);
            shopButtonHovers.Add(cyberH);
            shopButtonHovers.Add(gothH);
            shopButtonHovers.Add(itaH);
            shopButtonHovers.Add(prideH);


            blueWaveSold = Content.Load<Texture2D>("wave2");
            cyberSold = Content.Load<Texture2D>("cyber2");
            gothSold = Content.Load<Texture2D>("goth2");
            itaSold = Content.Load<Texture2D>("ita2");
            prideSold = Content.Load<Texture2D>("pride2");


            shopButtonSolds.Add(blueWaveSold);
            shopButtonSolds.Add(cyberSold);
            shopButtonSolds.Add(gothSold);
            shopButtonSolds.Add(itaSold);
            shopButtonSolds.Add(prideSold);


            for (int i = 0; i < shopButtonTextures.Count; i++)
            {
                shopButtons.Add(new ShopButton(shopButtonTextures[i], shopButtonHovers[i], shopButtonSolds[i], 0, 500, i, shopPrices[i]));
            }

            merchant = Content.Load<Texture2D>("merchant");
            bg = Content.Load<Texture2D>("marketBG");
            speechBubble = Content.Load<Texture2D>("speechBubble");
            contTexture = Content.Load<Texture2D>("continue");
            contHover = Content.Load<Texture2D>("continueHover");
            
            contButton = new Button(contTexture, contHover, 700, 0);

            nikuMaruBig = Content.Load<SpriteFont>("nikuMaruBig");
            nikuMaru = Content.Load<SpriteFont>("nikumaru");

            GenerateShopOpts();
        }

        public string Update(MouseState mouseState)
        {
            if (contButton.EnterButton(mouseState) && mouseState.LeftButton == ButtonState.Pressed)
            {
                audioPlayer.DeliverSFX();
                return "play";
            }
            int i = 0;
            foreach (ShopButton button in shopOpts)
            {
                if (mouseState == oldMState) break;
                if (button.texture != button.soldTexture && button.ButtonClicked(mouseState, cash))
                {
                    if (clickedButtonIndex > -1 && cash >= button.price)
                    {
                        shopOpts[clickedButtonIndex].Reset();
                        cash += shopOpts[clickedButtonIndex].price;
                        cart.skinStack.Pop();
                        ++mindChanges;
                    }else if (cash < button.price) {
                        merchantLine = "Stop being so poor";
                        break;
                    }
                    clickedButtonIndex = i;
                    cart.ChangeTexture(button.normTexture.Name);
                    merchantLine = merchantLines[button.index];
                    cash -= button.price;
                } else if (button.texture == button.soldTexture && button.ButtonClicked(mouseState, cash))
                {
                    button.Reset();
                    cart.Revert();
                    clickedButtonIndex = -1;
                    merchantLine = "oh ok";
                    cash += button.price;
                    ++mindChanges;

                }
                if (mindChanges> 3)
                {
                    merchantLine = "C'mon kid I ain't got all day, make up your mind!";
                }
                i++;
            }
            oldMState = mouseState;
            return "market";

        }

        public string WrapText(SpriteFont spriteFont, string text, float maxLineWidth)
        {
            string[] words = text.Split(' ');
            StringBuilder sb = new StringBuilder();
            float lineWidth = 0f;
            float spaceWidth = spriteFont.MeasureString(" ").X;

            foreach (string word in words)
            {
                Vector2 size = spriteFont.MeasureString(word);

                if (lineWidth + size.X < maxLineWidth)
                {
                    sb.Append(word + " ");
                    lineWidth += size.X + spaceWidth;
                }
                else
                {
                    sb.Append("\n" + word + " ");
                    lineWidth = size.X + spaceWidth;
                }
            }

            return sb.ToString();
        }

        public void GenerateShopOpts()
        {
            foreach (ShopButton button in shopButtons)
            {
                button.Reset();
            }
            Random numGen = new Random();
            List<ShopButton> allOptions = new List<ShopButton>();
            allOptions.AddRange(shopButtons);
            List<ShopButton> opts = new List<ShopButton>();

            int index = numGen.Next(0, allOptions.Count);
            opts.Add(allOptions[index]);
            allOptions.RemoveAt(index);
            opts[0].changeX(shopButtonXs[0]);

            index = numGen.Next(0, allOptions.Count);
            opts.Add(allOptions[index]);
            allOptions.RemoveAt(index);
            opts[1].changeX(shopButtonXs[1]);

            index = numGen.Next(0, allOptions.Count);
            opts.Add(allOptions[index]);
            allOptions.RemoveAt(index);
            opts[2].changeX(shopButtonXs[2]);

            shopOpts = opts;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(bg, new Vector2(0, 0), null, Color.White, 0f, Vector2.Zero, 1.2f, SpriteEffects.None, 1.0f);
            spriteBatch.Draw(merchant, new Vector2(650, 100), null, Color.White, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0.3f);
            spriteBatch.Draw(speechBubble, new Vector2(100, 150), null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.2f);
            contButton.Draw(spriteBatch);
            spriteBatch.DrawString(nikuMaruBig, this.WrapText(nikuMaruBig, this.merchantLine, 400), new Vector2(150, 200), Color.Black);

            foreach (ShopButton button in shopOpts)
            {
                button.Draw(spriteBatch);
            }
        }

    }
}
