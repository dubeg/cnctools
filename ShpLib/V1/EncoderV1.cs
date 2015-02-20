using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ShpLib;

namespace ShpLib.V1
{
    public class EncoderV1
    {
        // Const
        // ------
        public const byte FORMAT80 = 0x80;
        public const byte FORMAT40 = 0x40;
        public const byte FORMAT20 = 0x20;
        // Methods
        // ------
        public static byte[] Encode(byte[][] framesData, ushort width, ushort height)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(ms))
                {
                    ShpV1 shp;
                    uint fileOffset;  // Current FileOffset

                    shp = new ShpV1();
                    shp.FrameCount = (ushort)framesData.Length;
                    shp.FrameHeight = width;
                    shp.FrameWidth = height;

                    shp.Frames = new FrameV1[shp.FrameCount];

                    fileOffset = (uint)(ShpV1.HEADER_SIZE + (shp.FrameCount + 2) * FrameV1.HEADER_SIZE);
                    for (int i = 0; i < shp.FrameCount; i++)
                    {
                        FrameV1 f = new FrameV1();
                        shp.Frames[i] = f;
                        f.Format = FORMAT80;
                        f.FileOffset = fileOffset;
                        f.Data = Formats.Format80.Encode(framesData[i]);
                        fileOffset += (uint)f.Data.Length;
                    }
                    shp.FileSize = fileOffset;

                    // Writing file headers
                    writer.Write(shp.FrameCount);
                    writer.Write(shp.Unknown1);
                    writer.Write(shp.Unknown2);
                    writer.Write(shp.FrameWidth);
                    writer.Write(shp.FrameHeight);
                    writer.Write(shp.Unknown3);

                    // Writing frame headers
                    for (int i = 0; i < shp.Frames.Length; i++)
                    {
                        FrameV1 f = shp.Frames[i];
                        WriteOffset(writer, f.FileOffset);
                        writer.Write(f.Format);

                        WriteOffset(writer, f.RefOffset);
                        writer.Write(f.RefFormat);
                    }

                    // File Size & Zeros
                    writer.Write(shp.FileSize);
                    writer.Write(0);
                    writer.Write(0);
                    writer.Write(0);

                    // Encoded Frames
                    for (int i = 0; i < shp.FrameCount; i++)
                        writer.Write(shp.Frames[i].Data);
                }
                return ms.ToArray();
            }
        }

        private static void WriteOffset(BinaryWriter writer, uint offset)
        {
            byte b;

            b = (byte)(offset);
            writer.Write(b);
            b = (byte)(offset >> 8);
            writer.Write(b);
            b = (byte)(offset >> 16);
            writer.Write(b);
        }
    }
}
