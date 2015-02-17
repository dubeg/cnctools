using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShpLib.Formats
{
    public static class Format40
    {
        public static void DecodeInto(byte[] src, byte[] dest)
        {
            int sIndex = 0; // Source      Buffer Index
            int dIndex = 0; // Destination Buffer Index

            Byte v;// Byte Value
            Byte colorIndex;

            int count = 0;
            int tc = 0;

            while (sIndex < src.Length)
            {
                v = src[sIndex++];

                if ((v & 0x80) != 0)
                {
                    if (v != 0x80)
                    {
                        // CMD #1 : Small Skip
                        //-------------------------------------
                        count = v & 0x7F;
                        dIndex += count;
                    }
                    else
                    {
                        count = Utils.CombineBytes(src[sIndex], src[sIndex + 1]);
                        if (count == 0) break;
                        sIndex += 2;

                        tc = (count & 0xC000) >> 14;

                        if (tc == 0 || tc == 1)
                        {
                            // CMD #2 : Big Skip
                            //-------------------------------------
                            dIndex += count;
                        }
                        else if (tc == 2)
                        {
                            // CMD #3 : Big XOR
                            //-------------------------------------
                            count = count & 0x3FFF;

                            for (int j = 0; j < count; ++j)
                            {
                                dest[dIndex] = (byte)(dest[dIndex] ^ src[sIndex++]);
                                ++dIndex;
                            }
                        }
                        else if (tc == 3)
                        {
                            // CMD #4 : Big Repeated XOR
                            //-------------------------------------
                            count = count & 0x3FFF;
                            colorIndex = src[sIndex++];

                            for (int j = 0; j < count; ++j)
                            {
                                dest[dIndex] = (byte)(dest[dIndex] ^ colorIndex);
                                ++dIndex;
                            }
                        }
                        else
                        {
                            throw new Exception("DecodeFormat40 Error. Invalid tc value.");
                        }
                    }

                }
                else
                {
                    // XOR CMDs
                    if (v == 0)
                    {
                        // CMD #6 : REPEATED XOR
                        //-------------------------------------
                        count = src[sIndex++];
                        colorIndex = src[sIndex++];

                        for (int j = 0; j < count; ++j)
                        {
                            dest[dIndex] = (byte)(dest[dIndex] ^ colorIndex);
                            ++dIndex;
                        }
                    }
                    else
                    {
                        // CMD #5 : COPY XOR
                        //-------------------------------------
                        count = v;

                        for (int j = 0; j < count; ++j)
                        {
                            dest[dIndex] = (byte)(dest[dIndex] ^ src[sIndex++]);
                            ++dIndex;
                        }
                    }
                }
            }
        }
    }
}
