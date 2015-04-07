﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ShpLib.Formats;

namespace ShpLib.V1
{
    public static class DecoderV1
    {
        //----------------------------------------------------------------
        // Methods
        //----------------------------------------------------------------
        public static Frame[] Decode(byte[] data, out ShpV1 shp)
        {
            Frame[] frames;
            using (MemoryStream ms = new MemoryStream(data))
            {
                //ShpV1 shp;
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
                if (shp.FrameCount == 0 && shp.FrameWidth != 0)
                    throw new Exception("Might not be Shp(v1)");

                shp.Frames = new FrameV1[shp.FrameCount];
                frames = new Frame[shp.FrameCount];

                // Frame headers
                for (int i = 0; i < shp.FrameCount; i++)
                {
                    FrameV1 f = new FrameV1(r.BaseStream);
                    shp.Frames[i] = f;
                }

                // Filesize
                shp.FileSize = r.ReadUInt64();
                shp.Zero = r.ReadUInt64();

                // Special case
                if (shp.Zero != 0)
                {
                    // Frame, format20 (xor over preceding frame)
                    // RefOffset and RefFormat here means nothing (idk what the values are for)
                    // Desc: gives differences between first and last frame
                    // TODO: special field to ShpV1 (ex: LoopDiffOffset)
                    shp.SpecialFrame = new FrameV1(new MemoryStream( BitConverter.GetBytes(shp.FileSize) ));
                    shp.FileSize = shp.Zero;
                }

                // Body
                uint dataLength;
                for (int i = 0; i < shp.FrameCount; i++)
                {
                    FrameV1 f = shp.Frames[i];

                    // Find DataLength
                    //------------------------------
                    if (i + 1 == shp.FrameCount)
                    {
                        if (shp.SpecialFrame != null)
                            dataLength = shp.SpecialFrame.FileOffset - f.FileOffset;
                        else
                            dataLength = (uint)shp.FileSize - f.FileOffset;
                    }
                    else
                        dataLength = shp.Frames[i + 1].FileOffset - f.FileOffset;
                    f.CalculatedDataLength = dataLength;

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
                            // todo: create BST or hash tables for retrieving ref. frames faster if need be
                            for (int j = i - 1; j >= 0; --j)
                                if (shp.Frames[j].FileOffset == f.RefOffset)
                                {
                                    refData = frames[j].Pixels;
                                    break;
                                }

                            // Checking corruption.
                            if (refData == null)
                                throw new Exception("Format40: invalid reference value (0x0). ");

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
                            throw new Exception("Format: invalid format value (" + f.Format + ")");
                    }
                }

            }
            return frames;
        }
    }
}
