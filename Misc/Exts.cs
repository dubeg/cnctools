using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Misc
{
    public static class Exts
    {
        // Only useful before .NET 4
        public static void CopyTo(this Stream input, Stream output)
        {
            byte[] buffer = new byte[16 * 1024]; // Fairly arbitrary size
            int bytesRead;

            while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, bytesRead);
            }
        }


       public static T Merge<T, K, V>(this T me, params IDictionary<K, V>[] others)
       where T : IDictionary<K, V>
        {
            foreach (IDictionary<K, V> src in others)
            {
                foreach (KeyValuePair<K, V> p in src)
                {
                    if(!me.ContainsKey(p.Key))
                        me[p.Key] = p.Value;
                }
            }
            return me;
        }
    }
}
