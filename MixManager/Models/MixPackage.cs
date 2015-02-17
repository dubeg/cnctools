using MixManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MixManager.Xcc;
using Misc;
using MixManager.Util;
using Misc.Blowfish;

namespace MixManager
{
    public class MixPackage : MixEntry, IMixPackage
    {
        // Vars
        // ---------
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private Dictionary<uint, IMixEntry> _entries;
        private long _dataStart;
        private MixHashType _hType;
        public bool IsLoaded { get; private set; }
        public MixHashType MixHashType { get { return MixHashType.CRC32; } }
        public Dictionary<uint, IMixEntry> Entries { get { return _entries; } }
        // Public
        // ---------
        public MixPackage(string fullname) : base(fullname) // Root package ctor
        {
            _hash = 0;
        }

        public MixPackage(IMixEntry mE) : base(mE){}

        public Stream GetContent(uint hash)
        {
            long offset;

            Stream fs;
            if (Parent != null)
            {
                // Sub 
                fs = Parent.GetContent(_hash);
            }
            else
            // Root
            {
                fs = new FileStream(FullName, FileMode.Open);
            }

            offset = _dataStart;
            if (hash != 0)
                offset += _entries[hash].Offset;

            //debug:this fucking line made me RAGE
            fs.Seek(offset, SeekOrigin.Begin);
            return fs;
        }

        public void AddEntries(string[] filenames)
        {
            throw new NotImplementedException();
        }

        public void RemoveEntries(string[] filenames)
        {
            throw new NotImplementedException();
        }

        public void ExtractTo(IMixEntry[] entries, string dirPath)
        {
            foreach (var entry in entries)
            {
                using (Stream s = GetContent(entry.Hash))
                {
                    string fullname = Path.Combine(dirPath, entry.SafeName);
                    byte[] data = s.ReadBytes((int)entry.Length);
                    // todo write 
                    File.WriteAllBytes(fullname, data);
                }
            }
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void SaveChangesTo(string fullname)
        {
            throw new NotImplementedException();
        }

        public void LoadEntries()
        {
            Stream s;
            _entries = new Dictionary<uint, IMixEntry>();
            if (Parent != null)
                // Inside package
                s = Parent.GetContent(_hash);
            else
                s = GetContent(0);

            long sPos = s.Position;
            // Read mix header
            var isCncMix = (s.ReadUInt16() != 0);
            var isEncrypted = isCncMix ? false : (s.ReadUInt16() & 0x2) != 0;
            _hType = isCncMix ? MixHashType.Classic : MixHashType.CRC32;

            long unused;
            if (isEncrypted)
            {
                ParseEntries(DecryptHeader(s, sPos+4, out _dataStart), 0, out unused);
            }
            else
                ParseEntries(s, isCncMix ? sPos : sPos+4, out _dataStart);

            s.Dispose();
            IsLoaded = true;
        }

        public uint[] ResolveEntries(Dictionary<uint, string[]> entries)
        {
            List<uint> unresolvedIDs = new List<uint>();
            Dictionary<uint, MixPackage> changes = new Dictionary<uint, MixPackage>();
            foreach (KeyValuePair<uint, IMixEntry> kvp in _entries)
            {
                if (entries.ContainsKey(kvp.Value.Hash))
                {
                    string[] metadata = entries[kvp.Value.Hash];
                    kvp.Value.SetName(metadata[0]);
                    kvp.Value.Description = metadata[1];

                    if (MixEntry.IsMixFile(metadata[0]))
                        changes.Add(kvp.Value.Hash, new MixPackage(kvp.Value));
                }
                else
                {
                    log.Warn(String.Format("Unresolved: {0} - {1}", kvp.Key, kvp.Value.SafeName));
                    unresolvedIDs.Add(kvp.Key);
                }
            }

            foreach (var change in changes)
            {
                _entries[change.Key] = change.Value;
            }
            return unresolvedIDs.ToArray();
        }

        // Private
        // -------
        private void ParseEntries(Stream s, long offset, out long headerEnd)
        {
            s.Seek(offset, SeekOrigin.Begin);
            var numFiles = s.ReadUInt16();
            s.ReadUInt32();

            for (var i = 0; i < numFiles; i++)
            {
                MixEntry mE = new MixEntry(this, s);
                _entries.Add(mE.Hash, mE);
            }

            headerEnd = offset + 6 + numFiles * MixEntry.HEADER_SIZE;
        }

        private MemoryStream DecryptHeader(Stream s, long offset, out long headerEnd)
        {
            s.Seek(offset, SeekOrigin.Begin);

            // Decrypt blowfish key
            var keyblock = s.ReadBytes(80);
            var blowfishKey = new BlowfishKeyProvider().DecryptKey(keyblock);
            var fish = new Blowfish(blowfishKey);

            // Decrypt first block to work out the header length
            var ms = Decrypt(ReadBlocks(s, offset + 80, 1), fish);
            var numFiles = ms.ReadUInt16();

            // Decrypt the full header - round bytes up to a full block
            var blockCount = (13 + numFiles * MixEntry.HEADER_SIZE) / 8;
            headerEnd = offset + 80 + blockCount * 8;

            return Decrypt(ReadBlocks(s, offset + 80, blockCount), fish);
        }

        private MemoryStream Decrypt(uint[] h, Blowfish fish)
        {
            var decrypted = fish.Decrypt(h);

            var ms = new MemoryStream();
            var writer = new BinaryWriter(ms);
            foreach (var t in decrypted)
                writer.Write(t);
            writer.Flush();

            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }

        private uint[] ReadBlocks(Stream s, long offset, int count)
        {
            s.Seek(offset, SeekOrigin.Begin);

            // A block is a single encryption unit (represented as two 32-bit integers)
            var ret = new uint[2 * count];
            for (var i = 0; i < ret.Length; i++)
                ret[i] = s.ReadUInt32();

            return ret;
        }
    }
}
