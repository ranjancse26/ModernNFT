using System.Drawing;

namespace Modern.NFT
{
    /// <summary>
    /// https://stackoverflow.com/questions/39137486/converting-colour-name-to-hex-in-c-sharp
    /// </summary>
    public static class HexColorExtensions
    {
        public static string ToHex(this Color c) => $"#{c.R:X2}{c.G:X2}{c.B:X2}";
    }
}
