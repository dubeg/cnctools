using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShpLib.Formats;

namespace ShpLib.V1
{
    public static class DecoderV1
    {
        //----------------------------------------------------------------
        // Methods
        //----------------------------------------------------------------
        public static Frame[] Decode(byte[] data)
        {
            Frame[] frames;
            using (MemoryStream ms = new MemoryStream(data))
            {
                ShpV1 shp;
                BinaryReader r;


                r = new BinaryReader(ms);
                shp = new ShpV1();

                // Header
                shp.FrameCount = r.ReadUInt16();
                shp.Unknown1 = r.ReadUInt16();
                shp.Unknown2 = r.ReadUInt16();
                shp.FrameWidth = r.ReadUInt16();
                shp.FrameHeight = r.ReadUInt16();
                shp.Unknown3 = r.ReadUInt32();

                shp.Frames = new FrameV1[shp.FrameCount];
                frames = new Frame[shp.FrameCount];

                // Frame headers
                for (int i = 0; i < shp.FrameCount; i++)
                {
                    FrameV1 f = new FrameV1();
                    shp.Frames[i] = f;

                    f.FileOffset = r.ReadByte();
                    f.FileOffset = f.FileOffset + (uint)(r.ReadByte() << 8);
                    f.FileOffset = f.FileOffset + (uint)(r.ReadByte() << 16);
                    f.Format = r.ReadByte();
                    f.RefOffset = r.ReadByte();
                    f.RefOffset = f.RefOffset + (uint)(r.ReadByte() << 8);
                    f.RefOffset = f.RefOffset + (uint)(r.ReadByte() << 16);
                    f.RefFormat = r.ReadByte();
                }

                // Filesize
                shp.FileSize = r.ReadUInt32();
                r.ReadBytes(12);

                // Body
                uint dataLength;
                for (int i = 0; i < shp.FrameCount; i++)
                {
                    FrameV1 f = shp.Frames[i];

                    // Find DataLength
                    //------------------------------
                    if (i + 1 == shp.FrameCount)
                        dataLength = shp.FileSize - f.FileOffset;
                    else
                        dataLength = shp.Frames[i + 1].FileOffset - f.FileOffset;

                    // Read Data
                    //------------------------------
                    f.Data = r.ReadBytes((int)dataLength);
                    byte[] refData = null;

                    // Init.
                    Frame dF = new Frame(shp.FrameWidth, shp.FrameHeight);
                    frames[i] = dF;

                    // Decode
                    //------------------------------
                    switch (f.Format)
                    {
                        case 0x80:
                            Format80.DecodeInto(f.Data, dF.Pixels);
                            break;
                        case 0x40:
                            // Find reference frame
                            for (int j = i - 1; j >= 0; --j)
                                if (shp.Frames[j].FileOffset == f.RefOffset)
                                {
                                    refData = frames[j].Pixels;
                                    break;
                                }

                            // Checking corruption.
                            if (refData == null)
                                throw new Exception("Loading SHPv1: Reference frame NOT FOUND. ");

                            // Copy reference frame
                            for (int j = 0; j < refData.Length; j++)
                                dF.Pixels[j] = refData[j];

                            // Decode Format40
                            Format40.DecodeInto(f.Data, dF.Pixels);
                            break;
                        case 0x20:
                            // Copy preceding frame
                            refData = frames[i - 1].Pixels;
                            for (int j = 0; j < refData.Length; j++)
                                dF.Pixels[j] = refData[j];

                            // Decode Format20
                            Format40.DecodeInto(f.Data, dF.Pixels);
                            break;
                        default:
                            throw new Exception("Loading SHPv1: Unsupported Encoding Format. File may be corrupted.");
                            break;
                    }
                }

            }
            return frames;
        }
    }
}
