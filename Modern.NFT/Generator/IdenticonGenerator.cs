using Jdenticon;
using Jdenticon.Rendering;
using Modern.NFT.Fake;
using System;
using System.IO;
using System.Reflection;
using Range = Jdenticon.Range;

namespace Modern.NFT.Generator
{
    public class IdenticonGenerator : BaseAvatar
    {
        private string exePath;
        public IdenticonGenerator()
        {
            exePath = Path.GetDirectoryName(
             Assembly.GetExecutingAssembly().Location);
        }

        public void Generate(int count)
        {
            string indiIconPath = $"{exePath}\\IndiIcons";
            if (!Directory.Exists(indiIconPath))
                Directory.CreateDirectory(indiIconPath);

            var colorSets = GeneticAlgoHelper.Generate(count, count);

            var users = FakeUserGenerator.GenerateRandomUsers(count, Bogus.DataSets.Name.Gender.Female);

            int index = 0;
            foreach(var user in users)
            {
                var randomColor = HexToColor(colorSets[index]);
                var style = new IdenticonStyle
                {
                    Hues = new HueCollection { { 252, HueUnit.Degrees } },
                    BackColor = Color.FromRgba(randomColor.R, randomColor.G, randomColor.B, randomColor.A),
                    ColorLightness = Range.Create(0.40f, 0.80f),
                    GrayscaleLightness = Range.Create(0.30f, 0.90f),
                    ColorSaturation = 0.50f,
                    GrayscaleSaturation = 0.00f
                };

                var icon = Identicon.FromValue($"{user.FullName}-{index}", size: 100);
                icon.Style = style;
                icon.SaveAsPng($"{indiIconPath}\\indiIcon -{index}.png");
                index++;
            }
        }
    }
}
