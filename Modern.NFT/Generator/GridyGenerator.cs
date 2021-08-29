using Svg;
using System;
using System.IO;
using System.Text;
using System.Reflection;
using System.Drawing.Imaging;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Modern.NFT.Generator
{
    public class GridyShape
    {
       public List<string> body { get; set; }
        public List<string> mouth { get; set; }
        public List<string> eyes { get; set; }
    }

    public class GridyGenerator : BaseAvatar
    {
        private string exePath = "";
        private string shapesPath = "";
        private string gridyFolderPath = "";

        public GridyGenerator()
        {
            exePath = Path.GetDirectoryName(
                  Assembly.GetExecutingAssembly().Location);

            shapesPath = $"{exePath}\\Generator\\Gridy\\Shapes.json";
            gridyFolderPath = $"{exePath}\\Gridy";
        }

        public void Generate(int count, int width, 
            int height, int viewBoxSize)
        {
            string shapesJson = File.ReadAllText(shapesPath);
            var shapes = JsonConvert.DeserializeObject<GridyShape>(shapesJson);
            var bgColors = GeneticAlgoHelper.Generate(24, count);
            var fgColors = GeneticAlgoHelper.Generate(24, count);

            if (!Directory.Exists(gridyFolderPath))
                Directory.CreateDirectory(gridyFolderPath);

            int index = 1;
            for (int i=0; i< count-1; i++)
            {
                string clipA = $"clip-a-{index}";
                string clipB = $"clip-b-{index}";
                string urlA = $"url(#{clipA})";
                string urlB = $"url(#{clipB})";
                double half = 24 / 2;

                var gridyStringBuilder = new StringBuilder();
                gridyStringBuilder.AppendLine($"<svg width=\"{width}\" " +
                    $"height=\"{height}\" " +
                    $"viewBox=\"0 0 {viewBoxSize} {viewBoxSize}\"" +
                    $" xmlns=\"http://www.w3.org/2000/svg\"> ");

                var random = new Random();
                var randomBody = random.Next(0, shapes.body.Count - 1);
                var randomEyes = random.Next(0, shapes.eyes.Count - 1);
                var randomMouth = random.Next(0, shapes.mouth.Count - 1);

                gridyStringBuilder.AppendLine($"<defs><clipPath id=\"{clipA}\"><rect width=\"{half + 1}\" height=\"{viewBoxSize}\" x=\"0\" y=\"0\"></rect></clipPath>");
                gridyStringBuilder.AppendLine($"<clipPath id=\"{clipB}\"><rect width=\"{half}\" height=\"{viewBoxSize}\" x=\"{half}\" y=\"0\"></rect></clipPath></defs>");
                gridyStringBuilder.AppendLine($"<g style=\"fill:{bgColors[i]}\" clip-path=\"{urlA}\">{shapes.body[randomBody]}</g>");
                gridyStringBuilder.AppendLine($"<g style=\"fill:{bgColors[i]}\" clip-path=\"{urlB}\">{shapes.body[randomBody]}</g>");

                gridyStringBuilder.AppendLine($"<g style=\"fill:{fgColors[i]}\" clip-path=\"{urlA}\">{shapes.eyes[randomEyes]}</g>");
                gridyStringBuilder.AppendLine($"<g style=\"fill:{fgColors[i]}\" clip-path=\"{urlB}\">{shapes.eyes[randomEyes]}</g>");

                gridyStringBuilder.AppendLine($"<g style=\"fill:{fgColors[i]}\" clip-path=\"{urlA}\">{shapes.mouth[randomMouth]}</g>");
                gridyStringBuilder.AppendLine($"<g style=\"fill:{fgColors[i]}\" clip-path=\"{urlB}\">{shapes.mouth[randomMouth]}</g>");

                gridyStringBuilder.Append("</svg>");

                string finalSVG = gridyStringBuilder.ToString();
                File.WriteAllText($"gridy-{i}.svg", finalSVG);

                var svgDocument = SvgDocument.Open($"gridy-{i}.svg");
                var bitmap = svgDocument.Draw();
                bitmap.Save($"{gridyFolderPath}\\gridy-{i}.png", ImageFormat.Png);

                //File.Delete($"gridy-{i}.svg");
                index++;
            }
        }
    }
}
