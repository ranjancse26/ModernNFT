using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Reflection;

namespace Modern.NFT.Generator.Avatar
{
    public class FemaleAvatarGenerator : BaseAvatar
    {
        private string exePath = "";
        private string layerPath = "";
        private string femaleLayerPath = "";
        private string femaleBodyFrontAtEasePath = "";
        private string femaleBodyFrontHandOnHipPath = "";
        private string femaleBodyFrontHandFrontDefaultPath = "";

        private string svgBasePath = "";

        string[] files1;
        string[] files2;
        string[] files3;

        public FemaleAvatarGenerator()
        {
            exePath = Path.GetDirectoryName(
               Assembly.GetExecutingAssembly().Location);

            layerPath = string.Format("{0}\\{1}",
               exePath, "Generator\\Avatar\\layer");

            svgBasePath = string.Format("{0}\\{1}",
               layerPath, "svgBase.svg");

            femaleLayerPath = string.Format("{0}\\{1}",
               layerPath, "female");

            femaleBodyFrontAtEasePath = string.Format("{0}\\{1}",
              femaleLayerPath, "body_front_at_ease");

            femaleBodyFrontHandOnHipPath = string.Format("{0}\\{1}",
             femaleLayerPath, "body_front_hand-on-hip");

            femaleBodyFrontHandFrontDefaultPath = string.Format("{0}\\{1}",
             femaleLayerPath, "head_front_default");

            files1 = Directory.GetFiles(femaleBodyFrontAtEasePath);
            files2 = Directory.GetFiles(femaleBodyFrontHandOnHipPath);
            files3 = Directory.GetFiles(femaleBodyFrontHandFrontDefaultPath); ;
        }

        public void Generate(
            int height, int width,
            int count, FemaleAvatarTriat avatarTriat)
        {
            var avatarBackgroundColors = GeneticAlgoHelper.Generate(24, count);
            var random = new Random();

            for (int i = 0; i < count; i++)
            {
                var avatarStringBuilder = new StringBuilder();
                avatarStringBuilder.AppendLine(File.ReadAllText(svgBasePath));

                // Body Default
                var bodyDefault = (from layer in files1
                                    where layer.Contains("body_torso_default")
                                    select layer).FirstOrDefault();
                if(bodyDefault != null)
                    avatarStringBuilder.AppendLine(File.ReadAllText(bodyDefault));

                // Body Forarms
                var bodyforarms = (from layer in files1
                                   where layer.Contains("body_forearm")
                                   select layer);
                foreach (var layer in bodyforarms)
                {
                    avatarStringBuilder.AppendLine(File.ReadAllText(layer));
                }

                // Body Arms
                var bodyarms = (from layer in files1
                                   where layer.Contains("body_arm")
                                   select layer);
                foreach (var layer in bodyarms)
                {
                    avatarStringBuilder.AppendLine(File.ReadAllText(layer));
                }

                // Body Hands
                var bodyHands = from layer in files1
                                     where layer.Contains("body_hand")
                                     select layer;

                foreach (var layer in bodyHands)
                {
                   avatarStringBuilder.AppendLine(File.ReadAllText(layer));
                }

                // Armband
                if (avatarTriat.WithArmband)
                {
                    var armBandLayers = (from layer in files1
                                         where layer.Contains("armband")
                                         select layer).ToList();

                    int randomArmband = random.Next(0, armBandLayers.Count - 1);
                    string armBand = armBandLayers[randomArmband];
                    avatarStringBuilder.AppendLine(File.ReadAllText(
                        armBand));

                    if (armBand.Contains("left"))
                    {
                        avatarStringBuilder.AppendLine(File.ReadAllText(
                            armBand.Replace("left", "right")));
                    }
                    else if (armBand.Contains("right"))
                    {
                        avatarStringBuilder.AppendLine(File.ReadAllText(
                            armBand.Replace("right", "left")));
                    }
                }

                // Coat
                if (avatarTriat.WithCoat)
                {
                    var coatLayers = (from layer in files1
                                      where layer.Contains("coat")
                                      select layer).ToList();

                    int randomCoat = random.Next(0, coatLayers.Count - 1);
                    string coat = coatLayers[randomCoat];
                    avatarStringBuilder.AppendLine(File.ReadAllText(
                        coat));

                    if (coat.Contains("1_of_2"))
                    {
                        avatarStringBuilder.AppendLine(File.ReadAllText(
                            coat.Replace("1_of_2", "2_of_2")));
                    }
                    else if (coat.Contains("2_of_2"))
                    {
                        avatarStringBuilder.AppendLine(File.ReadAllText(
                            coat.Replace("2_of_2", "1_of_2")));
                    }
                }

                // Holster
                if (avatarTriat.WithHolster)
                {
                    var holsterLayers = (from layer in files1
                                         where layer.Contains("holster")
                                         select layer).ToList();

                    int randomHolster = random.Next(0, holsterLayers.Count - 1);
                    string hoslster = holsterLayers[randomHolster];
                    avatarStringBuilder.AppendLine(File.ReadAllText(
                        hoslster));

                    if (hoslster.Contains("1_of_2"))
                    {
                        avatarStringBuilder.AppendLine(File.ReadAllText(
                            hoslster.Replace("1_of_2", "2_of_2")));
                    }
                    else if (hoslster.Contains("2_of_2"))
                    {
                        avatarStringBuilder.AppendLine(File.ReadAllText(
                            hoslster.Replace("2_of_2", "1_of_2")));
                    }
                }

                // Collar
                if (avatarTriat.WithCollar)
                {
                    var collarLayers = (from layer in files1
                                        where layer.Contains("collar")
                                        select layer).ToList();

                    int randomCollar = random.Next(0, collarLayers.Count - 1);
                    avatarStringBuilder.AppendLine(File.ReadAllText(collarLayers[randomCollar]));
                }

                // Necklace
                if (avatarTriat.WithNecklace)
                {
                    var necklaceLayers = (from layer in files1
                                          where layer.Contains("necklace")
                                          select layer).ToList();

                    int randomNecklace = random.Next(0, necklaceLayers.Count - 1);
                    avatarStringBuilder.AppendLine(File.ReadAllText(necklaceLayers[randomNecklace]));
                }

                // Wings
                if (avatarTriat.WithWings)
                {
                    var wingLayers = (from layer in files1
                                      where layer.Contains("wings")
                                      select layer).ToList();

                    int randomWing = random.Next(0, wingLayers.Count - 1);
                    avatarStringBuilder.AppendLine(File.ReadAllText(wingLayers[randomWing]));
                }

                // Suit
                if (avatarTriat.WithSuit)
                {
                    var suitLayers = (from layer in files1
                                      where layer.Contains("suit")
                                      select layer).ToList();

                    int randomSuit = random.Next(0, suitLayers.Count - 1);
                    string suit = suitLayers[randomSuit];
                    avatarStringBuilder.AppendLine(File.ReadAllText(
                        suit));

                    if (suit.Contains("1_of_2"))
                    {
                        avatarStringBuilder.AppendLine(File.ReadAllText(
                            suit.Replace("1_of_2", "2_of_2")));
                    }
                    else if (suit.Contains("2_of_2"))
                    {
                        avatarStringBuilder.AppendLine(File.ReadAllText(
                            suit.Replace("2_of_2", "1_of_2")));
                    }
                }

                // Shirt
                if (avatarTriat.WithShirt)
                {
                    var shirtLayers = (from layer in files1
                                       where layer.Contains("shirt")
                                       select layer).ToList();

                    int randomShirt = random.Next(0, shirtLayers.Count - 1);
                    avatarStringBuilder.AppendLine(File.ReadAllText(shirtLayers[randomShirt]));
                }

                // Blouse
                if (avatarTriat.WithBlouse)
                {
                    var blouseLayers = (from layer in files1
                                        where layer.Contains("blouse")
                                        select layer).ToList();

                    int randomBlouse = random.Next(0, blouseLayers.Count - 1);
                    string blouse = blouseLayers[randomBlouse];
                    avatarStringBuilder.AppendLine(File.ReadAllText(blouse));

                    if (blouse.Contains("1_of_2"))
                    {
                        avatarStringBuilder.AppendLine(
                            File.ReadAllText(blouse.Replace("1_of_2", "2_of_2")));
                    }
                    else if (blouse.Contains("2_of_2"))
                    {
                        avatarStringBuilder.AppendLine(
                            File.ReadAllText(blouse.Replace("2_of_2", "1_of_2")));
                    }
                }

                // Scarf
                if (avatarTriat.WithScarf)
                {
                    var scarfLayers = (from layer in files1
                                       where layer.Contains("scarf")
                                       select layer).ToList();

                    int randomScarf = random.Next(0, scarfLayers.Count - 1);
                    avatarStringBuilder.AppendLine(File.ReadAllText(scarfLayers[randomScarf]));
                }

                // Shoes
                if (avatarTriat.WithShoes)
                {
                    var shoeLayers = (from layer in files1
                                      where layer.Contains("shoes")
                                      select layer).ToList();

                    int randomShoe = random.Next(0, shoeLayers.Count - 1);
                    avatarStringBuilder.AppendLine(File.ReadAllText(shoeLayers[randomShoe]));
                }

                // Shoulder Pads
                var shoulderPadLayers = (from layer in files1
                                         where layer.Contains("shoulderpads")
                                         select layer).ToList();

                int randomSholderPad = random.Next(0, shoulderPadLayers.Count - 1);
                avatarStringBuilder.AppendLine(File.ReadAllText(shoulderPadLayers[randomSholderPad]));

                // Top
                if (avatarTriat.WithTop)
                {
                    var topLayers = (from layer in files1
                                     where layer.Contains("top")
                                     select layer).ToList();

                    int randomTop = random.Next(0, topLayers.Count - 1);
                    avatarStringBuilder.AppendLine(File.ReadAllText(topLayers[randomTop]));
                }

                // Dress
                if (avatarTriat.WithDress)
                {
                    var dressLayers = (from layer in files1
                                       where layer.Contains("dress")
                                       select layer).ToList();

                    int randomDress = random.Next(0, dressLayers.Count - 1);
                    avatarStringBuilder.AppendLine(File.ReadAllText(dressLayers[randomDress]));
                }

                // Leggings
                if (avatarTriat.WithLeggings)
                {
                    var leggingsLayers = (from layer in files1
                                          where layer.Contains("leggings")
                                          select layer).ToList();

                    int randomLegging = random.Next(0, leggingsLayers.Count - 1);
                    avatarStringBuilder.AppendLine(File.ReadAllText(leggingsLayers[randomLegging]));
                }

                // Bracelet

                if (avatarTriat.WithBracelet)
                {
                    var braceletLayers = (from layer in files1
                                          where layer.Contains("bracelet")
                                          select layer).ToList();

                    int randomBracelet = random.Next(0, braceletLayers.Count - 1);
                    string bracelet = braceletLayers[randomBracelet];

                    avatarStringBuilder.AppendLine(File.ReadAllText(bracelet));

                    if (bracelet.Contains("left"))
                    {
                        avatarStringBuilder.AppendLine(
                            File.ReadAllText(bracelet.Replace("left", "right")));
                    }
                    else if (bracelet.Contains("right"))
                    {
                        avatarStringBuilder.AppendLine(
                            File.ReadAllText(bracelet.Replace("right", "left")));
                    }
                    else if (bracelet.Contains("1_of_2"))
                    {
                        avatarStringBuilder.AppendLine(
                            File.ReadAllText(bracelet.Replace("1_of_2", "2_of_2")));
                    }
                    else if (bracelet.Contains("2_of_2"))
                    {
                        avatarStringBuilder.AppendLine(
                            File.ReadAllText(bracelet.Replace("2_of_2", "1_of_2")));
                    }
                }


                // Skirt                
                if (avatarTriat.WithSkirt)
                {
                    var skirtsLayers = (from layer in files1
                                        where layer.Contains("skirt")
                                        select layer).ToList();

                    int randomSkirt = random.Next(0, skirtsLayers.Count - 1);
                    avatarStringBuilder.AppendLine(File.ReadAllText(skirtsLayers[randomSkirt]));
                }

                // Nail
                var nailLayers = (from layer in files1
                                  where layer.Contains("nails")
                                  select layer).ToList();

                foreach (var nailLayer in nailLayers)
                {
                    if (nailLayer.Contains("nails_short_left.svg") ||
                        nailLayer.Contains("nails_short_right.svg"))
                    {
                        avatarStringBuilder.AppendLine(File.ReadAllText(nailLayer));
                        break;
                    }
                }

                // Pants
                if (avatarTriat.WithPant)
                {
                    var pantsLayers = (from layer in files1
                                       where layer.Contains("pants")
                                       select layer).ToList();

                    int randomPant = random.Next(0, pantsLayers.Count - 1);
                    avatarStringBuilder.AppendLine(File.ReadAllText(pantsLayers[randomPant]));
                }

                // Belt
                if (avatarTriat.WithBelt)
                {
                    var beltLayers = (from layer in files1
                                      where layer.Contains("belt")
                                      select layer).ToList();

                    int randomBelt = random.Next(0, beltLayers.Count - 1);
                    avatarStringBuilder.AppendLine(File.ReadAllText(beltLayers[randomBelt]));
                }

                foreach (var layer in files3)
                {
                    if (layer.Contains("layers.json"))
                        continue;

                    if (layer.Contains("body") ||
                        layer.Contains("makeup") ||
                        layer.Contains("hair") ||
                        layer.Contains("mask") ||
                        layer.Contains("nose") ||
                        layer.Contains("hat") ||
                        layer.Contains("mouth") ||
                        layer.Contains("nails") ||
                        layer.Contains("headband") ||
                        layer.Contains("lashes") ||
                        layer.Contains("coat") ||
                        layer.Contains("smoke") ||
                        layer.Contains("horns") ||
                        layer.Contains("earpiece") ||
                        layer.Contains("glasses") ||
                        layer.Contains("earings") ||
                        layer.Contains("eyes") ||
                        layer.Contains("eyepatch") ||
                        layer.Contains("sockets"))
                    {
                        // Do nothing
                    }
                    else
                    {
                        avatarStringBuilder.AppendLine(File.ReadAllText(layer));
                    }
                }

                // Body Head
                var bodyLayers = (from layer in files3
                                  where layer.Contains("body")
                                  select layer).ToList();

                foreach (var bodyLayer in bodyLayers)
                {
                    if (bodyLayer.Contains("body_head_default.svg"))
                    {
                        avatarStringBuilder.AppendLine(File.ReadAllText(bodyLayer));
                        break;
                    }
                }

                // Glasses
                if (avatarTriat.WithGlass)
                {
                    var glassesLayers = (from layer in files3
                                         where layer.Contains("glasses")
                                         select layer).ToList();

                    int randomGlass = random.Next(0, glassesLayers.Count - 1);
                    avatarStringBuilder.AppendLine(File.ReadAllText(glassesLayers[randomGlass]));
                }

                // Eyes
                var eyeLayers = (from layer in files3
                                 where layer.Contains("eye")
                                 select layer).ToList();

                int randomEye = random.Next(0, eyeLayers.Count - 1);
                avatarStringBuilder.AppendLine(File.ReadAllText(eyeLayers[randomEye]));

                // Hairs
                if (avatarTriat.WithHairs)
                {
                    var hairLayers = (from layer in files3
                                      where layer.Contains("hair")
                                      select layer).ToList();

                    int randomHair = random.Next(0, hairLayers.Count - 1);

                    string hair = hairLayers[randomHair];
                    avatarStringBuilder.AppendLine(
                        File.ReadAllText(hair)
                        .Replace("${color}", avatarTriat.HairColor));

                    if (hair.Contains("1_of_2"))
                    {
                        avatarStringBuilder.AppendLine(
                            File.ReadAllText(hair.Replace("1_of_2", "2_of_2"))
                            .Replace("${color}", avatarTriat.HairColor));
                    }
                    else if (hair.Contains("2_of_2"))
                    {
                        avatarStringBuilder.AppendLine(
                            File.ReadAllText(hair.Replace("2_of_2", "1_of_2"))
                            .Replace("${color}", avatarTriat.HairColor));
                    }
                    else if (hair.Contains("2_of_3"))
                    {
                        avatarStringBuilder.AppendLine(
                            File.ReadAllText(hair.Replace("2_of_3", "3_of_3"))
                            .Replace("${color}", avatarTriat.HairColor));
                    }
                    else if (hair.Contains("3_of_3"))
                    {
                        avatarStringBuilder.AppendLine(
                           File.ReadAllText(hair.Replace("3_of_3", "1_of_3"))
                           .Replace("${color}", avatarTriat.HairColor));
                        avatarStringBuilder.AppendLine(
                            File.ReadAllText(hair.Replace("3_of_3", "2_of_3"))
                            .Replace("${color}", avatarTriat.HairColor));
                    }
                }

                // Hats
                if (avatarTriat.WithHat)
                {
                    var hatLayers = (from layer in files3
                                     where layer.Contains("hat")
                                     select layer).ToList();

                    int randomHat = random.Next(0, hatLayers.Count - 1);
                    string hat = hatLayers[randomHat];
                    avatarStringBuilder.AppendLine(File.ReadAllText(hat));

                    if (hat.Contains("1_of_2"))
                    {
                        avatarStringBuilder.AppendLine(
                            File.ReadAllText(hat.Replace("1_of_2", "2_of_2")));
                    }
                }
               
                // Brows
                if (avatarTriat.WithBrows)
                {
                    var browLayers = (from layer in files3
                                      where layer.Contains("brows")
                                      select layer).ToList();

                    int randomBrow = random.Next(0, browLayers.Count - 1);
                    avatarStringBuilder.AppendLine(File.ReadAllText(browLayers[randomBrow]));
                }

                // Earings
                if (avatarTriat.WithEarings)
                {
                    var earingLayers = (from layer in files3
                                        where layer.Contains("earings")
                                        select layer).ToList();

                    int randomEaring = random.Next(0, earingLayers.Count - 1);
                    avatarStringBuilder.AppendLine(File.ReadAllText(earingLayers[randomEaring]));
                }

                // Nose
                var noseLayers = (from layer in files3
                                  where layer.Contains("nose")
                                  select layer).ToList();

                int randomNose = random.Next(0, noseLayers.Count - 1);
                avatarStringBuilder.AppendLine(File.ReadAllText(noseLayers[randomNose]));

                // Mouth
                var mouthLayers = (from layer in files3
                                   where layer.Contains("mouth")
                                   select layer).ToList();

                foreach (var mouthLayer in mouthLayers)
                {
                    if (mouthLayer.Contains("mouth_default.svg"))
                    {
                        avatarStringBuilder.AppendLine(File.ReadAllText(mouthLayer));
                        break;
                    }
                }

                // Smoke
                if (avatarTriat.WithSmoke)
                {
                    var smokeLayers = (from layer in files3
                                       where layer.Contains("smoke")
                                       select layer).ToList();

                    int randomSmoke = random.Next(0, smokeLayers.Count - 1);
                    avatarStringBuilder.AppendLine(File.ReadAllText(smokeLayers[randomSmoke]));
                }

                // Headband
                if (avatarTriat.WithHeadband)
                {
                    var headBandLayers = (from layer in files3
                                          where layer.Contains("headband")
                                          select layer).ToList();

                    int randomHeadband = random.Next(0, headBandLayers.Count - 1);
                    avatarStringBuilder.AppendLine(File.ReadAllText(headBandLayers[randomHeadband]));
                }

                // Makeup
                if (avatarTriat.WithMakeup)
                {
                    var makeupLayers = (from layer in files3
                                        where layer.Contains("makeup")
                                        select layer).ToList();

                    int randomMakeup = random.Next(0, makeupLayers.Count - 1);
                    avatarStringBuilder.AppendLine(File.ReadAllText(makeupLayers[randomMakeup]));
                }

                // Socket
                if (avatarTriat.WithSocket)
                {
                    var socketLayers = (from layer in files3
                                        where layer.Contains("socket")
                                        select layer).ToList();

                    int randomSocket = random.Next(0, socketLayers.Count - 1);
                    avatarStringBuilder.AppendLine(File.ReadAllText(socketLayers[randomSocket]));
                }

                // Lashes
                if (avatarTriat.WithLashes)
                {
                    var lasheLayers = (from layer in files3
                                       where layer.Contains("lashes")
                                       select layer).ToList();

                    int randomLash = random.Next(0, lasheLayers.Count - 1);
                    avatarStringBuilder.AppendLine(File.ReadAllText(lasheLayers[randomLash]));
                }

                // Mask
                if (avatarTriat.WithMask)
                {
                    var maskLayers = (from layer in files3
                                      where layer.Contains("mask")
                                      select layer).ToList();

                    int randomMask = random.Next(0, maskLayers.Count - 1);
                    avatarStringBuilder.AppendLine(File.ReadAllText(maskLayers[randomMask]));
                }

                avatarStringBuilder.AppendLine("</svg>");

                string avatar = avatarStringBuilder.ToString();

                string avatarViewerFolderPath = $"{exePath}\\AvatarViewer";
                string avatarSVGPath = $"{avatarViewerFolderPath}\\avatarModern-{i}.svg";
                File.WriteAllText(avatarSVGPath,
                    avatar);

                if (!Directory.Exists($"{exePath}\\FemaleAvatarBitMap"))
                    Directory.CreateDirectory($"{exePath}\\FemaleAvatarBitMap");

                SVGToBitmap(avatarBackgroundColors[i],
                       avatarSVGPath,
                       $"{exePath}\\FemaleAvatarBitMap\\avatarModern-{i}.png", height, width);
            }
        }
    }
}
