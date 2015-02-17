using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misc;

namespace MixManager.Models
{
    public class MixEntry : NotifiedModel, IMixEntry
    {
        public const string MIX_FILE_EXTENSION = ".mix";
        public const int HEADER_SIZE = 12;
        // Vars
        // -------
        protected string _fn;
        protected string _type;
        protected EntryStates _state;
        protected uint _hash;
        protected uint _offset;
        protected uint _length;
        // Props
        // -------
        public bool NameResolved { get; private set; }
        public uint Hash { get { return _hash; } }
        public uint Offset { get { return _offset; } }
        public uint Length { get { return _length; } }
        public string FullName { get { return _fn; } }
        public string SafeName { get { return Path.GetFileName(FullName); } }
        public string Type { get { return _type; } }
        public string Description { get; set; }
        public EntryStates State { get { return _state; } set { SetField(ref _state, value, "State"); } }
        public IMixPackage Parent { get; private set; }
        // Methods
        // -------
        public MixEntry(string fullname) // Root package
        {
            _fn = fullname;
            _type = GetEntryType(fullname);
        }

        public MixEntry(IMixEntry mE) // Sub package
        {
            Parent = mE.Parent;
            _state = mE.State;
            _hash = mE.Hash;
            _length = mE.Length;
            _offset = mE.Offset;
            _type = mE.Type;
            _fn = mE.FullName;
            NameResolved = mE.NameResolved;
        }

        public MixEntry(IMixPackage parent, Stream s) // Existing entry
        {
            Parent = parent;
            _hash = s.ReadUInt32();
            _offset = s.ReadUInt32();
            _length = s.ReadUInt32();
            _fn = _hash.ToString("X");
            _type = "File";
        }

        public string GetPath()
        {
            string pathInMix = SafeName;
            if ((Parent != null))
                pathInMix = Parent.GetPath() + "\\" + pathInMix;
            return pathInMix;
        }

        public void SetName(string name)
        {
            _fn = name;
            _type = GetEntryType(name);
            NameResolved = true;
        }

        private string GetEntryType(string filename)
        {
            string type = string.Empty;
            string ext = Path.GetExtension(filename);
            switch (ext)
            {
                case "":
                    type = "???";
                    break;
                case ".ini":
                case ".txt":
                    type = "text";
                    break;
                case ".mix":
                    type = "mix";
                    break;
                default:
                    type = ext.TrimStart('.');
                    break;
            }
            return type;
        }
        // Static
        // -------
        public static bool IsMixFile(string fn)
        {
            return Path.GetExtension(fn) == MIX_FILE_EXTENSION;
        }
    }
}
