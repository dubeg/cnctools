using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ShpApp
{
    public class ShpModel
    {
        public object RawShp { get; set; }
        public string Filename { get; private set; }
        public string SafeName { get { return Path.GetFileName(Filename); } }
        public List<byte[]> Frames { get; private set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int FrameIndex { get; set; }
        public byte[] SelectedFrame { get { return Frames[FrameIndex]; } }
        public bool HasFrames { get { return Frames.Count > 0; } }

        public ShpModel(string filename)
        {
            FrameIndex = -1;
            Filename = filename;
            Frames = new List<byte[]>();
        }

        public override string ToString()
        {
            DirectoryInfo dInfo = new DirectoryInfo(Filename);
            return dInfo.Parent + " / " + SafeName;
        }

        public void SelectNextFrame()
        {
            ++FrameIndex;
            if (FrameIndex >= Frames.Count) FrameIndex = Frames.Count - 1;
        }

        public void SelectPrecedingFrame()
        {
            --FrameIndex;
            if (FrameIndex < 0) FrameIndex = 0;
        }
    }
}
