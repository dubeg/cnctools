using MixManager.Util;
using MixManager.Xcc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixManager.Models
{
    public interface IMixPackage : IMixEntry
    {
        MixHashType MixHashType { get; }
        Dictionary<uint, IMixEntry> Entries { get; }
        bool IsLoaded { get; }
        void LoadEntries();
        void AddEntries(string[] filenames);
        void RemoveEntries(string[] filenames);
        Stream GetContent(uint hash);
        void ExtractTo(IMixEntry[] entries, string dirPath);
        uint[] ResolveEntries(Dictionary<uint, string[]> dictionary);
        void SaveChanges();
        void SaveChangesTo(string fullname);
    }
}
