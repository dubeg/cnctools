using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ShpLib
{
    public static class PalEngine
    {
        /// <summary>
        /// Read palette from disk.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static Color[] Load(string filename)
        {
            Color[] colors = new Color[256];
            byte[] bytes = File.ReadAllBytes(filename);

            using (MemoryStream ms = new MemoryStream(bytes))
            {
                using (BinaryReader reader = new BinaryReader(ms))
                {
                    

                    byte r;
                    byte g;
                    byte b;

                    for (int i = 0; i < colors.Length; ++i)
                    {
                        r = (byte)(reader.ReadByte() << 2);
                        g = (byte)(reader.ReadByte() << 2);
                        b = (byte)(reader.ReadByte() << 2);

                        colors[i] = new Color(r, g, b);
                    }
                }
            }
            return colors;
        }


        /// <summary>
        /// Write palette to disk.
        /// </summary>
        /// <param name="palette"></param>
        public static void Write(Color[] colors, string filename)
        {
            byte[] bytes;
            bytes = new byte[colors.Length * 3];

            int dataIndex = 0;
            for (int i = 0; i < colors.Length; ++i)
            {
                bytes[dataIndex++] = (byte)(colors[i].Red >> 2);
                bytes[dataIndex++] = (byte)(colors[i].Green >> 2);
                bytes[dataIndex++] = (byte)(colors[i].Blue >> 2);
            }

            File.WriteAllBytes(filename, bytes);
        }
    }
}
