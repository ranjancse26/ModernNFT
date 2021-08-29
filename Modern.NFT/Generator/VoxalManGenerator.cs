using System.IO;
using System.Text;
using System.Reflection;
using Modern.NFT.Helper;

namespace Modern.NFT.Generator
{
    public class VoxalManGenerator : BaseAvatar
    {
        private string exePath;
        private string voxalManFolderPath;

        public VoxalManGenerator()
        {
            exePath = Path.GetDirectoryName(
              Assembly.GetExecutingAssembly().Location);

            voxalManFolderPath = $"{exePath}\\Generator\\VoxalMan";
        }

        private string ApplyColor(string triat, string color)
        {
            return triat.Replace("${color}", color);
        }
        public void GenerateVoxalMan(int count)
        {
            string exePath = Path.GetDirectoryName(
                  Assembly.GetExecutingAssembly().Location);
            string voxalManFolderPath = $"{exePath}\\VoxalMan";

            if (!Directory.Exists(voxalManFolderPath))
                Directory.CreateDirectory(voxalManFolderPath);

            var backgroundColors = GeneticAlgoHelper.Generate(24, count);

            var colorSet1 = GeneticAlgoHelper.Generate(24, count);
            var colorSet2 = GeneticAlgoHelper.Generate(24, count);
            var colorSet3 = GeneticAlgoHelper.Generate(24, count);
            var colorSet4 = GeneticAlgoHelper.Generate(24, count);
            var colorSet5 = GeneticAlgoHelper.Generate(24, count);
            var colorSet6 = GeneticAlgoHelper.Generate(24, count);
            var colorSet7 = GeneticAlgoHelper.Generate(24, count);
            var colorSet8 = GeneticAlgoHelper.Generate(24, count);

            for (int i = 1; i < count; i++)
            {
                string svg = Generate(
                    colorSet1[i],
                    colorSet2[i],
                    colorSet3[i],
                    colorSet4[i],
                    colorSet5[i],
                    colorSet6[i],
                    colorSet7[i],
                    colorSet8[i]);

                string inputImagePath = $"{voxalManFolderPath}\\voxalMan-{i}.svg";
                string outputImagePath = $"{voxalManFolderPath}\\voxalMan-{i}.jpg";

                System.IO.File.WriteAllText(inputImagePath,
                    svg.Trim());

                SVGToBitmap(backgroundColors[i],
                    inputImagePath, outputImagePath, 500, 500);
            }
        }

        private string Generate(
            string color1,
            string color2,
            string color3,
            string color4,
            string color5,
            string color6,
            string color7,
            string color8)
        {
            var voxalManStringBuilder = new StringBuilder();

            var allTriats = FileHelper.ReadFiles(voxalManFolderPath);

            int index = 0;

            voxalManStringBuilder.AppendLine("<svg xmlns=\"http://www.w3.org/2000/svg\" width = \"112.828mm\" height = \"102.667mm\" viewBox=\"0 0 533 485\">");

            voxalManStringBuilder.AppendLine(ApplyColor(allTriats[index++], color1));
            voxalManStringBuilder.AppendLine(ApplyColor(allTriats[index++], color2));
            voxalManStringBuilder.AppendLine(ApplyColor(allTriats[index++], color3));
            voxalManStringBuilder.AppendLine(ApplyColor(allTriats[index++], color4));
            voxalManStringBuilder.AppendLine(ApplyColor(allTriats[index++], color5));
            voxalManStringBuilder.AppendLine(ApplyColor(allTriats[index++], color6));
            voxalManStringBuilder.AppendLine(ApplyColor(allTriats[index++], color7));
            voxalManStringBuilder.AppendLine(ApplyColor(allTriats[index++], color8));

            voxalManStringBuilder.Append("</svg>");

            string finalSVG = voxalManStringBuilder.ToString();
            return finalSVG;
        }
    }
}
