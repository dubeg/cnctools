using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixManager.Models
{
    public interface IMixEntry
    {
        bool NameResolved { get; }
        string SafeName { get; }
        string FullName { get; }
        string Type { get; }
        string Description { get; set; }
        uint Hash { get; }
        uint Offset { get; }
        uint Length { get; }

        EntryStates State { get; set; }
        IMixPackage Parent { get; }

        void SetName(string name);
        string GetPath();
        // Should this be here or in IMixPackage?
        //Stream GetContent(uint hash);
    }
    public enum EntryStates { Unchanged, Added, Removed }
}
