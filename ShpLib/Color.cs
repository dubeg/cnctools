using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShpLib
{
    public class Color
    {
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }

        public Color(int r, int g, int b)
        {
            Red = r;
            Green = g;
            Blue = b;
        }
    }
}
