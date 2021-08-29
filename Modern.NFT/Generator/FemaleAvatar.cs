using System;
using System.Text;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Modern.NFT.Generator
{
    public class FemaleAvatar : BaseAvatar
    {
        private string GetRandomMood()
        {
            var moods = new List<string>
            {
                "sad",
                "happy",
                "surprised"
            };

            var random = new Random();
            int randomhatColor = random.Next(0, moods.Count - 1);
            return moods[randomhatColor];
        }

        private string GetRandomEye()
        {
            var eyesColors = new List<Color>
            {
                 HexToColor("#FFDBAC"),
                 HexToColor("#F5CFA0"),
                 HexToColor("#EAC393"),
                 HexToColor("#E0B687"),
                 HexToColor("#CB9E6E"),
                 HexToColor("#B68655"),
                 HexToColor("#A26D3D"),
                 HexToColor("#8D5524")
            };

            var random = new Random();
            int randomhatColor = random.Next(0, eyesColors.Count - 1);
            string hexColor = ColorToHex(eyesColors[randomhatColor]);

            List<string> eyes = new List<string>
            {
                "<path d=\"M5 9V7h3v2H5zm7-2h3v2h-3V7z\" fill=\"#FFF\"/><path d=\"M7 8v1h1V8H7zm7 0h1v1h-1V8z\" fill=\"${eyesColor.hex}\"/>",
                "<path d=\"M5 7h3v2H5V7zm7 0h3v2h-3V7z\" fill=\"#FFF\"/><path d=\"M6 8h1v1H6V8zm7 1V8h1v1h-1z\" fill=\"${eyesColor.hex}\"/>",
                "<path d=\"M5 7h3v2H5V7zm7 0h3v2h-3V7z\" fill=\"#FFF\"/><path d=\"M7 8h1v1H7V8zm5 0h1v1h-1V8z\" fill=\"${eyesColor.hex}\"/>",
                "<path d=\"M6 7h1v1h1v1H6V7zm6 0h1v1h1v1h-2V7z\" fill=\"#FFF\"/><path d=\"M6 8h1v1H6V8zm6 0h1v1h-1V8z\" fill=\"${eyesColor.hex}\"/>",
                "<path d=\"M5 8h2v1H5V8zm7 0h2v1h-2V8z\" fill=\"#FFF\"/><path d=\"M7 8h1v1H7V8zm7 0h1v1h-1V8z\" fill=\"${eyesColor.hex}\"/>",
                "<path d=\"M6 8h1v1H6V8zm7 0h1v1h-1V8z\" fill=\"#FFF\"/><path d=\"M7 8h1v1H7V8zm5 0h1v1h-1V8z\" fill=\"${eyesColor.hex}\"/>",
                "<path d=\"M5 7v1h3V7H5zm7 0h3v1h-3V7z\" fill=\"#FFF\"/><path d=\"M5 9V8h1V7h1v1h1v1H5zm7 0V8h1V7h1v1h1v1h-3z\" fill=\"${eyesColor.hex}\"/><path d=\"M5 9V8h1V7h1v1h1v1H7V8H6v1H5zm7 0V8h1V7h1v1h1v1h-1V8h-1v1h-1z\" fill=\"#FFF\" fill-opacity=\".5\"/>",
                "<path d=\"M5 8h3v1H5V8zm7 0h3v1h-3V8z\" fill=\"#FFF\"/><path d=\"M6 8h1v1H6V8zm7 0h1v1h-1V8z\" fill=\"${eyesColor.hex}\"/>",
                "<path d=\"M5 7h3v2H5V7zm7 0h3v2h-3V7z\" fill=\"#FFF\"/><path d=\"M5 8h2v1H5V8zm7 0h2v1h-2V8z\" fill=\"${eyesColor.hex}\"/>",
                "<path d=\"M5 7h3v3H5V7zm7 0h3v3h-3V7z\" fill=\"#FFF\"/><path d=\"M6 8h1v1H6V8zm7 0h1v1h-1V8z\" fill=\"${eyesColor.hex}\"/>",
                "<path d=\"M5 7h3v3H5V7zm7 0h3v3h-3V7z\" fill=\"#FFF\"/><path d=\"M6 7h2v2H6V7zm7 0h2v2h-2V7z\" fill=\"${eyesColor.hex}\"/><path d=\"M6 7v1h1v1h1V8H7V7H6zm7 0v1h1v1h1V8h-1V7h-1z\" fill=\"#FFF\" fill-opacity=\".4\"/><path d=\"M7 7v1h1V7H7zm7 0h1v1h-1V7z\" fill=\"#FFF\" fill-opacity=\".7\"/>",
                "<path d=\"M5 7h3v3H5V7zm7 0h3v3h-3V7z\" fill=\"#FFF\"/><path d=\"M5 8h2v1H5V8zm7 0h2v1h-2V8z\" fill=\"${eyesColor.hex}\"/><path d=\"M5 8h1v1H5V8zm7 0h1v1h-1V8z\" fill=\"#FFF\" fill-opacity=\".7\"/>",
                "<path d=\"M6 7h1v2H5V8h1V7zm7 0h1v2h-2V8h1V7z\" fill=\"#FFF\"/><path d=\"M7 7v1H6v1h2V7H7zm7 0v1h-1v1h2V7h-1z\" fill=\"${eyesColor.hex}\"/><path d=\"M7 7v1h1V7H7zM6 8v1h1V8H6zm8-1v1h1V7h-1zm-1 1v1h1V8h-1z\" fill=\"#FFF\" fill-opacity=\".5\"/>"
            };

            int maxLength = eyes.Count;
            int randomVal = random.Next(0, maxLength - 1);
            return eyes[randomVal].Replace("${eyesColor.hex}", hexColor);
        }

        private string GetRandomEyeBrows()
        {
            var eyeBrowColors = new List<Color>
            {
                  HexToColor("#76778b"),
                  HexToColor("#697b94"),
                  HexToColor("#647b90"),
                  HexToColor("#5b7c8b"),
                  HexToColor("#588387"),
            };

            var random = new Random();
            int randomhatColor = random.Next(0, eyeBrowColors.Count - 1);
            string hexColor = ColorToHex(eyeBrowColors[randomhatColor]);

            List<string> eyeBrows = new List<string>
            {
                "<path d=\"M7 5v1H5v1H4V6h1V5h2zm7 0v1h-2v1h-1V6h1V5h2z\" fill-rule=\"evenodd\" fill=\"${eyebrowsColor.hex}\"/>",
                "<path d=\"M8 4v1H7v1H5V5h2V4h1zm4 0h1v1h2v1h-2V5h-1V4z\" fill-rule=\"evenodd\" fill=\"${eyebrowsColor.hex}\"/>",
                "<path d=\"M6 5h3v2H8V6H6V5zm5 0h3v1h-2v1h-1V5z\" fill-rule=\"evenodd\" fill=\"${eyebrowsColor.hex}\"/>",
                "<path d=\"M5 5h3v1h1v1H8V6H5V5zm10 0h-3v1h-1v1h1V6h3V5z\" fill-rule=\"evenodd\" fill=\"${eyebrowsColor.hex}\"/>",
                "<path d=\"M6 5H4v2h1V6h1V5zm8 0h2v2h-1V6h-1V5z\" fill-rule=\"evenodd\" fill=\"${eyebrowsColor.hex}\"/>",
                "<path d=\"M5 6h2v1H5V6zm8 0h2v1h-2V6z\" fill-rule=\"evenodd\" fill=\"${eyebrowsColor.hex}\"/>",
                "<path d=\"M6 5h1v1h1v1H5V6h1V5zm7 0h1v1h1v1h-3V6h1V5z\" fill-rule=\"evenodd\" fill=\"${eyebrowsColor.hex}\"/>",
                "<path d=\"M6 5h2v1h1v1H8V6H6V5zm8 0h-2v1h-1v1h1V6h2V5z\" fill-rule=\"evenodd\" fill=\"${eyebrowsColor.hex}\"/>",
                "<path d=\"M12 7V6h1V5h1v1h1v1h-1V6h-1v1h-1zM5 7V6h1V5h1v1h1v1H7V6H6v1H5z\" fill-rule=\"evenodd\" fill=\"${eyebrowsColor.hex}\"/>",
                "<path d=\"M7 5v1H5v1H4V6h1V5h2zm6 0h2v1h1v1h-1V6h-2V5z\" fill-rule=\"evenodd\" fill=\"${eyebrowsColor.hex}\"/>",
                "<path d=\"M4 7V5h3v1H5v1H4zm12-2v2h-1V6h-2V5h3z\" fill-rule=\"evenodd\" fill=\"${eyebrowsColor.hex}\"/>",
                "<path d=\"M7 5h1v1h1v1H8V6H7V5zm6 0v1h-1v1h-1V6h1V5h1z\" fill-rule=\"evenodd\" fill=\"${eyebrowsColor.hex}\"/>",
                "<path d=\"M4 7V6h1V5h1v1H5v1H4zm10-2h1v1h1v1h-1V6h-1V5z\" fill-rule=\"evenodd\" fill=\"${eyebrowsColor.hex}\"/>"
            };

            int maxLength = eyeBrows.Count;
            int randomVal = random.Next(0, maxLength - 1);
            return eyeBrows[randomVal].Replace("${eyebrowsColor.hex}", hexColor);
        }

        private string GetRandomAccessories()
        {
            var accessoriesColors = new List<Color>
            {
                  HexToColor("#daa520"),
                  HexToColor("#ffd700"),
                  HexToColor("#eee8aa"),
                  HexToColor("#fafad2"),
                  HexToColor("#d3d3d3"),
                  HexToColor("#a9a9a9"),
            };

            var random = new Random();
            int randomhatColor = random.Next(0, accessoriesColors.Count - 1);
            string hexColor = ColorToHex(accessoriesColors[randomhatColor]);

            List<string> accessories = new List<string>
            {
              "<path d=\"M2 9v1h1V9H2zm15 0v1h1V9h-1z\" fill-rule=\"evenodd\" fill=\"${accessoriesColor.hex}\"/>",
              "<path d=\"M2 9v2h1V9H2zm15 0h1v2h-1V9z\" fill-rule=\"evenodd\" fill=\"${accessoriesColor.hex}\"/>",
              "<path d=\"M2 9v2h1V9H2zm15 0h1v2h-1V9z\" fill=\"${accessoriesColor.hex}\"/><path d=\"M2 9v1h1V9H2zm15 0h1v1h-1V9z\" fill=\"#FFF\" fill-opacity=\".4\"/>",
              "<path d=\"M1 9v3h3V9H1zm1 1v1h1v-1H2zm14-1v3h3V9h-3zm1 1v1h1v-1h-1z\" fill-rule=\"evenodd\" fill=\"${accessoriesColor.hex}\"/>",
            };

            int maxLength = accessories.Count;
            int randomVal = random.Next(0, maxLength - 1);
            return accessories[randomVal].Replace("${accessoriesColor.hex}", hexColor);
        }

        private string GetRandomGlasses()
        {
            var glassesColors = new List<Color>
            {
                  HexToColor("#5f705c"),
                  HexToColor("#43677d"),
                  HexToColor("#5e172d"),
                  HexToColor("#ffb67a"),
                  HexToColor("#a04b5d"),
                  HexToColor("#191919"),
                  HexToColor("#323232"),
                  HexToColor("#4b4b4b")
            };

            var random = new Random();
            int randomhatColor = random.Next(0, glassesColors.Count - 1);
            string hexColor = ColorToHex(glassesColors[randomhatColor]);

            List<string> glasses = new List<string>
            {
              "<path d=\"M3 8V7h1V6h2v1h1V6h2v1h2V6h2v1h1V6h2v1h1v1h-1v1h-1v1h-1v1h-1v-1h-1V9h-1V8H9v1H8v1H7v1H6v-1H5V9H4V8H3z\" fill=\"${glassesColor.hex}\"/><path d=\"M3 7v1h1V7h1V6H4v1H3zm5-1v1h1v1h2V7h1V6h-1v1H9V6H8zm7 0v1h1v1h1V7h-1V6h-1z\" fill-rule=\"evenodd\" fill=\"#FFF\" fill-opacity=\".2\"/>",
              "<path d=\"M5 7h3v3H5V7zm7 0h3v3h-3V7z\" fill-rule=\"evenodd\" fill=\"#FFF\" fill-opacity=\".2\"/><path d=\"M7 7h1v1H7V7zm7 0h1v1h-1V7z\" fill-rule=\"evenodd\" fill=\"#FFF\" fill-opacity=\".2\"/><path d=\"M12 10V7h3v3h-3zm-1-4v1H9V6H4v1H3v1h1v3h5V8h2v3h5V8h1V7h-1V6h-5zm-6 4V7h3v3H5z\" fill-rule=\"evenodd\" fill=\"${glassesColor.hex}\"/><path d=\"M3 7h1v1H3V7zm6 0h2v1H9V7zm7 0h1v1h-1V7z\" fill-rule=\"evenodd\" fill=\"#FFF\" fill-opacity=\".2\"/>",
              "<path d=\"M5 7h3v2H5V7zm7 0h3v2h-3V7z\" fill-rule=\"evenodd\" fill=\"#FFF\" fill-opacity=\".2\"/><path d=\"M7 7h1v1H7V7zm7 0h1v1h-1V7z\" fill-rule=\"evenodd\" fill=\"#FFF\" fill-opacity=\".2\"/><path d=\"M5 7v2h3V7H5zM4 6v1H3v1h1v1h1v1h3V9h1V8h2v1h1v1h3V9h1V8h1V7h-1V6h-5v1H9V6H4zm8 1v2h3V7h-3z\" fill-rule=\"evenodd\" fill=\"${glassesColor.hex}\"/><path d=\"M3 7h1v1H3V7zm6 0h2v1H9V7zm7 0h1v1h-1V7z\" fill-rule=\"evenodd\" fill=\"#FFF\" fill-opacity=\".2\"/>",
              "<path d=\"M5 8h3v1H5V8zm7 0h3v1h-3V8z\" fill-rule=\"evenodd\" fill=\"#FFF\" fill-opacity=\".2\"/><path d=\"M7 8h1v1H7V8zm7 0h1v1h-1V8z\" fill-rule=\"evenodd\" fill=\"#FFF\" fill-opacity=\".2\"/><path d=\"M5 8v1h3V8H5zM3 7v1h1v1h1v1h3V9h1V8h2v1h1v1h3V9h1V8h1V7H3zm9 1v1h3V8h-3z\" fill-rule=\"evenodd\" fill=\"${glassesColor.hex}\"/><path d=\"M3 7v1h1V7H3zm6 0v1h2V7H9zm7 0v1h1V7h-1z\" fill-rule=\"evenodd\" fill=\"#FFF\" fill-opacity=\".2\"/>",
              "<path d=\"M5 7h3v2H5V7zm7 0h3v2h-3V7z\" fill-rule=\"evenodd\" fill=\"#FFF\" fill-opacity=\".2\"/><path d=\"M7 7h1v1H7V7zm7 0h1v1h-1V7z\" fill-rule=\"evenodd\" fill=\"#FFF\" fill-opacity=\".2\"/><path d=\"M12 7v2h3V7h-3zM8 6H5v1H3v1h1v1h1v1h3V9h1V8h2v1h1v1h3V9h1V8h1V7h-2V6h-3v1H8V6zM5 7v2h3V7H5z\" fill-rule=\"evenodd\" fill=\"${glassesColor.hex}\"/><path d=\"M3 7h1v1H3V7zm6 0h2v1H9V7zm7 0h1v1h-1V7z\" fill-rule=\"evenodd\" fill=\"#FFF\" fill-opacity=\".2\"/>",
              "<path d=\"M4 8H3V7h1V6h5v1h2V6h5v1h1v1h-1v2h-5V8H9v2H4V8zm1 0V7h3v2H5V8zm7-1v2h3V7h-3z\" fill-rule=\"evenodd\" fill=\"${glassesColor.hex}\"/><path d=\"M5 7h3v2H5V7zm7 0h3v2h-3V7z\" fill-rule=\"evenodd\" fill=\"#FFF\" fill-opacity=\".2\"/><path d=\"M14 7h1v1h-1V7zM7 7h1v1H7V7z\" fill-rule=\"evenodd\" fill=\"#FFF\" fill-opacity=\".2\"/><path d=\"M3 8V7h1v1H3zm6-1v1h2V7H9zm7 0v1h1V7h-1z\" fill-rule=\"evenodd\" fill=\"#FFF\" fill-opacity=\".2\"/>",
              "<path d=\"M4 8H3V7h14v1h-1v2h-5V8H9v2H4V8zm1 0h3v1H5V8zm7 0h3v1h-3V8z\" fill-rule=\"evenodd\" fill=\"${glassesColor.hex}\"/><path d=\"M5 8h3v1H5V8zm7 0h3v1h-3V8z\" fill-rule=\"evenodd\" fill=\"#FFF\" fill-opacity=\".2\"/><path d=\"M7 8v1h1V8H7zm7 0v1h1V8h-1z\" fill-rule=\"evenodd\" fill=\"#FFF\" fill-opacity=\".2\"/><path d=\"M3 7v1h1V7H3zm13 0v1h1V7h-1zM9 7v1h2V7H9z\" fill-rule=\"evenodd\" fill=\"#FFF\" fill-opacity=\".2\"/>",
            };

            int maxLength = glasses.Count;
            int randomVal = random.Next(0, maxLength - 1);
            return glasses[randomVal].Replace("${glassesColor.hex}", hexColor);
        }

        private string GetRandomCloths()
        {
            var clothsColors = new List<Color>
            {
                  HexToColor("#d11141"),
                  HexToColor("#00b159"),
                  HexToColor("#00aedb"),
                  HexToColor("#f37735"),
                  HexToColor("#ffc425"),
                  HexToColor("#740001"),
                  HexToColor("#ae0001"),
                  HexToColor("#eeba30"),
                  HexToColor("#96ceb4"),
                  HexToColor("#ffeead"),
                  HexToColor("#ff6f69"),
                  HexToColor("#ffcc5c"),
                  HexToColor("#88d8b0")
            };

            var random = new Random();
            int randomhatColor = random.Next(0, clothsColors.Count - 1);
            string hexColor = ColorToHex(clothsColors[randomhatColor]);

            List<string> cloths = new List<string>
            {
                "<path d=\"M3 20v-3h1v-1h12v1h1v3H3z\" fill=\"${clothesColor.hex}\"/>",
                "<path d=\"M4 16v4h4v-1H7v-1H6v-1H5v-1H4zm12 0v4h-4v-1h1v-1h1v-1h1v-1h1z\" fill-rule=\"evenodd\" fill=\"${clothesColor.hex}\"/>",
                "<path d=\"M5 16h1v2h1v1h1v1H5v-4zm9 0h1v4h-3v-1h1v-1h1v-2z\" fill-rule=\"evenodd\" fill=\"${clothesColor.hex}\"/>",
                "<path d=\"M4 20v-2h1v-1h1v-1h2v1h1v1h2v-1h1v-1h2v1h1v1h1v2H4z\" fill=\"${clothesColor.hex}\"/>",
                "<path d=\"M3 20v-3h1v-1h4v1h1v1h2v-1h1v-1h4v1h1v3H3z\" fill=\"${clothesColor.hex}\"/><path d=\"M3 20v-3h1v-1h2v1h1v1h1v1h4v-1h1v-1h1v-1h2v1h1v3H3z\" fill=\"#FFF\" fill-opacity=\".2\"/>",
                "<path d=\"M3 20v-3h1v-1h5v1h2v-1h5v1h1v3H3z\" fill=\"${clothesColor.hex}\"/><path d=\"M3 20v-2h1v1h3v1H3zm14 0v-2h-1v1h-3v1h4z\" fill-rule=\"evenodd\" fill=\"#FFF\" fill-opacity=\".4\"/><path d=\"M7 16H4v1H3v1h1v1h3v1h6v-1h3v-1h1v-1h-1v-1h-3v1H7v-1z\" fill=\"#FFF\" fill-opacity=\".2\"/>",
                "<path d=\"M3 20v-3h1v-1h4v1h4v-1h4v1h1v3H3z\" fill=\"${clothesColor.hex}\"/><path d=\"M15 20h2v-3h-1v-1h-4v1H8v-1H4v1H3v3h2v-2h10v2z\" fill=\"#FFF\" fill-opacity=\".4\"/>",
                "<path d=\"M3 20v-3h1v-1h4v1h1v1h2v-1h1v-1h4v1h1v3H3z\" fill=\"${clothesColor.hex}\"/><path d=\"M6 16v1h1v1h1v1h4v-1h1v-1h1v-1h-2v1h-1v1H9v-1H8v-1H6z\" fill=\"#FFF\" fill-opacity=\".4\"/><path d=\"M13 20v-1h2v1h-2zm1-4v1h2v-1h-2zm-8 0H4v1h2v-1z\" fill-rule=\"evenodd\" fill=\"#FFF\" fill-opacity=\".2\"/>",
                "<path d=\"M3 20v-3h1v-1h3v1h1v1h1v1h2v-1h1v-1h1v-1h3v1h1v3H3z\" fill=\"${clothesColor.hex}\"/>",
                "<path d=\"M3 20v-3h1v-1h2v1h1v1h1v1h1v1H3zm14 0v-3h-1v-1h-2v1h-1v1h-1v1h-1v1h6z\" fill-rule=\"evenodd\" fill=\"${clothesColor.hex}\"/>",
                "<path d=\"M4 16v4h4v-1H7v-1H6v-2H4zM16 20v-4h-2v2h-1v1h-1v1h4z\" fill=\"${clothesColor.hex}\"/>",
                "<path d=\"M3 20v-3h1v-1h3v1h1v1h1v1h2v-1h1v-1h1v-1h3v1h1v3H3z\" fill=\"${clothesColor.hex}\"/><path d=\"M6 16v1h1v1h1v1h1v1h2v-1h1v-1h1v-1h1v-1h-1v1h-1v1h-1v1H9v-1H8v-1H7v-1H6z\" fill=\"#FFF\" fill-opacity=\".4\"/><path d=\"M15 16v1h-1v1h-1v1h-1v1h-1v-1h1v-1h1v-1h1v-1h1zM5 16v1h1v1h1v1h1v1h1v-1H8v-1H7v-1H6v-1H5z\" fill-rule=\"evenodd\" fill=\"#FFF\" fill-opacity=\".2\"/><path d=\"M3 20h1v-3h1v1h1v1h1v1h1v-1H7v-1H6v-1H5v-1H4v1H3v3zm14 0v-3h-1v-1h-1v1h-1v1h-1v1h-1v1h1v-1h1v-1h1v-1h1v3h1z\" fill-rule=\"evenodd\" fill=\"#FFF\" fill-opacity=\".1\"/>",
                "<path d=\"M3 20v-3h1v-1h5v1h2v-1h5v1h1v3H3z\" fill=\"${clothesColor.hex}\"/><path d=\"M3 17h14v1H3v-1zm0 2v1h14v-1H3z\" fill-rule=\"evenodd\" fill=\"#FFF\" fill-opacity=\".2\"/>",
            };

            int maxLength = cloths.Count;
            int randomVal = random.Next(0, maxLength - 1);
            return cloths[randomVal].Replace("${clothsColor.hex}", hexColor);
        }

        private string GetRandomHairs()
        {
            var hairColors = new List<Color>
            {
                HexToColor("#090806"),
                HexToColor("#2c222b"),
                HexToColor("#71635a"),
                HexToColor("#b7a69e"),
                HexToColor("#d6c4c2"),
                HexToColor("#cabfb1"),
                HexToColor("#dcd0ba"),
                HexToColor("#fff5e1"),
                HexToColor("#e6cea8"),
                HexToColor("#e5c8a8"),
                HexToColor("#debc99"),
                HexToColor("#b89778"),
                HexToColor("#a56b46"),
                HexToColor("#b55239"),
                HexToColor("#8d4a43"),
                HexToColor("#91553d"),
                HexToColor("#533d32"),
                HexToColor("#3b3024"),
                HexToColor("#554838"),
                HexToColor("#4e433f"),
                HexToColor("#504444"),
                HexToColor("#6a4e42"),
                HexToColor("#a7856a"),
                HexToColor("#977961")
            };

            var random = new Random();
            int randomhatColor = random.Next(0, hairColors.Count - 1);
            string hexColor = ColorToHex(hairColors[randomhatColor]);

            List<string> hairs = new List<string>
            {
                "<path d=\"M2 9v6h2v-4H3V9H2zm0-2h2V4h12v3h2V3h-1V2H3v1H2v4zm15 2h1v6h-2v-4h1V9z\" fill-rule=\"evenodd\" fill=\"${hairColor.hex}\"/>",
                "<path d=\"M4 12h1v1H3V4h1V3h1V2h10v1h1v1h1v9h-2v-1h1V5H4v7z\" fill=\"${hairColor.hex}\"/>",
                "<path d=\"M2 17h2v-1h4v-2H6v-1H5v-1H4V4h1V3h1v1h1V3h1v1h1V3h6v1h1v8h-1v1h-1v1h-2v2h4v1h2V3h-1V2h-1V1H4v1H3v1H2v14z\" fill=\"${hairColor.hex}\"/>",
                "<path d=\"M2 13V9h1v2h1v1h1v1H2zm15-4h1v4h-3v-1h1v-1h1V9zM2 7h1V4h1V3h1V2h10v1h1v1h1v3h1V3h-1V2h-1V1H4v1H3v1H2v4z\" fill-rule=\"evenodd\" fill=\"${hairColor.hex}\"/>",
                "<path d=\"M2 14h2V4h12v10h2V3h-1V2h-1V1H4v1H3v1H2v11z\" fill=\"${hairColor.hex}\"/>",
                "<path d=\"M2 14h2V7h1V6h1V5h1V4h1V3h6v1h1v1h1v9h2V3h-1V2h-1V1H4v1H3v1H2v11z\" fill=\"${hairColor.hex}\"/>",
                "<path d=\"M3 4v3h1V4h2v1h1V4h1v1h1V4h7v3h1V4h1V2h-1V1h-2v1H5V1H3v1H2v2h1z\" fill=\"${hairColor.hex}\"/>",
                "<path d=\"M2 10h1V8h1V7h1V6h1V5h1V4h7v1h3V4h-1V3h-1V2H4v1H3v1H2v6z\" fill=\"${hairColor.hex}\"/>",
                "<path d=\"M1 17h3v-1h4v-2H6v-1H5v-1H4v-1H3V9H2V7h1V5h1V4h7V3h1v1h1V3h1v1h1V3h1v2h1v2h1v2h-1v2h-1v1h-1v1h-1v1h-2v2h4v1h3V4h-1V3h-1V2h-1V1H4v1H3v1H2v1H1v13z\" fill=\"${hairColor.hex}\"/>",
                "<path d=\"M2 13h3v-1H4v-1H3V9H2v4zm13 0h3V9h-1v2h-1v1h-1v1zm2-6h1V3h-1V2h-1V1H4v1H3v1H2v4h1V5h1V4h1V3h1V2h8v1h1v1h1v1h1v2z\" fill-rule=\"evenodd\" fill=\"${hairColor.hex}\"/>",
                "<path d=\"M2 9v3h1v1H2v2H1v1h1v-1h1v-1h1v-1h1v-1H4v-1H3V9H2zm0-2h1V6h1V5h2V4h9v1h1v1h1v1h1V3h-1V2h-1V1H4v1H3v1H2v4z\" fill-rule=\"evenodd\" fill=\"${hairColor.hex}\"/>",
                "<path d=\"M1 15h5v-2H5v-1H4v-1H3V6h2V5h2V4h1V3h6v1h1v1h1v1h1v5h-1v1h-1v1h-1v2h5V4h-1V3h-1V2h-1V1H4v1H3v1H2v1H1v11z\" fill=\"${hairColor.hex}\"/>",
                "<path d=\"M2 9v3h1v1H2v2H1v1h1v-1h1v-1h1v-1h1v-1H4v-1H3V9H2zm16 0v3h-1v1h1v2h1v1h-1v-1h-1v-1h-1v-1h-1v-1h1v-1h1V9h1zm-1-2h1V3h-1V2h-1V1H4v1H3v1H2v4h1V6h1V5h2V4h9v1h1v1h1v1z\" fill-rule=\"evenodd\" fill=\"${hairColor.hex}\"/>",
            };

            int maxLength = hairs.Count;
            int randomVal = random.Next(0, maxLength - 1);
            return hairs[randomVal].Replace("${hairColor.hex}", hexColor);
        }

        private string GetRandomHats()
        {
            var hatsColors = new List<Color>
            {
                  HexToColor("#cc6192"),
                  HexToColor("#2663a3"),
                  HexToColor("#a62116"),
                  HexToColor("#3d8a6b"),
                  HexToColor("#614f8a")
            };

            var random = new Random();
            int randomhatColor = random.Next(0, hatsColors.Count - 1);
            string hexColor = ColorToHex(hatsColors[randomhatColor]);

            List<string> hats = new List<string>
            {
                  "<path d=\"M4 0v2H2v2h16V2h-2V0H4z\" fill=\"${hatColor.hex}\"/>",
                  "<path d=\"M4 3H2v1h16V3h-2V0H4v3z\" fill=\"${hatColor.hex}\"/>",
                  "<path d=\"M2 2v2h16V2h-1V1h-1V0H4v1H3v1H2z\" fill=\"${hatColor.hex}\"/>",
                  "<path d=\"M6 0v1H5v1H4v2h14V3h-2V2h-1V1h-1V0H6z\" fill=\"${hatColor.hex}\"/>",
                  "<path d=\"M2 4V2h2V0h12v2h2v2H2z\" fill=\"${hatColor.hex}\"/><path d=\"M4 0v2h12V0H4z\" fill=\"#FFF\" fill-opacity=\".2\"/>",
                  "<path d=\"M2 4V3h2V0h12v3h2v1H2z\" fill=\"${hatColor.hex}\"/><path d=\"M4 0v3h12V0H4z\" fill=\"#FFF\" fill-opacity=\".2\"/>",
                  "<path d=\"M2 4V2h1V1h1V0h12v1h1v1h1v2H2z\" fill=\"${hatColor.hex}\"/><path d=\"M3 1v1h14V1H3zM2 3v1h16V3H2z\" fill-rule=\"evenodd\" fill=\"#FFF\" fill-opacity=\".2\"/>",
                  "<path d=\"M14 0H6v1H5v1H4v2h14V3h-2V2h-1V1h-1V0z\" fill=\"${hatColor.hex}\"/><path d=\"M5 3h1V2h1V1h1V0H7v1H6v1H5v1z\" fill=\"#FFF\" fill-opacity=\".2\"/>",
                  "<path d=\"M4 0v2H2v2h16V2h-2V0H4z\" fill=\"${hatColor.hex}\"/><path d=\"M15 3V0h-1v3h1z\" fill=\"#FFF\" fill-opacity=\".2\"/>",
                  "<path d=\"M4 0v3H2v1h16V3h-2V0H4z\" fill=\"${hatColor.hex}\"/><path d=\"M15 3V0h-1v3h1zm-2-3v2h-1V0h1z\" fill-rule=\"evenodd\" fill=\"#FFF\" fill-opacity=\".2\"/>",
                  "<path d=\"M2 2v2h16V2h-1V1h-1V0H4v1H3v1H2z\" fill=\"${hatColor.hex}\"/><path d=\"M15 0v4h-1V0h1zm-2 0v4h-1V0h1z\" fill-rule=\"evenodd\" fill=\"#FFF\" fill-opacity=\".2\"/>",
                  "<path d=\"M5 2H4v2h14V3h-2V2h-1V1h-1V0H6v1H5v1z\" fill=\"${hatColor.hex}\"/><path d=\"M14 2h-3v1h3V2z\" fill=\"#FFF\" fill-opacity=\".2\"/>",
            };

            int maxLength = hats.Count;
            int randomVal = random.Next(0, maxLength - 1);
            return hats[randomVal].Replace("${hatColor.hex}", hexColor);
        }

        public void Generate(int count)
        {
            string exePath = Path.GetDirectoryName(
               Assembly.GetExecutingAssembly().Location);

            string avatarFolderPath = string.Format("{0}\\{1}", exePath, "Avatar");
            string avatarResizedFolderPath = string.Format("{0}\\{1}\\Resized", exePath, "Avatar");

            if (Directory.Exists(avatarFolderPath))
                Directory.Delete(avatarFolderPath, true);

            Directory.CreateDirectory(avatarFolderPath);
            Directory.CreateDirectory(avatarResizedFolderPath);

            var backgroundColors = GeneticAlgoHelper.Generate(24, 100);
            var colorSets = GeneticAlgoHelper.Generate(24, 100);

            for (int i = 1; i <= count; i++)
            {
                string finalSVG = Generate(
                  GetRandomMood(),
                  colorSets[i],
                  GetRandomEye(),
                  GetRandomEyeBrows(),
                  GetRandomAccessories(),
                  GetRandomGlasses(),
                  GetRandomCloths(),
                  GetRandomHairs(),
                  GetRandomHats());

                string inputImagePath = $"{avatarFolderPath}\\avatar-{i}.svg";
                string outputImagePath = $"{avatarResizedFolderPath}\\avatar-{i}.jpg";

                System.IO.File.WriteAllText(inputImagePath,
                    finalSVG.Replace("$", "").Trim());

                SVGToBitmap(backgroundColors[i],
                    inputImagePath, outputImagePath, 500, 500);

                File.Delete($"{avatarFolderPath}\\avatar-{i}.svg");
            }
        }
        private string Generate(string mood,
            string randomSkin,
            string randomEye,
            string randomEyeBrow,
            string randomAccessory,
            string randomGlass,
            string randomCloth,
            string randomHair,
            string randomHat)
        {
            var mouthColors = new List<Color>
            {
                HexToColor("#dbac98"),
                HexToColor("#d29985"),
                HexToColor("#c98276"),
                HexToColor("#e35d6a"),
                HexToColor("#e32153"),
                HexToColor("#de0f0d")
            };

            var mouthStringBuilder = new StringBuilder();
            var randomMouthColor = GetRandomColor(mouthColors);

            switch (mood)
            {
                case "sad":
                    mouthStringBuilder
                        .Append($"<path d=\"M9 11v1H8v1h4v-1h-1v-1H9z\" fill=\"${ColorToHex(randomMouthColor)})\"/>");
                    mouthStringBuilder
                       .Append($"<path d=\"M11 11v1H9v1H8v-1h1v-1h2z\" fill=\"${ColorToHex(randomMouthColor)}\"/>");
                    mouthStringBuilder
                       .Append($"<path d=\"M9 12h2v1H9v-1z\" fill=\"${ColorToHex(randomMouthColor)}\"/>");
                    mouthStringBuilder
                       .Append($"<path d=\"M9 12v1h1v1h1v-2H9z\" fill=\"${ColorToHex(randomMouthColor)}\"/>");
                    break;
                case "happy":
                    mouthStringBuilder
                        .Append($"<path d=\"M9 11v2h2v-1h-1v-1H9z\" fill=\"${ColorToHex(randomMouthColor)}\"/><path d=\"M11 13v-1h-1v-1H9v1h1v1h1z\" fill=\"#FFF\" fill-opacity=\".2\"/>");
                    mouthStringBuilder
                      .Append($"<path d=\"M10 11v1H9v1h2v-2h-1z\" fill=\"${ColorToHex(randomMouthColor)}\"/>");
                    mouthStringBuilder
                       .Append($"<path d=\"M8 11v1h1v1h2v-1h1v-1H8z\" fill=\"${ColorToHex(randomMouthColor)}\"/>");
                    mouthStringBuilder
                      .Append($"<path d=\"M9 12v1h2v-1h1v-1h-1v1H9z\" fill=\"${ColorToHex(randomMouthColor)}\"/>");
                    mouthStringBuilder
                       .Append($"<path d=\"M8 11v1h1v1h2v-1H9v-1H8z\" fill=\"${ColorToHex(randomMouthColor)}\"/>");
                    mouthStringBuilder
                      .Append($"<path d=\"M8 12v1h1v1h2v-1h1v-1h-1v-1H9v1H8z\" fill=\"${ColorToHex(randomMouthColor)}\"/><path d=\"M9 12v1h2v-1H9z\" fill=\"#FFF\"/>");
                    mouthStringBuilder
                     .Append($"<path d=\"M8 12v1h1v1h2v-1h1v-1h-1v-1H9v1H8z\" fill=\"${ColorToHex(randomMouthColor)}\"/><path d=\"M9 12v1h2v-1H9z\" fill=\"#FFF\" fill-opacity=\".2\"/>");
                    break;
                case "surprised":
                    mouthStringBuilder
                        .Append($"<path d=\"M9 12v1h1v-1H9z\" fill=\"${ColorToHex(randomMouthColor)}\"/>");
                    mouthStringBuilder
                        .Append($"<path d=\"M9 11v2h2v-2H9z\" fill=\"${ColorToHex(randomMouthColor)}\"/>");
                    break;
            }

            var bodyStringBuilder = new StringBuilder();
         
            // Head
            bodyStringBuilder.Append("<svg xmlns:dc=\"http://purl.org/dc/elements/1.1/\" xmlns:cc=\"http://creativecommons.org/ns#\" xmlns:rdf=\"http://www.w3.org/1999/02/22-rdf-syntax-ns#\" xmlns:svg=\"http://www.w3.org/2000/svg\" xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 20 20\" shape-rendering=\"crispEdges\"><metadata><rdf:RDF><cc:Work><dc:format>image/svg+xml</dc:format><dc:type rdf:resource=\"http://purl.org/dc/dcmitype/StillImage\"/><dc:title>Female / 8biticon</dc:title><dc:creator><cc:Agent><dc:title>Plastic Jam</dc:title></cc:Agent></dc:creator><dc:source>https://github.com/dicebear/dicebear</dc:source><cc:license rdf:resource=\"https://github.com/dicebear/dicebear/blob/v4/packages/avatars-female-sprites/LICENSE\"/><dc:contributor><cc:Agent><dc:title>Florian Körner</dc:title></cc:Agent></dc:contributor></cc:Work></rdf:RDF></metadata>");
            bodyStringBuilder.Append("<rect x=\"10.5\" y=\"10.5\" width=\"150\" height=\"20\" stroke-width=\"1px\" fill=\"none\" stroke=\"black\" />");
            bodyStringBuilder.Append($"<path d=\"M3 20v-3h1v-1h4v-2H6v-1H5v-1H4v-1H3V9H2V7h1V4h1V3h1V2h10v1h1v1h1v3h1v2h-1v2h-1v1h-1v1h-1v1h-2v2h4v1h1v3H3z\" fill =\"${randomSkin}\" /><path d =\"M14 14v-1h1v-1h1v-1h1V9h1V7h-1V4h-1V3h-1V2H5v1H4v1H3v3H2v2h1v2h1v1h1v1h1v1h8z\" fill =\"#FFF\" fill-opacity =\".1\" />");

            // Eye
            bodyStringBuilder.Append(randomEye);

            // Eyebrow
            bodyStringBuilder.Append(randomEyeBrow);

            // Accessories
            bodyStringBuilder.Append(randomAccessory);

            // Mouth
            bodyStringBuilder.Append(mouthStringBuilder.ToString());

            //Glasses
            bodyStringBuilder.Append(randomGlass);

            //Cloths
            bodyStringBuilder.Append(randomCloth);

            //Hair
            bodyStringBuilder.Append(randomHair);

            //Hat
            bodyStringBuilder.Append(randomHat);

            bodyStringBuilder.Append("</svg>");

            string finalSVG = bodyStringBuilder.ToString();
            return finalSVG;
        }
    }
}
