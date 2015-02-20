using ShpLib.Formats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ShpLib.V2
{
    /// <summary>
    /// Name: ShpEngine V2
    /// Desc: Engine for loading and saving Shp Version 2, used in CnC TS, RA2, YR.
    /// </summary>
    public class EncoderV2
    {
        public static byte[] Encode(byte[][] framesData, ushort width, ushort height, Color radarColor)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(ms))
                {
                    ShpV2 shp;
                    uint fileOffset;

                    shp = new ShpV2();
                    shp.FrameCount = (ushort)framesData.Length;
                    shp.FrameWidth = width;
                    shp.FrameHeight = height;
                    shp.Frames = new List<FrameV2>(shp.FrameCount);

                    // Encode frames.
                    for (int i = 0; i < shp.FrameCount; i++)
                    {
                        FrameV2 cFrame = EncodeFrame(framesData[i], width, height);
                        cFrame.RadarColor = radarColor;
                        shp.Frames.Add(cFrame);
                    }
                    
                    // Write header.
                    writer.Write(shp.Unknown1);
                    writer.Write(shp.FrameWidth);
                    writer.Write(shp.FrameHeight);
                    writer.Write(shp.FrameCount);

                    // Write frames headers.
                    fileOffset = (uint)(ShpV2.HEADER_SIZE + FrameV2.HEADER_SIZE * shp.FrameCount);
                    for (int i = 0; i < shp.FrameCount; i++)
                    {
                        FrameV2 cFrame = shp.Frames[i];

                        if (cFrame.OffsetX > 0)
                        {
                            fileOffset += (uint)(-fileOffset & 7);
                            cFrame.FileOffset = fileOffset;
                            fileOffset += (uint)cFrame.Data.Length;
                        }

                        writer.Write(cFrame.OffsetX);
                        writer.Write(cFrame.OffsetY);
                        writer.Write(cFrame.CompressedWidth);
                        writer.Write(cFrame.CompressedHeight);
                        writer.Write(cFrame.Compression);
                        writer.Write((byte)cFrame.RadarColor.Red);
                        writer.Write((byte)cFrame.RadarColor.Blue);
                        writer.Write((byte)cFrame.RadarColor.Green);
                        writer.Write((byte)0);
                        writer.Write(0);
                        writer.Write(cFrame.FileOffset);
                    }

                    // Frame Data
                    for (int i = 0; i < shp.FrameCount; i++)
                    {
                        FrameV2 cFrame = shp.Frames[i];
                        if (cFrame.OffsetX > 0)
                        {
                            int padding = (int)(cFrame.FileOffset - writer.BaseStream.Position);
                            writer.Seek(padding, SeekOrigin.Current);
                            writer.Write(cFrame.Data);
                        }
                    }
                }
                return ms.ToArray();
            }
        }
        
        private static FrameV2 EncodeFrame(byte[] data, int width, int height)
        {
            byte[] cData;// Compressed Data
            byte[] eData;// Encoded Data
            FrameV2 cFrame = new FrameV2();
            Point2D p1 = new Point2D(0, 0);
            Point2D p2 = new Point2D(width, height);
            bool used = GetUsedXY(data, width, height, p1, p2);

            if (used)
            {
                cFrame.OffsetX = (ushort)p2.X;
                cFrame.OffsetY = (ushort)p2.Y;
                cFrame.CompressedWidth = (ushort)(p1.X - p2.X + 1);
                cFrame.CompressedHeight = (ushort)(p1.Y - p2.Y + 1);

                cData = ExtractPixels(
                    data, width,
                    cFrame.OffsetX, cFrame.OffsetY,
                    cFrame.CompressedWidth, cFrame.CompressedHeight);
                
                eData = Format2.Encode(cData, cFrame.CompressedWidth, cFrame.CompressedHeight);


                if (eData.Length < cData.Length)
                {
                    cFrame.Data = eData;
                    cFrame.Compression = 3;
                }
                else
                {
                    cFrame.Data = cData;
                    cFrame.Compression = 0;
                }
            }
            return cFrame;
        }

        private static bool GetUsedXY(byte[] pixels, int width, int height, Point2D p1, Point2D p2)
        {
            p1.X = -1;
            p1.Y = -1;

            p2.X = width;
            p2.Y = height;

            int index = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (pixels[index] > 0)
                    {
                        if (p1.X < x)
                            p1.X = x;
                        if (p2.X > x)
                            p2.X = x;
                        if (p1.Y < y)
                            p1.Y = y;
                        if (p2.Y > y)
                            p2.Y = y;
                    }
                    ++index;
                }
            }

            return p1.X > -1 && p1.Y > -1 && p2.X < width && p2.Y < height;
        }

        private static byte[] ExtractPixels(byte[] pixels, int frameWidth, int offsetX, int offsetY, int width, int height)
        {
            byte[] cPixels = new byte[width * height];

            int destIndex = 0;
            int srcIndex = offsetY * frameWidth + offsetX;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    cPixels[destIndex] = pixels[srcIndex + x];
                    ++destIndex;
                }
                srcIndex += frameWidth;
            }
            return cPixels;
        }

    }
}
