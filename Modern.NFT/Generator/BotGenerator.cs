using Modern.NFT.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Modern.NFT.Generator
{
    public enum BotEnum
    {
        Eye,
        Face,
        Mouth,
        Side,
        Texture,
        Top
    }

    public class BotGenerator : BaseAvatar
    {
        private string exePath;

        private string botEyesFolderPath;
        private string botFacesFolderPath;
        private string botMouthFolderPath;
        private string botSidesFolderPath;
        private string botTextureFolderPath;
        private string botTopFolderPath;

        private List<string> eyeCollection = new List<string>();
        private List<string> faceCollection = new List<string>();
        private List<string> mouthCollection = new List<string>();
        private List<string> sideCollection = new List<string>();
        private List<string> textureCollection = new List<string>();
        private List<string> topCollection = new List<string>();

        public BotGenerator()
        {
            exePath = Path.GetDirectoryName(
               Assembly.GetExecutingAssembly().Location);

            botEyesFolderPath = string.Format("{0}\\Generator\\Bots\\{1}",
               exePath, "Eyes");
            botFacesFolderPath = string.Format("{0}\\Generator\\Bots\\{1}",
               exePath, "Faces");
            botMouthFolderPath = string.Format("{0}\\Generator\\Bots\\{1}",
              exePath, "Mouth");
            botSidesFolderPath = string.Format("{0}\\Generator\\Bots\\{1}",
              exePath, "Sides");
            botTextureFolderPath = string.Format("{0}\\Generator\\Bots\\{1}",
             exePath, "Texture");
            botTopFolderPath = string.Format("{0}\\Generator\\Bots\\{1}",
             exePath, "Top");

            eyeCollection = FileHelper.ReadFiles(botEyesFolderPath);
            faceCollection = FileHelper.ReadFiles(botFacesFolderPath);
            mouthCollection = FileHelper.ReadFiles(botMouthFolderPath);
            sideCollection = FileHelper.ReadFiles(botSidesFolderPath);
            textureCollection = FileHelper.ReadFiles(botTextureFolderPath);
            topCollection = FileHelper.ReadFiles(botTopFolderPath);
        }

        private string RandomTriat(BotEnum botEnum)
        {
            var random = new Random();
            switch (botEnum)
            {
                case BotEnum.Eye:
                    return eyeCollection[random.Next(0, 
                            eyeCollection.Count - 1)];
                case BotEnum.Face:
                    return faceCollection[random.Next(0,
                            faceCollection.Count - 1)];
                case BotEnum.Mouth:
                    return mouthCollection[random.Next(0,
                            mouthCollection.Count - 1)];
                case BotEnum.Side:
                    return sideCollection[random.Next(0,
                            sideCollection.Count - 1)];
                case BotEnum.Texture:
                    return textureCollection[random.Next(0,
                            textureCollection.Count - 1)];
                case BotEnum.Top:
                    return topCollection[random.Next(0,
                            topCollection.Count - 1)];
            }
            return "";
        }

        private string ApplyColor(string triat, string color)
        {
            return triat.Replace("${color}", color);
        }

        private string ApplyFaceColor(string face,
            string color, string texture)
        {
            face = face.Replace("${color}", color);
            face = face.Replace("${texture}", texture);
            return face;
        }

        private string Group(int x, int y,
            string content, bool chance)
        {
            if (chance)
            {
                string path = $"<g transform= \"translate(${x}, ${y})\">${content}</g>";
                return path;
            }
            return "";
        }

        private bool RandomBool(int max)
        {
            var random = new Random();
            var randomValue = random.Next(max);
            if (randomValue % 2 == 0)
                return true;
            return false;
        }

        public void GenerateBots(int count)
        {
            string exePath = Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().Location);

            string superBotsLocation = $"{exePath}\\SuperBots";

            if (!Directory.Exists(superBotsLocation))
                Directory.CreateDirectory(superBotsLocation);

            var colorSets1 = GeneticAlgoHelper.Generate(24, count);
            var colorSets2 = GeneticAlgoHelper.Generate(24, count);
            var backgroundColors = GeneticAlgoHelper.Generate(24, count);

            BotGenerator botGenerator = new BotGenerator();

            for (int i = 1; i < count; i++)
            {
                string primaryColor = colorSets1[i];
                string secondaryColor = colorSets2[i];

                string svg = botGenerator.Generate(
                    primaryColor,
                    secondaryColor,
                    true, true,
                    RandomBool(10), true,
                    true);

                System.IO.File.WriteAllText("superBot.svg",
                    svg.Replace("$", "").Trim());

                SVGToBitmap(backgroundColors[i],
                   $"{exePath}\\superBot.svg",
                   $"{exePath}\\SuperBots\\superBot-{i}.png", 500, 500);

                File.Delete("superBot.svg");
            }
        }

        private string Generate(string primaryColor,
            string secondaryColor,
            bool isDisplaySide,
            bool isDisplayTop,
            bool isDisplayTexture,
            bool isDisplayMouth,
            bool isDisplayEye)
        {
            var botStringBuilder = new StringBuilder();
            
            var sidesPart = ApplyColor(RandomTriat(BotEnum.Side), secondaryColor);
            var topPart = ApplyColor(RandomTriat(BotEnum.Top), secondaryColor);
            var facePart = ApplyColor(RandomTriat(BotEnum.Face), primaryColor);
            var mouthPart = ApplyColor(RandomTriat(BotEnum.Mouth), primaryColor);
            var eyesPart = RandomTriat(BotEnum.Eye);
            var texturePart = RandomTriat(BotEnum.Texture);

            botStringBuilder.Append("<svg xmlns:dc=\"http://purl.org/dc/elements/1.1/\" xmlns:cc=\"http://creativecommons.org/ns#\" xmlns:rdf=\"http://www.w3.org/1999/02/22-rdf-syntax-ns#\" xmlns:svg=\"http://www.w3.org/2000/svg\" xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 180 180\"><metadata><rdf:RDF><cc:Work><dc:format>image/svg+xml</dc:format><dc:type rdf:resource=\"http://purl.org/dc/dcmitype/StillImage\"/><dc:title>Bots</dc:title><dc:creator><cc:Agent><dc:title>Ranjan Dailata</dc:title></cc:Agent></dc:creator><dc:source>https://bottts.com/</dc:source><cc:license rdf:resource=\"https://bottts.com/\"/><dc:contributor><cc:Agent><dc:title>Florian Körner</dc:title></cc:Agent></dc:contributor></cc:Work></rdf:RDF></metadata><rect fill=\"transparent\" width=\"180\" height=\"180\" x=\"0\" y=\"0\"/>");

            // Build the Body
            botStringBuilder.Append(Group(0, 66, sidesPart, isDisplaySide));
            botStringBuilder.Append(Group(41, 0, topPart, isDisplayTop));
            botStringBuilder.Append(Group(25, 44, ApplyFaceColor(facePart, primaryColor,
                    isDisplayTexture ? texturePart : "undefined"), isDisplaySide));
            botStringBuilder.Append(Group(52, 124, mouthPart, isDisplayMouth));
            botStringBuilder.Append(Group(38, 76, eyesPart, isDisplayEye));

            botStringBuilder.Append("</svg>");

            string finalSVG = botStringBuilder.ToString();
            return finalSVG;
        }
    }
}
