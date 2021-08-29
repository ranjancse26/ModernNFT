using System.IO;
using System.Text;
using System.Reflection;
using Modern.NFT.Helper;

namespace Modern.NFT.Generator
{
    public class IndoBitGeneratorCustom : BaseAvatar
    {
        private string exePath;
        private string indoBitFolderPath;

        public IndoBitGeneratorCustom()
        {
            exePath = Path.GetDirectoryName(
              Assembly.GetExecutingAssembly().Location);

            indoBitFolderPath = $"{exePath}\\Generator\\IndoBits\\Custom";
        }

        private string ApplyColor(string triat, string color)
        {
            return triat.Replace("${color}", $"fill:{color}");
        }

        public void GenerateBots(int count)
        {
            string exePath = Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().Location);

            string indoBitsLocation = $"{exePath}\\IndoBits\\Custom";

            if (!Directory.Exists(indoBitsLocation))
                Directory.CreateDirectory(indoBitsLocation);

            var colorSets1 = GeneticAlgoHelper.Generate(24, count);
            var colorSets2 = GeneticAlgoHelper.Generate(24, count);
            var colorSets3 = GeneticAlgoHelper.Generate(24, count);
            var colorSets4 = GeneticAlgoHelper.Generate(24, count);
            var colorSets5 = GeneticAlgoHelper.Generate(24, count);
            var colorSets6 = GeneticAlgoHelper.Generate(24, count);
            var colorSets7 = GeneticAlgoHelper.Generate(24, count);
            var colorSets8 = GeneticAlgoHelper.Generate(24, count);
            var colorSets9 = GeneticAlgoHelper.Generate(24, count);
            var colorSets10 = GeneticAlgoHelper.Generate(24, count);

            var backgroundColors = GeneticAlgoHelper.Generate(24, count);

            for (int i = 1; i < count; i++)
            {
                string svg = Generate(
                   colorSets1[i],
                   colorSets2[i],
                   colorSets3[i],
                   colorSets4[i],
                   colorSets5[i],
                   colorSets6[i],
                   colorSets7[i],
                   colorSets8[i],
                   colorSets9[i],
                   colorSets10[i]);

                System.IO.File.WriteAllText("indoBit.svg",
                    svg.Replace("$", "").Trim());

                SVGToBitmap(backgroundColors[i],
                   $"{exePath}\\indoBit.svg",
                   $"{exePath}\\IndoBits\\Custom\\indoBit-{i}.png", 500, 500);

                File.Delete("indoBit.svg");
            }
        }

        public string Generate(
            string color1,
            string color2,
            string color3,
            string color4,
            string color5,
            string color6,
            string color7,
            string color8,
            string color9,
            string color10)
        {
            var indoBitStringBuilder = new StringBuilder();

            var allTriats = FileHelper.ReadFiles(indoBitFolderPath);

            int index = 0;

            indoBitStringBuilder.AppendLine("<svg version=\"1.1\" " +
                "id=\"svg186\" width=\"500\" height=\"500\" " +
                " viewBox=\"0 0 500 500\"" +
                " xmlns:xlink =\"http://www.w3.org/1999/xlink\"" +
                " xmlns=\"http://www.w3.org/2000/svg\"" +
                " xmlns:svg=\"http://www.w3.org/2000/svg\">");

            indoBitStringBuilder.AppendLine("<g id=\"g192\">");
            indoBitStringBuilder.AppendLine("<g id=\"g403\">");

            indoBitStringBuilder.AppendLine(ApplyColor(allTriats[index++], color1));
            indoBitStringBuilder.AppendLine(ApplyColor(allTriats[index++], color2));
            indoBitStringBuilder.AppendLine(ApplyColor(allTriats[index++], color3));
            indoBitStringBuilder.AppendLine(ApplyColor(allTriats[index++], color4));
            indoBitStringBuilder.AppendLine(ApplyColor(allTriats[index++], color5));
            indoBitStringBuilder.AppendLine(ApplyColor(allTriats[index++], color6));
            indoBitStringBuilder.AppendLine(ApplyColor(allTriats[index++], color7));
            indoBitStringBuilder.AppendLine(ApplyColor(allTriats[index++], color8));
            indoBitStringBuilder.AppendLine(ApplyColor(allTriats[index++], color9));
            indoBitStringBuilder.AppendLine(ApplyColor(allTriats[index++], color10));

            indoBitStringBuilder.Append("</g></g>");
            indoBitStringBuilder.Append("</svg>");

            string finalSVG = indoBitStringBuilder.ToString();
            return finalSVG;
        }
    }
}
