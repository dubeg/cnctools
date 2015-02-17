#region Copyright & License Information
/*
 * Copyright 2007-2015 The OpenRA Developers (see AUTHORS)
 * This file is part of OpenRA, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation. For more information,
 * see COPYING.
 */
#endregion

using MixManager.Util;
using MixManager.Models;
using System.Collections.Generic;
using System.IO;

namespace MixManager.Xcc
{
	public class XccGlobalDatabase
	{
        // Vars
        // ---------
        public readonly Dictionary<string, Dictionary<uint, string[]>> Dictionaries;
        // Fn
        // ---------
		public XccGlobalDatabase(Stream s)
		{
            Dictionaries = new Dictionary<string, Dictionary<uint, string[]>>();

            // add default dictionaries
            Dictionaries.Add("td", new Dictionary<uint, string[]>());
            Dictionaries.Add("ra", new Dictionary<uint, string[]>());
            Dictionaries.Add("ts", new Dictionary<uint, string[]>());
            Dictionaries.Add("ra2", new Dictionary<uint, string[]>());

			var reader = new BinaryReader(s);
            ReadList(reader, GetDictionary(XccLists.TD), MixHashType.Classic);
            ReadList(reader, GetDictionary(XccLists.RA), MixHashType.Classic);
            ReadList(reader, GetDictionary(XccLists.TS), MixHashType.CRC32);
            ReadList(reader, GetDictionary(XccLists.RA2), MixHashType.CRC32);
            s.Dispose();
		}

        private void ReadList(BinaryReader reader, Dictionary<uint, string[]> dict, MixHashType type)
        {
            string[] entry;
            string name, desc;
            var count = reader.ReadInt32();

            for (var i = 0; i < count; i++)
            {
                // Read filename
                name = ReadString(reader);
                // Read desc
                desc = ReadString(reader);

                entry = new string[2];
                entry[0] = name;
                entry[1] = desc;
                uint hash = MixUtil.HashFilename(entry[0], type);
                dict.Add(hash, entry);
            }
        }

        private string ReadString(BinaryReader reader)
        {
            List<char> chars = new List<char>();
            char c;

            while ((c = reader.ReadChar()) != 0)
                chars.Add(c);
            return new string(chars.ToArray());
        }

        public Dictionary<uint, string[]> GetDictionary(XccLists list)
        {
            switch (list)
            {
                case XccLists.TD:
                    return Dictionaries["td"];
                case XccLists.TS:
                    return Dictionaries["ts"];
                case XccLists.RA:
                    return Dictionaries["ra"];
                case XccLists.RA2:
                    return Dictionaries["ra2"];
                default:
                    throw new System.Exception();
            }
        }
	}
    // Enum
    // ---------
    public enum XccLists
    {
        TD,
        TS,
        RA,
        RA2
    }
}