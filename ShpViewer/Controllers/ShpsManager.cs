using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShpLib;
using System.IO;

namespace ShpViewer.Controllers
{
    public class ShpsManager
    {
        public enum EngineOptions
        {
            ShpLib,
            LibShp,
            OpenRA
        }
        public enum ForceLoadOptions
        {
            V1,
            V2
        }

        // Vars
        // --------
        private List<ShpModel> _shps;
        // Props
        // --------
        public ShpModel SelectedShp { get; private set; }
        public List<string> ShpFilenames { get; private set; }
        public EngineOptions EngineOption { get; private set; }

        /// <summary>
        /// Ctor
        /// </summary>
        public ShpsManager(List<string> shpFilenames, EngineOptions engineOption)
        {
            ShpFilenames = shpFilenames;
            _shps = new List<ShpModel>();
            EngineOption = engineOption;
            LoadFiles();
            SelectShp(0);
        }


        private void LoadFiles()
        {
            foreach (var fn in ShpFilenames)
            {
                if (File.Exists(fn))
                {
                    byte[] fData = File.ReadAllBytes(fn);
                    switch (EngineOption)
                    {
                        case EngineOptions.ShpLib:
                            _shps.Add(ConvertToModel(Engine.Decode(fData, DecodingOptions.ShpV1)));
                            break;
                        case EngineOptions.LibShp:
                            LibShp.ShpGen2 sGen2 = new LibShp.ShpGen2( new MemoryStream(fData));
                            _shps.Add( ConvertToModel( sGen2));
                            break;
                        case EngineOptions.OpenRA:
                            OpenRA.Mods.Common.SpriteLoaders.ShpTSLoader l = new OpenRA.Mods.Common.SpriteLoaders.ShpTSLoader();
                            OpenRA.Graphics.ISpriteFrame[] frames;
                            l.TryParseSprite(new MemoryStream(fData), out frames);
                            _shps.Add(ConvertToModel(frames));
                            break;
                        default:
                            break;
                    }
                }
            }
        }


        public ShpModel ConvertToModel(LibShp.ShpGen2 s)
        {
            ShpModel m = new ShpModel();
            m.Height = s.Header.Height;
            m.Width = s.Header.Width;
            for (int i = 0; i < s.Frames.Length; i++)
            {
                m.Frames.Add( s.Frames[i].RawData );
            }

            return m;
        }


        public ShpModel ConvertToModel(OpenRA.Graphics.ISpriteFrame[] frames)
        {
            ShpModel shp = new ShpModel();
            for (int i = 0; i < frames.Length; i++)
            {
                shp.Width = frames[i].FrameSize.Width;
                shp.Height = frames[i].FrameSize.Height;
                shp.Frames.Add(frames[i].Data);
            }
            return shp;
        }


        public ShpModel ConvertToModel(ShpLib.Frame[] frames)
        {
            ShpModel shp = new ShpModel();
            for (int i = 0; i < frames.Length; i++)
            {
                shp.Width = frames[i].Width;
                shp.Height = frames[i].Height;
                shp.Frames.Add(frames[i].Pixels);
            }
            return shp;
        }


        public void SelectShp(int index)
        {
                SelectedShp = _shps[index];
        }
    }
}
