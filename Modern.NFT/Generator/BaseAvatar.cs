using System;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using Svg;
using System.Drawing.Imaging;

namespace Modern.NFT.Generator
{
    public class BaseAvatar
    {
        /// <summary>  
        /// method for changing the opacity of an image  
        /// https://stackoverflow.com/questions/4779027/changing-the-opacity-of-a-bitmap-image
        /// </summary>  
        /// <param name="image">image to set opacity on</param>  
        /// <param name="opacity">percentage of opacity</param>  
        /// <returns></returns>  
        protected static Image SetImageOpacity(Image image, float opacity)
        {
            try
            {
                //create a Bitmap the size of the image provided  
                Bitmap bmp = new Bitmap(image.Width, image.Height);

                //create a graphics object from the image  
                using (Graphics gfx = Graphics.FromImage(bmp))
                {

                    //create a color matrix object  
                    ColorMatrix matrix = new ColorMatrix();

                    //set the opacity  
                    matrix.Matrix33 = opacity;

                    //create image attributes  
                    ImageAttributes attributes = new ImageAttributes();

                    //set the color(opacity) of the image  
                    attributes.SetColorMatrix(matrix,
                        ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                    //now draw the image  
                    gfx.DrawImage(image, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
                }
                return bmp;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        ///  https://stackoverflow.com/questions/6254422/convert-svg-to-png-or-jpeg
        /// </summary>
        /// <param name="inputImagePath"></param>
        /// <param name="outputImagePath"></param>
        protected static void SVGToBitmap(string background,
            string inputImagePath,
            string outputImagePath, int width, int height)
        {
            string newOutputImagePath = outputImagePath;

            var svgDocument = SvgDocument.Open(inputImagePath);
            using (var smallBitmap = svgDocument.Draw())
            {
                var oldWidth = smallBitmap.Width;
                var oldHeight = smallBitmap.Height;

                if (oldWidth != 500)
                {
                    oldWidth = width;
                    oldHeight = height / smallBitmap.Width * oldHeight;
                }

                using (var bitmap = svgDocument.Draw(oldWidth, oldHeight))
                {
                    bitmap.Save(outputImagePath);

                    var newImage = new Bitmap(bitmap.Width, bitmap.Height);
                    using (var g = Graphics.FromImage(newImage))
                    {
                        Color color = System.Drawing.ColorTranslator.FromHtml(background);
                        g.Clear(color);
                        g.DrawImage(bitmap, new Point(0, 0));
                    }

                    File.Delete(outputImagePath);

                    newImage = (Bitmap)SetImageOpacity(newImage, 1);
                    newImage.Save(newOutputImagePath);
                }
            }
        }

        /// <summary>
        ///  https://stackoverflow.com/questions/6254422/convert-svg-to-png-or-jpeg
        /// </summary>
        /// <param name="inputImagePath"></param>
        /// <param name="outputImagePath"></param>
        protected static void SVGToBitmap(
            string inputImagePath,
            string outputImagePath, 
            int width, int height)
        {
            string newOutputImagePath = outputImagePath;

            var svgDocument = SvgDocument.Open(inputImagePath);
            using (var smallBitmap = svgDocument.Draw())
            {
                var oldWidth = smallBitmap.Width;
                var oldHeight = smallBitmap.Height;

                if (oldWidth != 500)
                {
                    oldWidth = width;
                    oldHeight = height / smallBitmap.Width * oldHeight;
                }

                using (var bitmap = svgDocument.Draw(oldWidth, oldHeight))
                {
                    bitmap.Save(outputImagePath);

                    var newImage = new Bitmap(bitmap.Width, bitmap.Height);
                    using (var g = Graphics.FromImage(newImage))
                    {
                       g.DrawImage(bitmap, new Point(0, 0));
                    }

                    File.Delete(outputImagePath);

                    newImage = (Bitmap)SetImageOpacity(newImage, 1);
                    newImage.Save(newOutputImagePath);
                }
            }
        }

        protected static string ColorToHex(Color color)
        {
            return Color.FromArgb(color.R, color.G, color.B).ToHex();
        }

        protected static Color HexToColor(string hexColor)
        {
            Color color = System.Drawing.ColorTranslator.FromHtml(hexColor);
            return color;
        }

        protected static Color GetRandomColor(List<Color> colors)
        {
            int maxLength = colors.Count;
            var random = new Random();
            int randomVal = random.Next(0, maxLength - 1);
            return colors[randomVal];
        }
    }
}
