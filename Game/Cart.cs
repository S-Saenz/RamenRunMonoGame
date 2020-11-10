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
    class Cart
    {
        public int health, maxHp;
        public Texture2D texture; 
        Texture2D ogSkin, blueWave, cyber, goth, ita, pride;
        Texture2D dmg, highDmg, medDmg, lowDmg, noDmg;
        public Vector2 cartPos;
        public Stack<Texture2D> skinStack;


        public Cart()
        {
            health = 4;
            maxHp = 4;
            cartPos = new Vector2(-30, 500);
            skinStack = new Stack<Texture2D>();
            skinStack.Push(ogSkin);
        }

        public void Revert()
        {
            skinStack.Pop();
            texture = skinStack.Peek();
        }

        public void ChangeTexture(string str)
        {
            switch (str)
            {
                case "wave":
                    skinStack.Push(blueWave);
                    break;
                case "cyber":
                    skinStack.Push(cyber);
                    break;
                case "goth":
                    skinStack.Push(goth);
                    break;
                case "ita":
                    skinStack.Push(ita);
                    break;
                case "pride":
                    skinStack.Push(pride);
                    break;
            }
            texture = skinStack.Peek();
            
        }

        public void Load(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("cartFull");
            //cart skins

            ogSkin = Content.Load<Texture2D>("cartFull");
            blueWave = Content.Load<Texture2D>("cartFull_BlueWave");
            cyber = Content.Load<Texture2D>("cartFull_Cyber");
            goth = Content.Load<Texture2D>("cartFull_Goth");
            ita = Content.Load<Texture2D>("cartFull_Ita");
            pride = Content.Load<Texture2D>("cartFull_Pride");

            highDmg = Content.Load<Texture2D>("highDmg");
            medDmg = Content.Load<Texture2D>("medDmg");
            lowDmg = Content.Load<Texture2D>("lowDmg");
            noDmg = Content.Load<Texture2D>("noDmg");
            dmg = Content.Load<Texture2D>("noDmg");

            texture = goth;
        }

        public void Update()
        {
            //int cartBump;
            Random numGen = new Random();

            if (numGen.Next(0, 10) < 3 && cartPos.Y > 490)
            {
                if (numGen.Next(0, 10) < 5 && cartPos.Y > 490)
                {
                    cartPos.Y--;
                }
                else if (cartPos.Y < 500)
                {
                    cartPos.Y++;
                }
            }
            if (health <= maxHp / 3)
            {
                dmg = highDmg;
            }
            else if (health <= maxHp / 2)
            {
                dmg = medDmg;
            }
            else if (health < maxHp)
            {
                dmg = lowDmg;
            }
            else if (dmg != noDmg)
            {
                dmg = noDmg;
            }
        }



        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, cartPos, null, Color.White, 0f, Vector2.Zero, 0.4f, SpriteEffects.None, 0.5f);
            spriteBatch.Draw(dmg, cartPos, null, Color.White, 0f, Vector2.Zero, 0.4f, SpriteEffects.None, 0.4f);
            
        }
    }


}
