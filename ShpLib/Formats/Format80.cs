using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShpLib.Formats
{
    public static class Format80
    {
        public static void DecodeInto(byte[] src, byte[] dest)
        {
            Stream ss = new MemoryStream(src);
            int di = 0;

            int v = ss.ReadByte();
            while (v != -1)
            {
                if ((v & 0x80) == 0)
                    RepeatCopy(ss, dest, ref di, (byte)v);
                else
                {
                    if ((v & 0x40) == 0)
                        CopyAsIs(ss, dest, ref di, (byte)v);
                    else
                    {
                        v = v & 0x3F;
                        if (v < 0x3E)
                            Copy(ss, dest, ref di, (byte)v);
                        else if (v == 0x3F)
                            LargeCopy(ss, dest, ref di);
                        else
                            Fill(ss, dest, ref di);
                    }
                }
                v = ss.ReadByte();
            }
        }

        private static void CopyAsIs(Stream ss, byte[] dest, ref int di, byte v)
        {
            int count = v & 0x3F;
            for (int j = 0; j < count; ++j)
                dest[di++] = ((byte)ss.ReadByte());
        }
        private static void RepeatCopy(Stream ss, byte[] dest, ref int di, byte v)
        {
            int count = ((v & 0x70) >> 4) + 3;
            int relPos = ((v & 0x0F) << 8) | ss.ReadByte();
            int pos = 0;

            if (relPos == 1)
            {
                pos = di - relPos;

                for (int i = 0; i < count; i++)
                    dest[di++] = dest[pos++];
            }
            else
            {
                v = dest[di - 1];
                for (int i = 0; i < count; i++)
                    dest[di++] = v;
            }
        }
        private static void Copy(Stream ss, byte[] dest, ref int di, byte v)
        {
            int count = ((v & 0x3F) + 3);
            int pos = ss.ReadByte() | ( ss.ReadByte() << 8 );
            
            for (int i = 0; i < count; i++)
                dest[di++] = dest[pos++];
        }
        private static void LargeCopy(Stream ss, byte[] dest, ref int di)
        {
            int count = ss.ReadByte() | (ss.ReadByte() << 8);
            int pos = ss.ReadByte() | (ss.ReadByte() << 8);

            for (int i = 0; i < count; i++)
                dest[di++] = dest[pos++];
        }
        private static void Fill(Stream ss, byte[] dest, ref int di)
        {
            int count = (ss.ReadByte() | (ss.ReadByte() << 8));
            byte v = (byte)ss.ReadByte();

            for (int i = 0; i < count; i++)
                dest[di++] = v;
        }

        public static byte[] Encode(byte[] src)
        {
            List<byte> dest = new List<byte>();

            int i = 0;//  Source Index
            int runLen;// Run Length
            int sPos;//   Sequence Start Position
            int sLen;//   Sequence Length
            int copyIndex = -1;// CopyFrom Index. For AS IS command.

            // Encoding
            while (i < src.Length)
            {
                sPos = 0;
                sLen = 0;

                runLen = Utils.GetRunLength(src, i, src.Length);
                GetSameSequence(src, i, ref sPos, ref sLen);

                // Determine which compress the most (RunLength or SameSequence).
                // IF RunLength or SameSequence's not effective enough, just COPY AS IS.
                if (runLen < sLen && sLen > 2)
                {
                    Flush_c1(src, dest, ref copyIndex, i - copyIndex);

                    if (sLen - 3 < 8 && i - sPos < 0x1000)
                        Write80_c0(dest, sLen, i - sPos);
                    else if (sLen - 3 < 0x3E)
                        Write80_c2(dest, sLen, sPos);
                    else
                        Write80_c4(dest, sLen, sPos);
                    i += sLen;
                }
                else
                {
                    if (runLen < 3)
                    {
                        // Setup COPY AS IS.
                        if (copyIndex < 0)
                            copyIndex = i;
                    }
                    else
                    {
                        Flush_c1(src, dest, ref copyIndex, i - copyIndex);
                        Write80_c3(dest, runLen, src[i]);
                    }
                    i += runLen;
                }
            }

            Flush_c1(src, dest, ref copyIndex, i - copyIndex);

            // Finish with 0x80 Command.
            Write80_c1(src, dest, 0, 0);
            return dest.ToArray();
        }

        private static void Flush_c1(byte[] src, List<byte> dest, ref int copyIndex, int copyLength)
        {
            if (copyIndex > -1)
            {
                Write80_c1(src, dest, copyIndex, copyLength);
                copyIndex = -1;
            }
        }

        private static void Write80_c0(List<byte> dest, int count, int relativePosition)
        {
            int p1 = ((count - 3) << 4) | (relativePosition >> 8); // 0ccccrrrr 
            int p2 = relativePosition & 0xFF; // rrrrrrrr

            dest.Add((byte)p1);
            dest.Add((byte)p2);
        }
        private static void Write80_c1(byte[] src, List<byte> dest, int copyIndex, int copyLength)
        {
            do
            {
                int count = copyLength < 0x40 ? copyLength : 0x3F;
                byte cmd = (byte)(0x80 | count);

                dest.Add(cmd);
                for (int i = 0; i < count; i++)
                    dest.Add(src[copyIndex++]);

                copyLength -= count;
            }
            while (copyLength > 0);
        }
        private static void Write80_c2(List<byte> dest, int sLen, int absolutePosition)
        {
            int cmd = (0xC0 | sLen - 3);

            dest.Add((byte)cmd);
            WriteWord(dest, absolutePosition);
        }
        private static void Write80_c3(List<byte> dest, int count, byte colorIndex)
        {
            dest.Add(0xFE);
            WriteWord(dest, count);
            dest.Add(colorIndex);
        }
        private static void Write80_c4(List<byte> dest, int count, int absolutePosition)
        {
            dest.Add(0xFF);
            WriteWord(dest, count);
            WriteWord(dest, absolutePosition);
        }
        private static void WriteWord(List<byte> dest, int value)
        {
            byte lo = (byte)(value & 0xFF);
            byte hi = (byte)(value >> 8);
            dest.Add(lo);
            dest.Add(hi);
        }

        /// <summary>
        /// Search an identical sequence of values, 
        /// starting at the beginning of the data array,
        /// that starts before the reference sequence.
        /// </summary>
        /// <param name="data">Data to perform the op. in.</param>
        /// <param name="refPos">Position of the reference sequence.</param>
        /// <param name="sPos">Position of the other same sequence.</param>
        /// <param name="sLen">Length of the sequences.</param>
        /// <remarks>
        /// The input is s <= r < s_end, output is p and cb_p.
        /// It searches for the longest string that starts < r that
        /// matches the string that starts at r (and ends before s_end)
        /// </remarks>
        private static void GetSameSequence(byte[] src, int refPos, ref int pos, ref int sLength)
        {
            int ri;    // Reference Position Index
            int si;    // Source Index
            int osi;   // Old Source Index (last si)
            int count; // Sequence length
            int ocount; // Old length

            pos = 0;
            osi = 0;
            si = 0;
            ocount = 0;

            // Next String
            while (si < refPos)
            {
                count = 0;
                ri = refPos;
                osi = si;

                while (ri < src.Length)
                {
                    if (src[si++] == src[ri])
                    {
                        ++ri;
                        ++count;
                    }
                    else
                    {
                        if (count > ocount)
                        {
                            ocount = count;
                            pos = osi;
                        }
                        break;
                    }
                }
            }
            sLength = ocount;
        }
    }
}
