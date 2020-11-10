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
    class Ingredient
    {
        
        private Vector2 pos;
        private Texture2D texture,broth1,broth2,broth3,noodle1,noodle2,noodle3,topping1,topping2,topping3;
        private Texture2D[] broths, noodles, toppings;
        private string textureName;
        private bool hasCaught;
        private List<int> brothChance, noodleChance, toppingChance;
        private int currPhase;
        int row;
        bool visible = true;

        public Ingredient(string textureStr, int row, int column)
        {
            this.row = row;
            this.textureName = textureStr;

            int x = 1100+ (200 * column);
            if (row == 1)
            {
                pos = new Vector2(x, 660);
            } else if (row == 2)
            {
                pos = new Vector2(x, 580);
            }
            else
            {
                pos = new Vector2(x, 500);
            }

            brothChance = new List<int>();
            brothChance.Add(6);
            brothChance.Add(2);
            brothChance.Add(2);
            noodleChance = new List<int>();
            noodleChance.Add(2);
            noodleChance.Add(6);
            noodleChance.Add(2);
            toppingChance = new List<int>();
            toppingChance.Add(2);
            toppingChance.Add(2);
            toppingChance.Add(6);

            broths = new Texture2D[3];
            noodles = new Texture2D[3];
            toppings = new Texture2D[3];
            

            currPhase = 0;
        }

        public void Load(ContentManager Content)
        {
            this.texture = Content.Load<Texture2D>(this.textureName);
            broth1 = Content.Load<Texture2D>("broth1");
            broth2 = Content.Load<Texture2D>("broth2");
            broth3 = Content.Load<Texture2D>("broth3");
            broths[0] = broth1;
            broths[1] = broth2;
            broths[2] = broth3;

            noodle1 = Content.Load<Texture2D>("noodle1");
            noodle2 = Content.Load<Texture2D>("noodle2");
            noodle3 = Content.Load<Texture2D>("noodle3");
            noodles[0] = noodle1;
            noodles[1] = noodle2;
            noodles[2] = noodle3;

            topping1 = Content.Load<Texture2D>("topping1");
            topping2 = Content.Load<Texture2D>("topping2");
            topping3 = Content.Load<Texture2D>("topping3");
            toppings[0] = topping1;
            toppings[1] = topping2;
            toppings[2] = topping3;
        }

        public bool Update()
        {
            if (pos.X > -200)
            {
                pos.X -= 5;
            }
            else
            {
                pos.X = 1500;
                if (currPhase < 3)
                {
                    this.visible = true;
                    hasCaught = false;
                    this.ChangeTexture();
                    return true;

                }
                else
                {
                    this.visible = false;
                }
               
            }
            return false;
        }

        public bool Caught(Cart cart, Driver driver)
        {
            if (pos.X <= cart.cartPos.X + 550 && !hasCaught && driver.pos+2 == this.row)
            {
                hasCaught = true;
                this.visible = false;
                return true;
            }
            return false;
        }

        public void ChangeTexture()
        {
            Random numGen = new Random();
            int ingType = numGen.Next(1, 11);
            int typTyp = numGen.Next(0, 3);
            
            if (ingType < brothChance[currPhase])
            {
                this.texture = broths[typTyp];
            }
            else if (ingType < brothChance[currPhase] + noodleChance[currPhase])
            {
                this.texture = noodles[typTyp];
            }
            else
            {
                this.texture = toppings[typTyp];
            }
            if (currPhase == 3)
            {
                this.visible = false;
            }

            this.textureName = this.texture.Name;
        }

        public void PhaseProgress(int phase)
        {
            currPhase = phase;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.visible) { spriteBatch.Draw(this.texture, this.pos, null, Color.White, 0f, Vector2.Zero, 0.4f, SpriteEffects.None, 0f); }
            
        }

        public string Texture()
        {
            return this.textureName;
        }
    }
}
