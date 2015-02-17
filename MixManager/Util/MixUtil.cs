using Misc.Blowfish;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MixManager.Util
{
    public enum MixHashType { Classic, CRC32 }
    public static class MixUtil
    {
        public static uint HashFilename(string name, MixHashType type)
        {
            switch (type)
            {
                case MixHashType.Classic:
                    {
                        name = name.ToUpperInvariant();
                        if (name.Length % 4 != 0)
                            name = name.PadRight(name.Length + (4 - name.Length % 4), '\0');

                        var ms = new MemoryStream(Encoding.ASCII.GetBytes(name));
                        var reader = new BinaryReader(ms);

                        var len = name.Length >> 2;
                        uint result = 0;

                        while (len-- != 0)
                            result = ((result << 1) | (result >> 31)) + reader.ReadUInt32();

                        return result;
                    }

                case MixHashType.CRC32:
                    {
                        name = name.ToUpperInvariant();
                        var l = name.Length;
                        var a = l >> 2;
                        if ((l & 3) != 0)
                        {
                            name += (char)(l - (a << 2));
                            var i = 3 - (l & 3);
                            while (i-- != 0)
                                name += name[a << 2];
                        }

                        return CRC32.Calculate(Encoding.ASCII.GetBytes(name));
                    }

                default: throw new NotImplementedException(String.Format("Unknown hash type: {0}", name));
            }
        }
        public const string XCC_LOCAL_MIX_DATABASE_FILENAME = "local mix database.dat";
        public static readonly uint XCC_LOCAL_MIX_DATABASE_HASH_CRC32 = HashFilename(XCC_LOCAL_MIX_DATABASE_FILENAME, MixHashType.CRC32);
        public static readonly uint XCC_LOCAL_MIX_DATABASE_HASH_CLASSIC = HashFilename(XCC_LOCAL_MIX_DATABASE_FILENAME, MixHashType.CRC32);
        public static uint GetLocalMixDatabaseHash(MixHashType h)
        {
            switch (h)
            {
                case MixHashType.Classic:
                    return XCC_LOCAL_MIX_DATABASE_HASH_CLASSIC;
                case MixHashType.CRC32:
                    return XCC_LOCAL_MIX_DATABASE_HASH_CRC32;
                default:
                    throw new Exception("Unimplemented hash type.");// dubeg:throw specific exception
            }
        }
    }
}
