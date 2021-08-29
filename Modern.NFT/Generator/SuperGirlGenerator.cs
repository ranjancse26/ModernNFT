using System.IO;
using System.Reflection;
using System.Text;
using Modern.NFT.Helper;

namespace Modern.NFT.Generator
{
    public class SuperGirlGenerator : BaseAvatar
    {
        private string exePath;
        private string superGirlFolderPath;

        public SuperGirlGenerator()
        {
            exePath = Path.GetDirectoryName(
              Assembly.GetExecutingAssembly().Location);

            superGirlFolderPath = $"{exePath}\\Generator\\SuperGirl";
        }

        private string ApplyColor(string triat, string color)
        {
            return triat.Replace("${color}", color);
        }
                
        public void GenerateSuperGirl(int count)
        {
            string exePath = Path.GetDirectoryName(
                  Assembly.GetExecutingAssembly().Location);
            string superGirlFolderPath = $"{exePath}\\SuperGirl";

            if (!Directory.Exists(superGirlFolderPath))
                Directory.CreateDirectory(superGirlFolderPath);

            var backgroundColors = GeneticAlgoHelper.Generate(24, count);

            var leftEyeColorSets = GeneticAlgoHelper.Generate(24, count);
            var rightEyeColorSets = GeneticAlgoHelper.Generate(24, count);
            var eyeColorSets = GeneticAlgoHelper.Generate(24, count);
            var faceColorSets = GeneticAlgoHelper.Generate(24, count);
            var hairColorSets = GeneticAlgoHelper.Generate(24, count);
            var LeftRightColorSets = GeneticAlgoHelper.Generate(24, count);
            var LipsColorSets = GeneticAlgoHelper.Generate(24, count);
            var NeckColorSets = GeneticAlgoHelper.Generate(24, count);
            var OverAllColorSets = GeneticAlgoHelper.Generate(24, count);

            SuperGirlGenerator superGirlGenerator = new SuperGirlGenerator();
            for (int i = 1; i < count; i++)
            {
                string svg = Generate(
                    leftEyeColorSets[i],
                    rightEyeColorSets[i],
                    eyeColorSets[i],
                    faceColorSets[i],
                    hairColorSets[i],
                    LeftRightColorSets[i],
                    LipsColorSets[i],
                    NeckColorSets[i],
                    "#0d0d0d");

                string inputImagePath = $"{superGirlFolderPath}\\superGirl-{i}.svg";
                string outputImagePath = $"{superGirlFolderPath}\\superGirl-{i}.jpg";

                System.IO.File.WriteAllText(inputImagePath,
                    svg.Trim());

                SVGToBitmap("#000000",
                    inputImagePath, outputImagePath, 500, 500);
            }
        }

        private string Generate(
            string leftEyeColor,
            string rightEyeColor,
            string eyeColor,
            string faceColor,
            string hairColor,
            string leftRightColor,
            string lipsColor,
            string neckColor,
            string overAllColor)
        {
            var superGirlStringBuilder = new StringBuilder();

            var allTriats = FileHelper.ReadFiles(superGirlFolderPath);

            int index = allTriats.Count-1;

            superGirlStringBuilder.AppendLine("<svg xmlns=\"http://www.w3.org/2000/svg\" width = \"108.374mm\" height = \"99.9915mm\" viewBox = \"0 0 1280 1181\" >");

            superGirlStringBuilder.AppendLine(ApplyColor(allTriats[index--], rightEyeColor));
            superGirlStringBuilder.AppendLine(ApplyColor(allTriats[index--], overAllColor));
            superGirlStringBuilder.AppendLine(ApplyColor(allTriats[index--], neckColor));
            superGirlStringBuilder.AppendLine(ApplyColor(allTriats[index--], lipsColor));
            superGirlStringBuilder.AppendLine(ApplyColor(allTriats[index--], leftRightColor));
            superGirlStringBuilder.AppendLine(ApplyColor(allTriats[index--], leftEyeColor));
            superGirlStringBuilder.AppendLine(ApplyColor(allTriats[index--], hairColor));
            superGirlStringBuilder.AppendLine(ApplyColor(allTriats[index--], faceColor));
            superGirlStringBuilder.AppendLine(ApplyColor(allTriats[index--], eyeColor));

            superGirlStringBuilder.Append("</svg>");

            string finalSVG = superGirlStringBuilder.ToString();
            return finalSVG;
        }
    }
}
