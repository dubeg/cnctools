using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShpLib.V1
{
    public class FrameV1
    {
        //----------------------------------------------------------------
        // Const
        //----------------------------------------------------------------
        /// <summary>
        /// Size in bytes of Shp Frame Header.
        /// </summary>
        public const int HEADER_SIZE = 8;


        //----------------------------------------------------------------
        // Properties
        //----------------------------------------------------------------
        public UInt32 FileOffset { get; set; }
        public byte Format { get; set; }
        public UInt32 RefOffset { get; set; }
        public byte RefFormat { get; set; }
        public byte[] Data { get; set; }
    }
}
