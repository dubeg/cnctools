using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShpLib.Formats
{
    public static class Format2
    {
        public static byte[] Decode(byte[] src, int width, int height)
        {
            byte[] dest = new byte[width * height];
            int sI = 0;
            int dI = 0;

            int lineLength = 0;
            int runLength = 0;

            for (int y = 0; y < height; ++y)
            {
                lineLength = Utils.CombineBytes(src[sI], src[sI + 1]) - 2;
                sI += 2;

                int curX = 0;
                while (0 < lineLength--)
                {
                    byte v = src[sI++];

                    if (v != 0)
                    {
                        dest[dI++] = v;
                        ++curX;
                    }
                    else
                    {
                        if (sI >= src.Length) break;

                        --lineLength;
                        runLength = src[sI++];

                        if (curX + runLength > width)
                            runLength = width - curX;
                        curX += runLength;
                        dI += runLength;
                    }
                }
            }
            return dest;
        }

        public static byte[] Encode(byte[] src, int width, int height)
        {
            List<Byte> dest = new List<byte>();

            int sI;// Src  Buffer Index
            int dI;// Dest Buffer Index

            int maxSI;// End of Line index of currently processed line
            int oldDI;// Dest. Buffer Index before encoding a new line.

            int length;// Compressed Length (Run Length)

            byte v,// Value of current byte 
                 loB,// Low byte containing the Length of the encoded line
                 hiB;// Hi byte containing the Length of the encoded line

            sI = 0;
            dI = 0;

            for (int y = 0; y < height; ++y)
            {
                maxSI = sI + width;

                oldDI = dI;
                dI += 2;
                dest.Add(0);
                dest.Add(0);

                // Encode line (with run length)
                while (sI < maxSI)
                {
                    v = src[sI];
                    dest.Add(v);
                    ++dI;

                    if (v > 0)
                        ++sI;
                    else
                    {
                        length = Utils.GetRunLength(src, sI, maxSI);

                        if (length > 255)
                            length = 255;

                        sI += length;

                        dest.Add((byte)length);
                        ++dI;
                    }
                }

                //Store Encoded Line Length in two bytes before the beginning of the line.
                Utils.SplitWord((ushort)(dI - oldDI), out loB, out hiB);
                dest[oldDI] = loB;
                dest[oldDI + 1] = hiB;
            }

            return dest.ToArray();
        }
    }
}
