using System;
using System.Collections.Generic;
using System.IO;
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
        public UInt32 FileOffset;
        public byte Format;
        public UInt32 RefOffset;
        public byte RefFormat;
        public byte[] Data;
        public uint CalculatedDataLength { get; set; }

        public FrameV1() { }

        public FrameV1(Stream s)
        {
            byte[] buffer = new byte[4];
            s.Read(buffer, 0, 4);
            ReadBytes(ref FileOffset, ref Format, buffer);
            s.Read(buffer, 0, 4);
            ReadBytes(ref RefOffset, ref RefFormat, buffer);
        }

        public FrameV1(byte[] frameBytes, byte[] refBytes)
        {
            ReadBytes(ref FileOffset, ref Format, frameBytes);
            ReadBytes(ref RefOffset, ref RefFormat, refBytes);
        }

        private void ReadBytes(ref UInt32 offset, ref byte format, byte[] bytes)
        {
            offset = bytes[0];
            offset += (uint)bytes[1] << 8;
            offset += (uint)bytes[2] << 16;
            format = bytes[3];
        }


        /*
        public FrameV1(byte[] frameBytes, byte[] refBytes)
        {
            FileOffset = frameBytes[0];
            FileOffset += (uint)frameBytes[1] << 8;
            FileOffset += (uint)frameBytes[2] << 16;
            Format = frameBytes[3];

            RefOffset = refBytes[0];
            RefOffset += (uint)refBytes[1] << 8;
            RefOffset += (uint)refBytes[2] << 16;
            RefFormat = refBytes[3];
        }
         */
    }
}
