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
    class Recipe
    {
        private string broth,noodle,topping;
        public int brothPts, noodlePts, toppingPts, mistakes;
        public int maxPointsPer = 2;
        private Random numGen;
        private List<string> broths, noodles, toppings;
        private int brothIndex, noodleIndex, toppingIndex;

        public Recipe()
        {
            numGen = new Random();
            broths = new List<string>();
            broths.Add("broth1");
            broths.Add("broth2");
            broths.Add("broth3");
            noodles = new List<string>();
            noodles.Add("noodle1");
            noodles.Add("noodle2");
            noodles.Add("noodle3");
            toppings = new List<string>();
            toppings.Add("topping1");
            toppings.Add("topping2");
            toppings.Add("topping3");


            brothIndex = numGen.Next(0, 3);
            noodleIndex = numGen.Next(0, 3);
            toppingIndex = numGen.Next(0, 3);

            broth = broths[brothIndex];
            noodle = noodles[noodleIndex];
            topping = toppings[toppingIndex];

        }

        public int BrothIndex()
        {
            return this.brothIndex;
        }

        public int NoodleIndex()
        {
            return this.noodleIndex;
        }

        public int ToppingIndex()
        {
            return this.toppingIndex;
        }

        public int NextIngredientIndex(int phase)
        {
            if (phase == 0)
            {
                return this.brothIndex;
            } else if (phase == 1)
            {
                return this.noodleIndex;
            }
            else if(phase == 2)
            {
                return this.toppingIndex;
            }
            return 0;
        }

        public int CalcPrice()
        {
            int price = brothPts + noodlePts + toppingPts;
            if (mistakes > 1) price /= 5;
            if (mistakes > 3) price /= 5;
            if (mistakes > 5) price /= 5;
            if (mistakes > 10) price /= 5;
            return price;
        }
        public void Reset()
        {
            brothIndex = numGen.Next(0, 3);
            noodleIndex = numGen.Next(0, 3);
            toppingIndex = numGen.Next(0, 3);

            broth = broths[brothIndex];
            noodle = noodles[noodleIndex];
            topping = toppings[toppingIndex];

            brothPts = noodlePts = toppingPts = 0;
            
        }

        public void CatchIng(Ingredient ing, int phase, AudioPlayer audioPlayer)
        {
            if (ing.Texture().Contains("broth"))
            {
                audioPlayer.BrothSFX();
            } else if (ing.Texture().Contains("noodle"))
            {
                audioPlayer.NoodleSFX();
            }
            else
            {
                audioPlayer.ToppingSFX();
            }
            if (phase == 0)
            {
                if (ing.Texture() == this.broth && brothPts < maxPointsPer)
                {
                    this.brothPts++;
                }
                else if(ing.Texture() != this.broth && ing.Texture().Contains("broth"))
                {
                    this.mistakes++;
                }
            }
            else if (phase == 1)
            {
                if (ing.Texture() == this.noodle && noodlePts < maxPointsPer)
                {
                    this.noodlePts++;
                }
                else if (ing.Texture() != this.noodle && ing.Texture().Contains("noodle"))
                {
                    this.mistakes++;
                }
            }
            else if (phase == 2)
            {
                if (ing.Texture() == this.topping && toppingPts < maxPointsPer)
                {
                    this.toppingPts++;
                }
                else if (ing.Texture() != this.topping && ing.Texture().Contains("topping"))
                {
                    this.mistakes++;
                }
            }
        }
    }
}
