﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShpApp
{
    public class PalettesManager
    {
        // Vars
        // --------
        private ObservableCollection<PaletteModel> _palettes;
        // Props
        // --------
        public ObservableCollection<string> PalettesFilenames { get; private set; }
        public PaletteModel SelectedPalette { get; private set; }
        public ObservableCollection<PaletteModel> Palettes { get { return _palettes; } }
        // Methods
        // --------
        public PalettesManager( ObservableCollection<string> palettesFilenames)
        {
            PalettesFilenames = palettesFilenames;
            _palettes = new ObservableCollection<PaletteModel>();

            if (Load())
                SelectPalette(0);
        }

        private bool Load()
        {
            foreach (var fn in PalettesFilenames)
            {
                if (File.Exists(fn))
                    _palettes.Add(ConvertToModel(fn, ShpLib.PalEngine.Load(fn)));
            }

            return _palettes.Count > 0;
        }

        private PaletteModel ConvertToModel(string fn, ShpLib.Color[] colors)
        {
            return new PaletteModel(fn, colors);
        }

        public void SelectPalette(int index)
        {
            SelectedPalette = _palettes[index];
        }
    }
}
