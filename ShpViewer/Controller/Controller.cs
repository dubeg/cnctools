using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShpApp
{
    public class Controller
    {
        // Fields
        // -------
        const string FILENAME_SETTINGS = "settings.cfg";
        public SettingsManager SettingsManager { get; private set; }
        public ShpsManager ShpsManager { get; private set; }
        public PalettesManager PalettesManager { get; private set; }
        // Methods
        // -------
        public Controller()
        {
            SettingsManager = new SettingsManager(FILENAME_SETTINGS);
            PalettesManager = new PalettesManager(SettingsManager.PalFilenames);
            ShpsManager = new ShpsManager(SettingsManager.ShpFilenames, SettingsManager.EngineOption);
        }
    }
}
