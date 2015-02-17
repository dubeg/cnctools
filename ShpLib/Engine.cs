using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ShpLib
{
    public class Engine
    {
        /// <Notes>
        /// Todo:
        /// * One Decode/Encode fn each for V1 and V2
        /// * Provide option 'DecodeOptionsV2.DontUnpad'
        /// </Notes>

        public Engine(){}

        public static Frame[] Decode(byte[] data, DecodingOptions option)
        {
            switch (option)
            {
                case DecodingOptions.ShpV1:
                    return V1.DecoderV1.Decode(data);
                case DecodingOptions.ShpV2:
                    return V2.DecoderV2.Decode(data);
                case DecodingOptions.AutoDetect:
                default:
                    return TryDecodingAny(data);
            }    
        }

        public static Frame[] TryDecodingAny(byte[] data)
        {
            return V1.DecoderV1.Decode(data);

            //return V2.DecoderV2.Decode(data);

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
                return V2.EncoderV2.Encode(framesData, width, height);
        }
    }
}
