using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.ObjectModel;
using ShpLib;

namespace ShpApp
{
    public class ShpsManager
    {
        // Enums
        // --------
        public enum EngineOptions
        {
            ShpLib,
            LibShp,
            OpenRA,
            Unspecified
        }
        public enum ForceLoadOptions
        {
            V1,
            V2
        }
        // Vars
        // --------
        private ObservableCollection<ShpModel> _shps;
        // Props
        // --------
        public ShpModel SelectedShp { get; private set; }
        public ObservableCollection<string> ShpFilenames { get; private set; }
        public EngineOptions EngineOption { get; private set; }
        public ObservableCollection<ShpModel> Shps { get { return _shps; } }
        // Methods
        // --------
        public ShpsManager(ObservableCollection<string> shpFilenames, EngineOptions engineOption)
        {
            ShpFilenames = shpFilenames;
            _shps = new ObservableCollection<ShpModel>();
            EngineOption = engineOption;

            if(LoadShp(0))
                SelectShp(0);
        }

        private bool LoadShp(int index)
        {
            string fn;
            bool loaded = false;
            if (index < ShpFilenames.Count)
            {
                fn = ShpFilenames[index];
                if (File.Exists(fn))
                {
                    byte[] fData = File.ReadAllBytes(fn);
                    switch (EngineOption)
                    {
                        case EngineOptions.ShpLib:
                            _shps.Add(ConvertToModel(fn, Engine.Decode(fData, DecodingOptions.ShpV1)));
                            loaded = true;
                            break;
                        case EngineOptions.LibShp:
                            //LibShp.ShpGen2 sGen2 = new LibShp.ShpGen2( new MemoryStream(fData));
                            //_shps.Add( ConvertToModel( sGen2));
                            break;
                        case EngineOptions.OpenRA:
                            //OpenRA.Mods.Common.SpriteLoaders.ShpTSLoader l = new OpenRA.Mods.Common.SpriteLoaders.ShpTSLoader();
                            //OpenRA.Graphics.ISpriteFrame[] frames;
                            //l.TryParseSprite(new MemoryStream(fData), out frames);
                            //_shps.Add(ConvertToModel(fn, frames));
                            //loaded = true;
                            break;
                        default:
                            break;
                    }
                }
            }
            return loaded;
        }

        //public ShpModel ConvertToModel(string fn, ShpLib.ShpGen2 s)
        //{
        //    ShpModel m = new ShpModel(fn);
        //    m.Height = s.Header.Height;
        //    m.Width = s.Header.Width;
        //    for (int i = 0; i < s.Frames.Length; i++)
        //    {
        //        m.Frames.Add( s.Frames[i].RawData );
        //    }

        //    return m;
        //}
        /*
        public ShpModel ConvertToModel(string fn, OpenRA.Graphics.ISpriteFrame[] frames)
        {
            ShpModel shp = new ShpModel(fn);
            shp.FrameIndex = frames.Length > 0 ? 0 : -1;
            for (int i = 0; i < frames.Length; i++)
            {
                shp.Width = frames[i].FrameSize.Width;
                shp.Height = frames[i].FrameSize.Height;
                shp.Frames.Add(frames[i].Data);
            }
            return shp;
        }
        */
        public ShpModel ConvertToModel(string fn, ShpLib.Frame[] frames)
        {
            ShpModel shp = new ShpModel(fn);
            shp.FrameIndex = frames.Length > 0 ? 0 : -1;
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
