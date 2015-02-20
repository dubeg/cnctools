using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ShpLib
{
    public static class Engine
    {
        /// <Notes>
        /// Todo:
        /// * One Decode/Encode fn each for V1 and V2
        /// * Provide option 'DecodeOptionsV2.DontUnpad'
        /// </Notes>

        public static Frame[] Decode(byte[] data, DecodingOptions option)
        {
            Frame[] frames = null;

            switch (option)
            {
                case DecodingOptions.ShpV1:
                    frames = V1.DecoderV1.Decode(data);
                    break;
                case DecodingOptions.ShpV2:
                    frames = V2.DecoderV2.Decode(data);
                    break;
                case DecodingOptions.AutoDetect:
                default:
                    frames = TryDecodingAny(data);
                    break;
            }

            return frames;
        }

        public static Frame[] TryDecodingAny(byte[] data)
        {
            Frame[] frames = null;

            try
            {
                frames = V1.DecoderV1.Decode(data);
            }
            catch (Exception)
            {
                frames = V2.DecoderV2.Decode(data);
            }

            return frames;
        }

        public static Frame[] Load(string filename, DecodingOptions option)
        {
            byte[] data = File.ReadAllBytes(filename);
            return Decode(data, option);
        }

        public static byte[] Encode(byte[][] framesData, ushort width, ushort height, EncodingOptions option)
        {
            if (option == EncodingOptions.ShpV1)
                return V1.EncoderV1.Encode(framesData, width, height);
            else
                return V2.EncoderV2.Encode(framesData, width, height, new Color(0,0,0));
        }

        public static void Save(string filename, byte[][] framesData, ushort width, ushort height, EncodingOptions option)
        {
            byte[] bytes = null;
            switch (option)
            {
                case EncodingOptions.ShpV1:
                    bytes = ShpLib.V1.EncoderV1.Encode(framesData, width, height);
                    break;
                case EncodingOptions.ShpV2:
                    bytes = ShpLib.V2.EncoderV2.Encode(framesData, width, height, new Color(0,0,0));
                    break;
            }

            File.WriteAllBytes(filename, bytes);

        }


        public static byte[] EncodeSHPv2(byte[][] framesData, ushort width, ushort height, Color radarColor)
        {
            return V2.EncoderV2.Encode(framesData, width, height, radarColor);
        }

        public static void SaveSHPv2(string filename, byte[][] framesData, ushort width, ushort height, Color radarColor)
        {
            byte[] bytes = ShpLib.V2.EncoderV2.Encode(framesData, width, height, radarColor);
            File.WriteAllBytes(filename, bytes);
        }
    }
}
