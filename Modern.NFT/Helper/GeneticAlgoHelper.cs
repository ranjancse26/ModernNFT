using System;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;
using SimpleGeneticAlgorithm;

namespace Modern.NFT
{
    public class GeneticAlgoHelper
    {
        static string ColorToHex(Color color)
        {
            return Color.FromArgb(color.R, color.G, color.B).ToHex();
        }

        public static List<string> Generate(int generationSize, int populationSize)
        {
            List<string> colorCollection = new List<string>();
            World world = new World(generationSize, populationSize, 0, 0);
            world.InitializePopulation();

            foreach (var population in world.Population)
            {
                var splittedGenomes = population
                    .ToString()
                    .Replace(" ", "")
                    .SplitBy(8)
                    .ToList();

                int R = Convert.ToInt32(
                        splittedGenomes[0].ToString().Replace(" ", ""),
                        2);
                int G = Convert.ToInt32(
                        splittedGenomes[1].ToString().Replace(" ", ""),
                        2);
                int B = Convert.ToInt32(
                        splittedGenomes[2].ToString().Replace(" ", ""),
                        2);

                colorCollection.Add(ColorToHex(Color.FromArgb(R, G, B)));
            }

            return colorCollection;
        }
    }
}
