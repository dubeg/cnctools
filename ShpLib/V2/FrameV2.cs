using System;
using System.Collections.Generic;
using System.Text;

namespace ShpLib.V2
{
    /// <summary>
    /// SHP Version 2 : Encoded Frame
    /// </summary>
    public class FrameV2
    {
        //----------------------------------------------------------------
        // Const
        //----------------------------------------------------------------
        public const int HEADER_SIZE = 24;


        //----------------------------------------------------------------
        // Properties
        //----------------------------------------------------------------
        /// <summary>
        /// Offset/dimensions of compressed frame relative to the uncompressed frame.
        /// </summary>
        public ushort OffsetX { get; set; }
        public ushort OffsetY { get; set; }
        public ushort CompressedWidth { get; set; }
        public ushort CompressedHeight { get; set; }
        public int CompressedSize { get { return CompressedHeight * CompressedWidth; } }

        /// <summary>
        /// Frame Offset in Shp file.
        /// </summary>
        public uint FileOffset { get; set; }
        public Color RadarColor { get; set; }
        public uint Compression { get; set; }
        public byte[] Data { get; set; }

    }
}
