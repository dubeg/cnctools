#region Copyright & License Information
/*
 * Copyright 2007-2015 The OpenRA Developers (see AUTHORS)
 * This file is part of OpenRA, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation. For more information,
 * see COPYING.
 */
#endregion

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MixManager;
using MixManager.Models;
using MixManager.Util;

namespace MixManager.Xcc
{
	public class XccLocalDatabase
	{
		public readonly Dictionary<uint, string> Entries;

        // Load
		public XccLocalDatabase(Stream s, MixHashType type)
		{
            Entries = new Dictionary<uint, string>();

			// Skip unnecessary header data
			s.Seek(48, SeekOrigin.Current);
			var reader = new BinaryReader(s);


            var count = reader.ReadInt32();
            string entry;
            for (var i = 0; i < count; i++)
            {
                var chars = new List<char>();
                char c;
                while ((c = reader.ReadChar()) != 0)
                    chars.Add(c);

                entry = new string(chars.ToArray());
                var hash = MixUtil.HashFilename(entry, type);
                if (!Entries.ContainsKey(hash)) Entries.Add(hash, entry);
            }

            s.Dispose();
		}

        // Get bytes
		public byte[] Data()
		{
			var data = new MemoryStream();
            string[] entries = Entries.Values.ToArray();
			using (var writer = new BinaryWriter(data))
			{
				writer.Write(Encoding.ASCII.GetBytes("XCC by Olaf van der Spek"));
				writer.Write(new byte[] { 0x1A, 0x04, 0x17, 0x27, 0x10, 0x19, 0x80, 0x00 });

                writer.Write((int)(entries.Aggregate(entries.Length, (a, b) => a + b.Length) + 52)); // Size
				writer.Write((int)0); // Type
				writer.Write((int)0); // Version
				writer.Write((int)0); // Game/Format (0 == TD)
                writer.Write((int)entries.Length); // Entries
                foreach (var e in entries)
				{
					writer.Write(Encoding.ASCII.GetBytes(e));
					writer.Write((byte)0);
				}
			}

			return data.ToArray();
		}

        // Add entries
        public XccLocalDatabase(IEnumerable<string> filenames)
        {
            throw new System.NotImplementedException();
        }
	}
}
