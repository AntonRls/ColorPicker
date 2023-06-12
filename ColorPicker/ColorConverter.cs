using System.Drawing;

namespace ColorPicker
{
    class ColorConverter
    {
        public static string HexConvert(Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }
        public static string RGBConvert(Color c)
        {
            return $"{c.R},{c.G},{c.B}";
        }
    }
}
