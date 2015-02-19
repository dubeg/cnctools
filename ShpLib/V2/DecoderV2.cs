using ShpLib.Formats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ShpLib.V2
{
    public static class DecoderV2
    {
        //----------------------------------------------------------------
        // Methods
        //----------------------------------------------------------------
        public static Frame[] Decode(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(ms))
                {
                    Frame[] frames;
                    ShpV2 file;
                    uint nextOffset;
                    uint encodedSize;

                    // Reading file header.
                    file = new ShpV2();
                    file.Unknown1 = reader.ReadUInt16();
                    file.FrameWidth = reader.ReadUInt16();
                    file.FrameHeight = reader.ReadUInt16();
                    file.FrameCount = reader.ReadUInt16();

                    file.Frames = new List<FrameV2>(file.FrameCount);
                    frames = new Frame[file.FrameCount];

                    // Check Validity
                    if (file.Unknown1 != 0)
                        throw new Exception("File may be corrupted.");


                    // Frame Headers
                    for (int i = 0; i < file.FrameCount; i++)
                    {
                        FrameV2 cFrame = new FrameV2();
                        uint zeros;
                        file.Frames.Add(cFrame);
                        cFrame.OffsetX = reader.ReadUInt16();
                        cFrame.OffsetY = reader.ReadUInt16();
                        cFrame.CompressedWidth = reader.ReadUInt16();
                        cFrame.CompressedHeight = reader.ReadUInt16();
                        cFrame.Compression = reader.ReadUInt32();
                        cFrame.RadarColor = new Color( reader.ReadByte(), reader.ReadByte(), reader.ReadByte()); 
                        zeros = reader.ReadUInt32();
                        cFrame.FileOffset = reader.ReadUInt32();

                        // Check Validity
                        if (cFrame.OffsetX > file.FrameWidth || cFrame.OffsetY > file.FrameHeight ||
                            cFrame.FileOffset > reader.BaseStream.Length || cFrame.CompressedHeight > file.FrameHeight ||
                            cFrame.CompressedWidth > file.FrameWidth || zeros != 0)
                            throw new Exception("File may be corrupted.");
                    }

                    // Check Validity
                    if (file.FrameCount > 0)
                        if (file.Frames[0].FileOffset > (ShpV2.HEADER_SIZE + FrameV2.HEADER_SIZE * file.FrameCount))
                            throw new Exception("File may be corrupted.");


                    // Frame Data
                    for (int i = 0; i < file.FrameCount; i++)
                    {
                        FrameV2 cFrame = file.Frames[i];
                        Frame frame;

                        if (cFrame.FileOffset != 0)
                        {
                            if (cFrame.Compression == 3)
                            {
                                // ------------------- COMPRESSION 3 ------------------------
                                encodedSize = 0;
                                nextOffset = FindNextOffset(file.Frames, i + 1);

                                if (nextOffset > 0)
                                    encodedSize = nextOffset - cFrame.FileOffset;
                                else
                                    encodedSize = (uint)(reader.BaseStream.Length - 1 - reader.BaseStream.Position);

                                if (encodedSize > 0)
                                {
                                    reader.BaseStream.Seek(cFrame.FileOffset - reader.BaseStream.Position, SeekOrigin.Current);
                                    cFrame.Data = reader.ReadBytes((int)encodedSize);
                                }
                                else
                                    throw new Exception("Encoded Frame has no data, but its FileOffset > 0. File may be corrupted.");
                            }
                            else if (cFrame.Compression == 0)
                            {
                                // ------------------- COMPRESSION 0 ------------------------
                                reader.BaseStream.Seek(cFrame.FileOffset - reader.BaseStream.Position, SeekOrigin.Current);
                                cFrame.Data = reader.ReadBytes(cFrame.CompressedSize);
                            }
                            else
                                throw new Exception("Unsupported compression value. File may be corrupted.");

                            frame = Decompress(cFrame, file.FrameWidth, file.FrameHeight);
                        }
                        else
                        {
                            frame = new Frame(file.FrameWidth, file.FrameHeight);
                        }

                        frames[i] = frame;
                    }
                    return frames;
                }
            }
        }

        private static Frame Decompress(FrameV2 cFrame, ushort frameWidth, ushort frameHeight)
        {
            Frame frame;
            byte[] dData;
            int sIndex;
            int dIndex;

            if (cFrame.Compression == 3)
                dData = Format2.Decode(cFrame.Data, cFrame.CompressedWidth, cFrame.CompressedHeight);
            else
                dData = cFrame.Data;


            frame = new Frame(frameWidth, frameHeight);
            sIndex = 0;
            dIndex = cFrame.OffsetY * frameWidth + cFrame.OffsetX;

            for (int y = 0; y < cFrame.CompressedHeight; ++y)
            {
                for (int x = 0; x < cFrame.CompressedWidth; ++x)
                {
                    frame.Pixels[dIndex + x] = dData[sIndex];
                    ++sIndex;
                }
                dIndex += frameWidth;
            }
            return frame;
        }

        private static uint FindNextOffset(List<FrameV2> cFrames, int currentPosition)
        {
            uint nextOffset = 0;

            for (; currentPosition < cFrames.Count; ++currentPosition)
            {
                nextOffset = cFrames[currentPosition].FileOffset;
                if (nextOffset != 0)
                    break;
            }

            return nextOffset;
        }
    }
}
