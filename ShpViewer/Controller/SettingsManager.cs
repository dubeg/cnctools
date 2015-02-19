using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace ShpApp
{
    public class SettingsManager
    {
        private enum Sections
        {
            None,
            ShpFilenames,
            ShpPalettes,
            EngineOption
        }
        // Constants
        // ---------
        private readonly string FILENAME_SETTINGS;
        // Vars
        // ---------
        private ObservableCollection<string> _shpFilenames;
        private ObservableCollection<string> _palFilenames;
        // Props
        // ---------
        public ObservableCollection<string> ShpFilenames { get { return _shpFilenames; } }
        public ObservableCollection<string> PalFilenames { get { return _palFilenames; } }
        public ShpApp.ShpsManager.EngineOptions EngineOption { get; set; }

        // Methods
        // ---------
        public SettingsManager(string filename)
        {
            FILENAME_SETTINGS = filename;
            _shpFilenames = new ObservableCollection<string>();
            _palFilenames = new ObservableCollection<string>();
            Load();
        }

        public bool Load()
        {
            bool loaded = false;
            if (File.Exists(FILENAME_SETTINGS))
            {
                StreamReader reader;
                string line;
                Sections cSec;

                reader = File.OpenText(FILENAME_SETTINGS);
                cSec = Sections.None;

                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim().ToLower();

                    if (line.Length > 0)
                        if(!ParseSection(line, ref cSec))
                        {
                            switch (cSec)
                            {
                                case Sections.EngineOption:
                                    EngineOption = ParseEngineOption(line);
                                    break;
                                case Sections.ShpFilenames:
                                    _shpFilenames.Add(line);
                                    break;
                                case Sections.ShpPalettes:
                                    _palFilenames.Add(line);
                                    break;
                                case Sections.None:
                                default:
                                    break;
                            }
                        }
                }
                reader.Dispose();
                loaded = true;
            }
            return loaded;
        }

        private bool ParseSection(string line, ref Sections cSec)
        {
            if (line[0] == '[')
            {
                if (line.Equals("[shps]", StringComparison.OrdinalIgnoreCase))
                    cSec = Sections.ShpFilenames;
                else if (line.Equals("[palettes]", StringComparison.OrdinalIgnoreCase))
                    cSec = Sections.ShpPalettes;
                else if (line.Equals("[engine]", StringComparison.OrdinalIgnoreCase))
                    cSec = Sections.EngineOption;
                return true;
            }
            return false;
        }

        private ShpApp.ShpsManager.EngineOptions ParseEngineOption(string line)
        {
            if (line == "libshp")
                return ShpsManager.EngineOptions.LibShp;
            else if (line == "openra" || line == "ora")
                return ShpsManager.EngineOptions.OpenRA;
            else if (line == "shplib")
                return ShpsManager.EngineOptions.ShpLib;
            else
                return ShpsManager.EngineOptions.Unspecified;
        }
    }
}
