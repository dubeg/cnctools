using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShpViewer
{
    public class SettingsManager
    {
        // Constants
        // ---------
        private readonly string FILENAME_SETTINGS;
        // Vars
        // ---------
        private List<string> _shpFilenames;
        private List<string> _palFilenames;
        // Props
        // ---------
        public List<string> ShpFilenames  {get{return _shpFilenames;}}
        public List<string> PalFilenames { get { return _palFilenames; } }
        public ShpViewer.Controllers.ShpsManager.EngineOptions EngineOption { get; set; }

        /// <summary>
        /// Init. new instance of Program Settings
        /// </summary>
        /// <param name="filename"></param>
        public SettingsManager(string filename)
        {
            FILENAME_SETTINGS = filename;
            _shpFilenames = new List<string>();
            _palFilenames = new List<string>();
            Load();
        }


        /// <summary>
        /// Load settings from disk.
        /// </summary>
        public bool Load()
        {
            bool loaded = false;
            if (File.Exists(FILENAME_SETTINGS))
            {
                StreamReader reader = File.OpenText(FILENAME_SETTINGS);
                string line;
                Sections currentSection = Sections.None;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (line.Length > 0)
                        if (line.Equals("[Shps]", StringComparison.OrdinalIgnoreCase))
                            currentSection = Sections.ShpFilenames;
                        else if (line.Equals("[Palettes]", StringComparison.OrdinalIgnoreCase))
                            currentSection = Sections.ShpPalettes;
                        else if (line.Equals("[EngineOption]", StringComparison.OrdinalIgnoreCase))
                            currentSection = Sections.EngineOption;
                        else
                        {
                            switch (currentSection)
                            {
                                case Sections.EngineOption:
                                    if (line.ToLower() == "libshp")
                                        EngineOption = Controllers.ShpsManager.EngineOptions.LibShp;
                                    else if (line.ToLower() == "openra" || line.ToLower() == "ora")
                                        EngineOption = Controllers.ShpsManager.EngineOptions.OpenRA;
                                    else
                                        EngineOption = Controllers.ShpsManager.EngineOptions.ShpLib;
                                    break;
                                case Sections.ShpFilenames:
                                    if (File.Exists(line))
                                        _shpFilenames.Add(line);
                                    break;
                                case Sections.ShpPalettes:
                                    if (File.Exists(line))
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


        /// <summary>
        /// File Settings - Sections
        /// </summary>
        private enum Sections
        {
            None,
            ShpFilenames,
            ShpPalettes,
            EngineOption
        }
    }
}
