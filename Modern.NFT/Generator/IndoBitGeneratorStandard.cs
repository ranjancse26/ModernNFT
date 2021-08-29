using Svg;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace Modern.NFT.Generator
{
    public class IndoBitGeneratorStandard : BaseAvatar
    {
        private string exePath;
        private string indoBitFolderPath;

        public IndoBitGeneratorStandard()
        {
            exePath = Path.GetDirectoryName(
              Assembly.GetExecutingAssembly().Location);

            indoBitFolderPath = $"{exePath}\\Generator\\IndoBits";
        }

        private string ApplyColor(string triat, string color)
        {
            return triat.Replace("${color}", $"fill:{color}");
        }

        private List<string> GetBodies()
        {
            var bodies = new List<string>
            {
                "body1",
                "body2",
                "body3",
                "body4"
            };
            return bodies;
        }

        private List<string> GetLayers()
        {
            var layers = new List<string>
            {
                "face1",
                "face2",
                "face3",
                "face4",
                "face5",
                "face6",
                "face7",
                "face8",
            };
            return layers;
        }

        private List<string> GetBackgrounds()
        {
            var backgrounds = new List<string>
            {
                "background1",
                "background2",
                "background3",
                "background4",
                "background5",
                "background6",
                "background7",
                "background8",
                "background9",
                "background10",
                "background11",
                "background12",
                "background13",
                "background14",
                "background15",
                "background16"
            };
            return backgrounds;
        }

        public void GenerateBots()
        {
            List<string> backgrounds = GetBackgrounds();
            List<string> layers = GetLayers();
            List<string> bodies = GetBodies();

            string exePath = Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().Location);

            string indoBitsLocation = $"{exePath}\\IndoBits";

            if (!Directory.Exists(indoBitsLocation))
                Directory.CreateDirectory(indoBitsLocation);

            string indoBitsLayered = $"{indoBitFolderPath}\\IndoBitsLayered.svg";

            int index = 1;

            foreach (var body in bodies)
            {
                Generate(backgrounds, layers, body,
                    exePath, indoBitsLayered, ref index);
            }
        }

        private void Generate(List<string> backgrounds, 
            List<string> layers, string body,
            string exePath, string indoBitsLayered, 
            ref int index)
        {
            foreach (var background in backgrounds)
            {
                for (int i = 0; i < layers.Count; i++)
                {
                    var svgDocument = SvgDocument.Open(indoBitsLayered);

                    RemoveFaceExcept(layers[i], svgDocument);

                    RemoveBackGroundExcept(background, svgDocument);

                    RemoveBodyExcept(body, svgDocument);

                    var smallBitmap = svgDocument.Draw(750, 750);
                    smallBitmap.Save($"{exePath}\\indoBits\\indoBit-{index}.png");
                    index++;
                }
            }
        }

        private void RemoveFaceExcept(string name, SvgDocument svgDocument)
        {
            var childs = svgDocument.Children.ToList();
            for (int idx = 0; idx < childs.Count; idx++)
            {
                var element = childs[idx];
                var attr = element.CustomAttributes;
                if (attr.Count == 2)
                {
                    if (!attr["label"].ToLower().Equals(name) &&
                        !attr["label"].ToLower().Contains("background") &&
                        !attr["label"].ToLower().Contains("body"))
                    {
                        svgDocument.Children.Remove(element);
                    }
                }
            }
        }

        private void RemoveBackGroundExcept(string name, SvgDocument svgDocument)
        {
            var childs = svgDocument.Children.ToList();
            for (int idx = 0; idx < childs.Count; idx++)
            {
                var element = childs[idx];
                var attr = element.CustomAttributes;
                if (attr.Count == 2)
                {
                    if (!attr["label"].ToLower().Equals(name) &&
                        !attr["label"].ToLower().Contains("face") &&
                        !attr["label"].ToLower().Contains("body"))
                    {
                        svgDocument.Children.Remove(element);
                    }
                }
            }
        }

        private void RemoveBodyExcept(string name, SvgDocument svgDocument)
        {
            var childs = svgDocument.Children.ToList();
            for (int idx = 0; idx < childs.Count; idx++)
            {
                var element = childs[idx];
                var attr = element.CustomAttributes;
                if (attr.Count == 2)
                {
                    if (!attr["label"].ToLower().Equals(name) &&
                        !attr["label"].ToLower().Contains("face") &&
                        !attr["label"].ToLower().Contains("background"))
                    {
                        svgDocument.Children.Remove(element);
                    }
                }
            }
        }
    }
}
