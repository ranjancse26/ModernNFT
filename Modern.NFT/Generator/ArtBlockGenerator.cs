using Svg;
using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Drawing;
using System.Reflection;
using System.Drawing.Imaging;
using System.Collections.Generic;

namespace Modern.NFT.Generator
{
    public class ArtBlockGenerator : BaseAvatar
    {
        private string exePath = "";
        private string designPath1 = "";
        private string artBlocksDesign1FolderPath = "";

        public ArtBlockGenerator()
        {
            exePath = Path.GetDirectoryName(
                  Assembly.GetExecutingAssembly().Location);

            designPath1 = $"{exePath}\\Generator\\ArtBlocks\\Design1\\Paths.svg";
            artBlocksDesign1FolderPath = $"{exePath}\\ArtBlocks\\Design1";
        }

        private List<string> GetRandomPaths(int pathCount,
            List<string> originalPaths)
        {
            var randomPathCovered = new List<int>();
            var paths = new List<string>();
            var random = new Random();

            do
            {
                int randomPath = random.Next(0, originalPaths.Count - 1);
                if (!randomPathCovered.Contains(randomPath))
                {
                    randomPathCovered.Add(randomPath);
                    paths.Add(originalPaths[randomPath]);
                }
            }
            while (paths.Count < pathCount -1);

            return paths;
        }

        public void GenerateDesign1(int count)
        {
            var random = new Random();

            if (!Directory.Exists(artBlocksDesign1FolderPath))
                Directory.CreateDirectory(artBlocksDesign1FolderPath);

            var allPaths = File.ReadAllText(designPath1);
            var splittedPaths = allPaths.Split("<path").ToList();

            splittedPaths.RemoveAt(0);

            for (int i=0; i< count-1; i++)
            {
                var pathColors = GeneticAlgoHelper.Generate(24, splittedPaths.Count);
                var bgColors = GeneticAlgoHelper.Generate(24, splittedPaths.Count);

                var design1StringBuilder = new StringBuilder();
                design1StringBuilder.AppendLine($"<svg version=\"1.0\" xmlns=\"http://www.w3.org/2000/svg\" " +
                    $"viewBox=\"0 0 406.000000 486.000000\" preserveAspectRatio=\"xMidYMid meet\">");

                design1StringBuilder.AppendLine("<g transform=\"translate(0.000000, 486.000000)" +
                    " scale(0.100000, -0.100000)\" stroke=\"none\">");

                int index = 0;
                var splittedRandomPaths = GetRandomPaths(250,
                    splittedPaths);

                foreach (var path in splittedRandomPaths)
                {
                    string updatedPath = "<path " + path.Replace("#{color}", pathColors[index]);
                    index++;
                    design1StringBuilder.AppendLine(updatedPath.Replace("\r\n", ""));
                }

                design1StringBuilder.AppendLine("</g>");
                design1StringBuilder.AppendLine("</svg>");

                string finalSVG = design1StringBuilder.ToString();
                File.WriteAllText($"artBlock-{i}.svg", finalSVG);

                var svgDocument = SvgDocument.Open($"artBlock-{i}.svg");
                var bitmap = svgDocument.Draw();
                bitmap.Save($"{artBlocksDesign1FolderPath}\\artBlock-{i}.png",
                    ImageFormat.Png);

                var newImage = new Bitmap(bitmap.Width, bitmap.Height);
                using (var g = Graphics.FromImage(newImage))
                {
                    Color color = System.Drawing.ColorTranslator.FromHtml(
                        bgColors[i]);
                    g.Clear(color);
                    g.DrawImage(bitmap, new Point(0, 0));
                }
                newImage.Save($"{artBlocksDesign1FolderPath}\\artBlock-{i}.png",
                    ImageFormat.Png);

                File.Delete($"artBlock-{i}.svg");
            }           
        }
    }
}
