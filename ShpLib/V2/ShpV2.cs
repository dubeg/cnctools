using System;
using System.Collections.Generic;
using System.Text;


namespace ShpLib.V2
{
    /// <summary>
    /// SHP Version 2 : File Header
    /// </summary>
    public class ShpV2
    {
        //----------------------------------------------------------------
        // Const
        //----------------------------------------------------------------
        public const int HEADER_SIZE = 8;


        //----------------------------------------------------------------
        // Properties
        //----------------------------------------------------------------
        public ushort Unknown1 { get; set; }
        public ushort FrameWidth { get; set; }
        public ushort FrameHeight { get; set; }
        public ushort FrameCount { get; set; }
        public List<FrameV2> Frames { get; set; }
        public int FrameSize
        {
            get
            {
                return FrameWidth * FrameHeight;
            }
        }
    }
}
