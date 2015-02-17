using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ShpLib
{
    public static class Utils
    {
        /// <summary>
        /// Split word in 2 bytes
        /// Note: could use BitConverter from System.BitConverter
        /// </summary>
        /// <param name="word"></param>
        /// <param name="loByte"></param>
        /// <param name="hiByte"></param>
        public static void SplitWord(ushort word, out Byte loByte, out Byte hiByte)
        {
            loByte = (Byte)(word & 0xFF);
            hiByte = (Byte)(word >> 8);
        }


        /// <summary>
        /// Combine 2 bytes as word
        /// Note: Note: could use BitConverter from System.BitConverter
        /// </summary>
        /// <param name="loByte"></param>
        /// <param name="hiByte"></param>
        /// <returns></returns>
        public static ushort CombineBytes(Byte loByte, Byte hiByte)
        {
            return (ushort)((hiByte << 8) | loByte);
        }


        /// <summary>
        /// Get Run length
        /// 
        /// Returns nbr of bytes equal to the current byte
        /// from the start position to the last.
        /// </summary>
        /// <param name="source">Bytes to search in</param>
        /// <param name="startPos">Start Position in source</param>
        /// <param name="maxPos">Last position in source</param>
        /// <returns></returns>
        public static int GetRunLength(Byte[] source, int startPos, int maxPos)
        {
            byte v;
            int count;

            count = 1;
            v = source[startPos];
            ++startPos;

            while (startPos < maxPos && source[startPos] == v)
            {
                ++startPos;
                ++count;
            }

            return count;
        }


        /// <summary>
        /// Find nearest upper multiple
        /// </summary>
        /// <param name="numToRound"></param>
        /// <param name="multiple"></param>
        /// <returns></returns>
        public static int NextMultiple(int numToRound, int multiple)
        {
            if (multiple == 0)
            {
                return numToRound;
            }

            int remainder = numToRound % multiple;
            if (remainder == 0)
                return numToRound;
            return numToRound + multiple - remainder;
        }  

    }
}
