using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShpViewer.Controllers;

namespace ShpViewer
{
    public class Controller
    {
        const string FILENAME_SETTINGS = "settings.cfg";
        public SettingsManager SettingsManager { get; private set; }
        public ShpsManager ShpsManager { get; private set; }
        public PalettesManager PalettesManager { get; private set; }
        public LogManager LogManager { get; private set; }


        public Controller()
        {
            LogManager = new Controllers.LogManager();
            SettingsManager = new SettingsManager(FILENAME_SETTINGS);
            PalettesManager = new Controllers.PalettesManager(SettingsManager.PalFilenames);
            ShpsManager = new ShpsManager(SettingsManager.ShpFilenames, SettingsManager.EngineOption );
            
        }
    }
}
