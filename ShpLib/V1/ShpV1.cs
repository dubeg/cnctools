using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShpLib.V1
{
    public class ShpV1
    {
        //----------------------------------------------------------------
        // Const
        //----------------------------------------------------------------
        /// <summary>
        /// Size in bytes of Shp Header.
        /// </summary>
        public const int HEADER_SIZE = 14;


        //----------------------------------------------------------------
        // Properties
        //----------------------------------------------------------------
        public UInt16 FrameCount { get; set; }
        public UInt16 Unknown1 { get; set; }
        public UInt16 Unknown2 { get; set; }
        public UInt16 FrameWidth { get; set; }
        public UInt16 FrameHeight { get; set; }
        public UInt32 Unknown3 { get; set; }
        public FrameV1[] Frames { get; set; }
        public UInt32 FileSize { get; set; }
    }
}
