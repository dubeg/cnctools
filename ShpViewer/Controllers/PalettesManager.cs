using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShpViewer.Controllers
{
    public class PalettesManager
    {
        // Vars
        // --------
        private List<Color[]> _palettes;
        // Props
        // --------
        public List<string> PalettesFilenames { get;private set; }
        public Color[] SelectedPalette { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="palettesFilenames"></param>
        public PalettesManager(List<string> palettesFilenames)
        {
            PalettesFilenames = palettesFilenames;
            _palettes = new List<Color[]>();
            Load();
            SelectPalette(0);
        }


        private bool Load()
        {
            bool result = false;
            foreach (var fn in PalettesFilenames)
            {
                if (File.Exists(fn))
                    _palettes.Add(ConvertToDrawColor(ShpLib.PalEngine.Load(fn)));
            }

            return result;
        }


        private Color[] ConvertToDrawColor(ShpLib.Color[] colors)
        {
            Color[] result = new Color[colors.Length];
            for (int i = 0; i < colors.Length; i++)
            {
                result[i] = Color.FromArgb(
                    byte.MaxValue,
                    colors[i].Red,
                    colors[i].Green,
                    colors[i].Blue)
                    ;
            }
            return result;
        }


        public void SelectPalette(int index)
        {
            SelectedPalette = _palettes[index];
        }
    }
}
