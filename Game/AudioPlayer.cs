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
    class AudioPlayer
    {
        // WAV files            ==>   SoundEffect
        // MP3 or other files   ==>   Song

        public Song playTheme;
        public Song menuTheme;
        public Song marketTheme;
        public string currSong = "none";
        //ingredient sound effects
        public List<SoundEffect> brothSFXs = new List<SoundEffect>();
        public List<SoundEffect> noodleSFXs = new List<SoundEffect>();
        public List<SoundEffect> toppingSFXs = new List<SoundEffect>();
        public SoundEffect broth1, broth2, broth3, broth4, noodle1, noodle2, noodle3, noodle4, topping1, topping2, topping3;

        //customer sound effects
        public List<SoundEffect> deliverSFXs = new List<SoundEffect>();
        public SoundEffect deliver1, deliver2, deliver3;

        //bird & human hit & push sfx
        public List<SoundEffect> humanPushSFXs = new List<SoundEffect>();
        public SoundEffect hPush1, hPush2, hPush3, hPush4, hPush5, hPush6;
        
        public List<string> pushTranslations = new List<string>();
        
        public SoundEffect humanHit, birdHit, birdPush;

        private Random numGen;

        public AudioPlayer()
        {
            pushTranslations.Add("You Fuck!");
            pushTranslations.Add("OW (bitch accent)");
            pushTranslations.Add("Stop you fuck!");
            pushTranslations.Add("You scat bastard!");
            pushTranslations.Add("whats wrong with you!");
            pushTranslations.Add("nooo, my burrrgeerr");
            numGen = new Random();

        }


        public void Load(ContentManager Content)
        {
            playTheme = Content.Load<Song>("audio/RR_Play");
            menuTheme = Content.Load<Song>("audio/RR_Menu");
            marketTheme = Content.Load<Song>("audio/RR_Market");

            //ingredient loads ==============================================
            noodle1 = Content.Load<SoundEffect>("audio/RR_noodle");
            noodle2 = Content.Load<SoundEffect>("audio/RR_noodle2");
            noodle3 = Content.Load<SoundEffect>("audio/RR_noodle3");
            noodle4 = Content.Load<SoundEffect>("audio/RR_noodle4");
            noodleSFXs.Add(noodle1);
            noodleSFXs.Add(noodle2);
            noodleSFXs.Add(noodle3);
            noodleSFXs.Add(noodle4);


            broth1 = Content.Load<SoundEffect>("audio/RR_broth");
            broth2 = Content.Load<SoundEffect>("audio/RR_broth2");
            broth3 = Content.Load<SoundEffect>("audio/RR_broth3");
            broth4 = Content.Load<SoundEffect>("audio/RR_broth4");
            brothSFXs.Add(broth1);
            brothSFXs.Add(broth2);
            brothSFXs.Add(broth3);
            brothSFXs.Add(broth4);


            topping1 = Content.Load<SoundEffect>("audio/RR_topping");
            topping2 = Content.Load<SoundEffect>("audio/RR_topping2");
            topping3 = Content.Load<SoundEffect>("audio/RR_topping3");
            toppingSFXs.Add(topping1);
            toppingSFXs.Add(topping2);
            toppingSFXs.Add(topping3);

            // human & bird sfx =================================================

            hPush1 = Content.Load<SoundEffect>("audio/RR_humanPush1");
            hPush2 = Content.Load<SoundEffect>("audio/RR_humanPush2");
            hPush3 = Content.Load<SoundEffect>("audio/RR_humanPush3");
            hPush4 = Content.Load<SoundEffect>("audio/RR_humanPush4");
            hPush5 = Content.Load<SoundEffect>("audio/RR_humanPush5");
            hPush6 = Content.Load<SoundEffect>("audio/RR_humanPush6");
            humanPushSFXs.Add(hPush1);
            humanPushSFXs.Add(hPush2);
            humanPushSFXs.Add(hPush3);
            humanPushSFXs.Add(hPush4);
            humanPushSFXs.Add(hPush5);
            humanPushSFXs.Add(hPush6);

            humanHit = Content.Load<SoundEffect>("audio/RR_hit");
            birdHit = Content.Load<SoundEffect>("audio/RR_hit2");
            birdPush = Content.Load<SoundEffect>("audio/RR_birdPush");

            //deliver sfx =========================================================

            deliver1 = Content.Load<SoundEffect>("audio/RR_delivery");
            deliver2 = Content.Load<SoundEffect>("audio/RR_delivery2");
            deliver3 = Content.Load<SoundEffect>("audio/RR_delivery3");
            deliverSFXs.Add(deliver1);
            deliverSFXs.Add(deliver2);
            deliverSFXs.Add(deliver3);


        }

        public void PlayTheme()
        {
            MediaPlayer.Play(playTheme);
            //loop song
            MediaPlayer.IsRepeating = true;
            currSong = "play";
        }
    

        public void MenuTheme()
        {
            MediaPlayer.Play(menuTheme);
            //loop song
            MediaPlayer.IsRepeating = true;
            currSong = "menu";
        }

        public void MarketTheme()
        {
            MediaPlayer.Play(marketTheme);
            //loop song
            MediaPlayer.IsRepeating = true;
            currSong = "market";
        }

        public void BrothSFX()
        {
            brothSFXs[numGen.Next(0, 4)].Play();
        }

        public void NoodleSFX()
        {
            noodleSFXs[numGen.Next(0, 4)].Play();
        }

        public void ToppingSFX()
        {
            toppingSFXs[numGen.Next(0, 3)].Play();
        }


        public void DeliverSFX()
        {
            deliverSFXs[numGen.Next(0, 3)].Play();
        }

        public void HumanPushSFX(Subtitle subtitle)
        {
            int random = numGen.Next(0, 6);
            humanPushSFXs[random].Play();
            subtitle.ChangeTextTo(this.pushTranslations[random]);
        }

        public void HumanHitSFX()
        {
            humanHit.Play();
        }
        public void BirdHitSFX()
        {
            birdHit.Play();
        }
        public void BirdPushSFX()
        {
            birdPush.Play();
        }


        public void Update()
        {
        }
    }
}
