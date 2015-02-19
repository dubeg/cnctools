using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ShpLib
{
    public class Frame
    {
        //----------------------------------------------------------------
        // Vars
        //----------------------------------------------------------------
        public ushort Width { get; set; }
        public ushort Height { get; set; }
        public Byte[] Pixels { get; set; }
        public Color ColorOnRadar { get; set; }

        public Frame(ushort width, ushort height): this(width, height, new byte[width * height], null){}
        public Frame(ushort width, ushort height, Byte[] pixels, Color colorOnRadar)
        {
            Pixels = pixels;
            Width = width;
            Height = height;
            ColorOnRadar = colorOnRadar;
        }
    }
}
