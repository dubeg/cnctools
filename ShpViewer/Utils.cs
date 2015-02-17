using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ShpApp
{
    public static class Utils
    {
        public static Color ConvertToDrawingColor(ShpLib.Color c)
        {
            return Color.FromArgb(c.Red, c.Green, c.Blue);
        }
        public static Color[] ConvertToDrawingColors(ShpLib.Color[] colors)
        {
            Color[] result = new Color[colors.Length];
            for (int i = 0; i < colors.Length; i++)
            {
                result[i] = ConvertToDrawingColor(colors[i]);
            }
            return result;
        }
    }
}
