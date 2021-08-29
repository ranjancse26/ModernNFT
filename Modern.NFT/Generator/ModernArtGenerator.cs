using System;
using System.IO;
using System.Xml;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using AForge.Imaging.Filters;
using System.Drawing;
using AForge.Imaging.ColorReduction;

namespace Modern.NFT.Generator
{
    public interface IModernArtGenerator
    {
        void Generate(string uniqueId,
            int count,
            GeometricStyle geometricStyle,
            ImageFilter imageFilter = null,
            bool withHat = false,
            bool withGlass = false,
            bool withMustashe = false,
            bool withMask = false,
            bool withSmoke = false);
    }

    public class ImageFilter
    {
        public bool ApplyOilPaint { get; set; }
        public bool ApplyJitter { get; set; }
        public bool ApplyGrayScale { get; set; }
        public bool ApplySepia { get; set; }
        public bool ApplyHue { get; set; }
        public bool ApplyRotateChannel { get; set; }
        public bool ApplyInvert { get; set; }
        public bool ApplyColorImageQuantizer { get; set; }
    }

    public enum GlassStyle
    {
        Glass1,
        Glass2,
        Glass3,
        Glass4,
        Glass5,
        Glass6
    }

    public enum GeometricStyle
    {
        Geometric1,
        Geometric2,
        Geometric3,
        Geometric4,
        Geometric5,
        Geometric6,
        Geometric7,
        Geometric8,
        Geometric9,
        Rectangle
    }

    public enum ModernArtEnum
    {
        Mask,
        Dot,
        Chain,
        HeadPhone,
        Smoke,
        Hat,
        Glass,
        Mustashe
    }

    public class ModernArtGenerator : BaseAvatar, IModernArtGenerator
    {
        private string exePath;
        private string modernArtBaseFolderPath;

        private int height;
        private int width;

        public ModernArtGenerator(int height, int width)
        {
            this.height = height;
            this.width = width;

            exePath = Path.GetDirectoryName(
              Assembly.GetExecutingAssembly().Location);

            modernArtBaseFolderPath = $"{exePath}\\Generator\\ModernArt";

            GetAllPaths();
        }

        private List<string> GetGNodes(string xml)
        {
            List<string> gPaths = new List<string>();

            // Encode the XML string in a UTF-8 byte array
            byte[] encodedString = Encoding.UTF8.GetBytes(xml);

            // Put the byte array into a stream and rewind it to the beginning
            MemoryStream ms = new MemoryStream(encodedString);
            ms.Flush();
            ms.Position = 0;

            // Build the XmlDocument from the MemorySteam of UTF-8 encoded bytes
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(ms);

            XmlNode node = xmlDoc.SelectSingleNode("root");
            XmlNodeList gNodes = node.SelectNodes("g");

            foreach (XmlNode gNode in gNodes)
            {
                gPaths.Add(gNode.OuterXml);
            }

            return gPaths;
        }

        private List<string> ChainPaths = new List<string>();
        private List<string> DotPaths = new List<string>();
        private List<string> GlassPaths = new List<string>();
        private List<string> HatPaths = new List<string>();
        private List<string> HeadPhonePaths = new List<string>();
        private List<string> MaskPaths = new List<string>();
        private List<string> SmokePaths = new List<string>();
        private List<string> MustashePaths = new List<string>();

        private void GetAllPaths()
        {
            var paths = File.ReadAllText($"{modernArtBaseFolderPath}\\Chain.svg");
            ChainPaths.AddRange(GetGNodes(paths));

            paths = File.ReadAllText($"{modernArtBaseFolderPath}\\Dot.svg");
            DotPaths.AddRange(GetGNodes(paths));

            paths = File.ReadAllText($"{modernArtBaseFolderPath}\\Glass.svg");
            GlassPaths.AddRange(GetGNodes(paths));

            paths = File.ReadAllText($"{modernArtBaseFolderPath}\\Hat.svg");
            HatPaths.AddRange(GetGNodes(paths));

            paths = File.ReadAllText($"{modernArtBaseFolderPath}\\Headphone.svg");
            HeadPhonePaths.AddRange(GetGNodes(paths));

            paths = File.ReadAllText($"{modernArtBaseFolderPath}\\Mask.svg");
            MaskPaths.AddRange(GetGNodes(paths));

            paths = File.ReadAllText($"{modernArtBaseFolderPath}\\Smoke.svg");
            SmokePaths.AddRange(GetGNodes(paths));

            paths = File.ReadAllText($"{modernArtBaseFolderPath}\\Mustashe.svg");
            MustashePaths.AddRange(GetGNodes(paths));
        }

        private string GetBaseLayer()
        {
            var baseLayer = File.ReadAllText($"{modernArtBaseFolderPath}" +
                $"\\Layer.svg");
            return baseLayer;
        }

        private string GetRandomPath(ModernArtEnum modernArtEnum)
        {
            List<string> filteredPaths = new List<string>();

            switch (modernArtEnum)
            {
                case ModernArtEnum.Chain:
                    filteredPaths = ChainPaths;
                    break;
                case ModernArtEnum.Dot:
                    filteredPaths = DotPaths;
                    break;
                case ModernArtEnum.Glass:
                    filteredPaths = GlassPaths;
                    break;
                case ModernArtEnum.Hat:
                    filteredPaths = HatPaths;
                    break;
                case ModernArtEnum.HeadPhone:
                    filteredPaths = HeadPhonePaths;
                    break;
                case ModernArtEnum.Mask:
                    filteredPaths = MaskPaths;
                    break;
                case ModernArtEnum.Smoke:
                    filteredPaths = SmokePaths;
                    break;
                case ModernArtEnum.Mustashe:
                    filteredPaths = MustashePaths;
                    break;
            }

            var randomObject = new Random();
            var randomIndex = randomObject.Next(0,
                filteredPaths.Count());

            return filteredPaths[randomIndex].Replace("\n", "");
        }

        public void Generate(
            string uniqueId,
            int count,
            GeometricStyle geometricStyle,
            ImageFilter imageFilter = null,
            bool withHat = false,
            bool withGlass = false,
            bool withMustashe = false,
            bool withMask = false,
            bool withSmoke = false)
        {
            var random = new Random();
            var colorSets = new Dictionary<int, List<string>>
            {
                {0, new List<string>{"#660099", "#95F071"  } },
                {1, new List<string>{ "#CCCCCC", "#3F3F3F" } },
                {2, new List<string>{ "#660099", "#95F071" } },
                {3, new List<string>{ "#0000FF", "#FFFF00" } },
                {4, new List<string>{ "#FFFFCC", "#040526" } },
                {5, new List<string>{ "#FFFFFF", "#020100"  } },
                {6, new List<string>{ "#999966", "#4E6F80" } },
                {7, new List<string>{ "#FFCC00", "#0033EE" } },
                {8, new List<string>{ "#FF6699", "#149E70" } },
                {9, new List<string>{ "#66CC33", "#B040D9" } },
                {10, new List<string>{ "#FFFFCC", "#020B23" } },
                {11, new List<string>{ "#3399CC", "#B6622F" } },
                {12, new List<string>{ "#FF9933", "#1456C8" } },
                {13, new List<string>{ "#CC0000", "#1EEDE7" } },
                {14, new List<string>{ "#666633", "#90B1C8" } },
                {15, new List<string>{ "#33CC99", "#B7406E" } },
                {16, new List<string>{ "#CCCCCC", "#3C3838" } },
                {17, new List<string>{ "#336699", "#BB9452" } },
                {18, new List<string>{ "#333366", "#C9BAB0" } },
                {19, new List<string>{ "#FF0033", "#19FFD9" } },
                {20, new List<string>{ "#99FF99", "#6A175C" } },
                {21, new List<string>{ "#6600FF", "#AAFF00" } }
            };

            var maskColorSets1 = GeneticAlgoHelper.Generate(24, count);
            var maskColorSets2 = GeneticAlgoHelper.Generate(24, count);
            var bgColorSets1 = GeneticAlgoHelper.Generate(24, count);
            var glassColorSets = GeneticAlgoHelper.Generate(24, count);

            for (int i = 0; i < count; i++)
            {
                var genomeColorSets = GeneticAlgoHelper.Generate(24, 10);
                var modernArtStringBuilder = new StringBuilder();

                string baseLayer = GetBaseLayer();
                baseLayer = SetBaseLayerBackground(geometricStyle, baseLayer);

                baseLayer = baseLayer.Replace("#{color1}", genomeColorSets[0]);
                baseLayer = baseLayer.Replace("#{color2}", genomeColorSets[1]);
                baseLayer = baseLayer.Replace("#{color3}", genomeColorSets[2]);
                baseLayer = baseLayer.Replace("#{color4}", genomeColorSets[3]);
                baseLayer = baseLayer.Replace("#{color5}", genomeColorSets[4]);
                baseLayer = baseLayer.Replace("#{color6}", genomeColorSets[5]);
                baseLayer = baseLayer.Replace("#{color7}", genomeColorSets[6]);
                baseLayer = baseLayer.Replace("#{color8}", genomeColorSets[7]);
                baseLayer = baseLayer.Replace("#{color9}", genomeColorSets[7]);

                baseLayer = baseLayer.Replace("#{bgColor}", bgColorSets1[i]);
                baseLayer = baseLayer.Replace("#{RectangleColor}", bgColorSets1[i]);
              
                modernArtStringBuilder.AppendLine(baseLayer);

                // Mask
                if (withMask)
                {
                    var randomMask = GetRandomPath(ModernArtEnum.Mask);
                    randomMask = randomMask.Replace("#{coloredMask1}",
                            maskColorSets1[i]);
                    randomMask = randomMask.Replace("#{coloredMask2}",
                        maskColorSets2[i]);

                    modernArtStringBuilder.AppendLine(randomMask);
                }

                // Glass
                if (withGlass)
                {
                    var randomGlass = GetRandomPath(ModernArtEnum.Glass);
                    randomGlass = randomGlass.Replace("#{GlassColor}", glassColorSets[i]);
                    modernArtStringBuilder.AppendLine(randomGlass);
                }

                // Dot
                var randomDot = GetRandomPath(ModernArtEnum.Dot);
                modernArtStringBuilder.AppendLine(randomDot);

                // Chain
                var randomChain = GetRandomPath(ModernArtEnum.Chain);
                modernArtStringBuilder.AppendLine(randomChain);

                // Headphone
                var randomHeadphone = GetRandomPath(ModernArtEnum.HeadPhone);
                modernArtStringBuilder.AppendLine(randomHeadphone);

                // Smoke
                if (withSmoke)
                {
                    var randomSmoke = GetRandomPath(ModernArtEnum.Smoke);
                    modernArtStringBuilder.AppendLine(randomSmoke);
                }

                // Hat
                if (withHat)
                {
                    var randomHat = GetRandomPath(ModernArtEnum.Hat);
                    modernArtStringBuilder.AppendLine(randomHat);
                }

                // Mustashe
                if (withMustashe)
                {
                    var randomMustashe = GetRandomPath(ModernArtEnum.Mustashe);
                    modernArtStringBuilder.AppendLine(randomMustashe);
                }

                string traitsString = modernArtStringBuilder.ToString();
                string finalSVGString = traitsString + "</svg>";

                string modernArtSVGPath = $"modernArt-{i}-{uniqueId}.svg";
                File.WriteAllText(modernArtSVGPath,
                    finalSVGString);

                if (!Directory.Exists($"{exePath}\\ModernArt\\{uniqueId}"))
                    Directory.CreateDirectory($"{exePath}\\ModernArt\\{uniqueId}");

                string bmpPath = $"{exePath}\\ModernArt\\{uniqueId}\\modernArt-{i}.jpg";
                SVGToBitmap(modernArtSVGPath,
                       bmpPath, height, width);

                File.Delete(modernArtSVGPath);

                string newBmpPath = $"{exePath}\\ModernArt\\{uniqueId}\\modernArt-{i}.png";
                Bitmap bitMap = (Bitmap)Bitmap.FromFile(bmpPath);
              
                if(imageFilter != null)
                {
                    if (imageFilter.ApplyOilPaint)
                    {
                        OilPainting oilPaintFilter = new OilPainting(3);
                        oilPaintFilter.ApplyInPlace(bitMap);
                    }
                    if (imageFilter.ApplyJitter)
                    {
                        Jitter jitterFilter = new Jitter(1);
                        jitterFilter.ApplyInPlace(bitMap);
                    }
                    if (imageFilter.ApplyGrayScale)
                    {
                        Grayscale grayScaleFilter = new Grayscale(0.2125, 0.7154, 0.0721);
                        var grayScaleBitmap = grayScaleFilter.Apply(bitMap);
                        grayScaleBitmap.Save(newBmpPath);
                        continue;
                    }
                    if (imageFilter.ApplySepia)
                    {
                        Sepia filter = new Sepia();
                        filter.ApplyInPlace(bitMap);
                    }
                    if (imageFilter.ApplyHue)
                    {
                        HueModifier filter = new HueModifier(random.Next(50,180));
                        filter.ApplyInPlace(bitMap);
                    }
                    if (imageFilter.ApplyRotateChannel)
                    {
                        RotateChannels filter = new RotateChannels();
                        filter.ApplyInPlace(bitMap);
                    }
                    if (imageFilter.ApplyInvert)
                    {
                        Invert filter = new Invert();
                        filter.ApplyInPlace(bitMap);
                    }
                    if (imageFilter.ApplyColorImageQuantizer)
                    {
                        ColorImageQuantizer ciq = new ColorImageQuantizer(new MedianCutQuantizer());
                        Color[] colorTable = ciq.CalculatePalette(bitMap, 16);
                        FloydSteinbergColorDithering dithering = new FloydSteinbergColorDithering();
                        dithering.ColorTable = colorTable;
                        Bitmap newImage = dithering.Apply(bitMap);
                        newImage.Save(newBmpPath);
                        continue;
                    }

                    bitMap.Save(newBmpPath);
                }
            }
        }

        private static string SetBackgroundDisplay(int number, string baseLayer)
        {
            for(int i=1; i<= 9; i++)
            {
                if(i != number)
                    baseLayer = baseLayer.Replace(
                        "{Geometric{0}Style}".Replace("{0}", i.ToString()), 
                        "none");
            }
            baseLayer = baseLayer.Replace(
                      "{Geometric{0}Style}".Replace("{0}", number.ToString()),
                      "inline");
            return baseLayer;
        }

        private static string SetBaseLayerBackground(GeometricStyle geometricStyle, string baseLayer)
        {
            // Geometric 5,6,7 - These are backgrounds

            if(geometricStyle == GeometricStyle.Rectangle)
            {
                baseLayer = baseLayer.Replace("{RectangleStyle}", "inline");
                baseLayer = baseLayer.Replace("{Geometric1Style}", "none");
                baseLayer = baseLayer.Replace("{Geometric2Style}", "none");
                baseLayer = baseLayer.Replace("{Geometric3Style}", "none");
                baseLayer = baseLayer.Replace("{Geometric4Style}", "none");
                baseLayer = baseLayer.Replace("{Geometric5Style}", "none");
                baseLayer = baseLayer.Replace("{Geometric6Style}", "none");
                baseLayer = baseLayer.Replace("{Geometric7Style}", "none");
                baseLayer = baseLayer.Replace("{Geometric8Style}", "none");
                baseLayer = baseLayer.Replace("{Geometric9Style}", "none");
                return baseLayer;
            }

            baseLayer = baseLayer.Replace("{RectangleStyle}", "none");

            switch (geometricStyle)
            {
                case GeometricStyle.Geometric1:
                    baseLayer = SetBackgroundDisplay(1, baseLayer);
                    break;
                case GeometricStyle.Geometric2:
                    baseLayer = SetBackgroundDisplay(2, baseLayer);
                    break;
                case GeometricStyle.Geometric3:
                    baseLayer = SetBackgroundDisplay(3, baseLayer);
                    break;
                case GeometricStyle.Geometric4:
                    baseLayer = SetBackgroundDisplay(4, baseLayer);
                    break;
                case GeometricStyle.Geometric5:
                    baseLayer = SetBackgroundDisplay(5, baseLayer);
                    break;
                case GeometricStyle.Geometric6:
                    baseLayer = SetBackgroundDisplay(6, baseLayer);
                    break;
                case GeometricStyle.Geometric7:
                    baseLayer = SetBackgroundDisplay(7, baseLayer);
                    break;
                case GeometricStyle.Geometric8:
                    baseLayer = SetBackgroundDisplay(8, baseLayer);
                    break;
                case GeometricStyle.Geometric9:
                    baseLayer = SetBackgroundDisplay(9, baseLayer);
                    break;
            }

            return baseLayer;
        }
    }
}
