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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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

            if( Load() )
                SelectShp(0);
        }

        public bool Load()
        {
            foreach (var fn in ShpFilenames)
            {
                if (File.Exists(fn))
                {
                    log.Info("Loading: " + fn);
                    LoadShp(fn);
                }
                else
                {
                    log.Warn("Not found: " + fn);
                }
            }

            return _shps.Count > 0;
        }

        public bool LoadShp(string fn)
        {
            bool loaded = false;
            if (fn != null && File.Exists(fn))
            {
                switch (EngineOption)
                {
                    case EngineOptions.ShpLib:
                        ReadShp(fn);
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
        public void ReadShp(string fn)
        {
            ShpLib.V1.ShpV1 shpV1;
            ShpLib.V2.ShpV2 shpV2;
            Frame[] frames;
            byte[] fData = File.ReadAllBytes(fn);
            try
            {
                log.Info("Decoding Shp(v1)");
                frames = ShpLib.V1.DecoderV1.Decode(fData, out shpV1);
                log.Info("Success");
                _shps.Add(ConvertToModel(fn, frames, shpV1));
            }
            catch (Exception ex)
            {
                log.Error("Failure : " + ex.Message);
                log.Info("Decoding Shp(v2)");
                frames = ShpLib.V2.DecoderV2.Decode(fData, out shpV2);
                log.Info("Success");
                _shps.Add(ConvertToModel(fn, frames, shpV2));
            }
        }

        public ShpModel ConvertToModel(string fn, ShpLib.Frame[] frames, object rawShp)
        {
            ShpModel shp = new ShpModel(fn);
            shp.FrameIndex = frames.Length > 0 ? 0 : -1;
            shp.RawShp = rawShp;

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

        public void SaveCurrentShpTo(string filename, EncodingOptions option)
        {

            byte[][] framesData = new byte[SelectedShp.Frames.Count][];
            for (int i = 0; i < framesData.GetLength(0); i++)
            {
                framesData[i] = SelectedShp.Frames[i];
            }

            Engine.Save(filename, framesData, (ushort)SelectedShp.Width, (ushort)SelectedShp.Height, option);
        }
    }
}
