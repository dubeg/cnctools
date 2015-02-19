using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ShpApp
{
    public class ShpModel
    {
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
            return SafeName;
        }
    }
}
