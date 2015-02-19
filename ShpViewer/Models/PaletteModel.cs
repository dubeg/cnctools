using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ShpApp
{
    public class PaletteModel
    {
        // Props
        // ------
        public Color[] Colors { get; private set; }
        public string Name { get { return System.IO.Path.GetFileName(Filename); } }
        public string Filename { get; set; }
        // Methods
        // ------
        public PaletteModel(string fn, ShpLib.Color[] colors)
        {
            Filename = fn;
            Colors = Utils.ConvertToDrawingColors(colors);
        }

        public override string ToString()
        {
            return Name;
        }
    }



}
