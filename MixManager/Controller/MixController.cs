using MixManager.Models;
using Misc.Threading;
using MixManager.Xcc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MixManager.Util;

namespace MixManager
{
    public class MixController
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private enum TaskTypes
        {
            Saving,
            Extracting
        }

        // Const
        // --------
        const string _GLOBAL_MIX_DATABASE_FILENAME = "global mix database.dat";
        const string _LOCAL_MIX_DATABASE_FILENAME = "local mix database.dat";
        readonly string _EXECUTABLE_DIRECTORY = Path.GetFullPath("./");
        // Vars
        // --------
        private ConcurrentHashSet<Guid> _tasks;
        private XccGlobalDatabase _xgdb;

        public bool AutoDetectNameDictionary { get; set; }
        public XccLists CurrentNameDictionary { get; set; }
        public IMixPackage Root { get; private set; }
        public IMixPackage CurrentPackage { get; private set; }
        public bool IsRootLoaded { get { return Root != null; } }
        public bool AreTasksRunning { get { return _tasks.Count > 0; } }
        // Public fn
        // --------
        public MixController() 
        {
            _tasks = new ConcurrentHashSet<Guid>();
            InitXccSettings();
        }

        private void InitXccSettings()
        {
            TryLoadXccGlobalDatabase();
            AutoDetectNameDictionary = true;
        }

        private void TryLoadXccGlobalDatabase()
        {
            try
            {
                _xgdb = new XccGlobalDatabase(
                   new FileStream(
                       _GLOBAL_MIX_DATABASE_FILENAME,
                       FileMode.Open, FileAccess.Read)
                   );
            }
            catch (FileNotFoundException)
            {
                log.Warn("X Global DB not found.");
            }
        }

        public void OpenRoot(string filename)
        {
            try
            {
                SetupCurrentPackage(new MixPackage(filename));
                Root = CurrentPackage;
            }
            catch (IOException e)
            {
                log.Error(e.Message);
            }
        }

        public void CloseRoot()
        {
            if (_tasks.Count > 0) throw new Exception("Could not close, task in progress.");//Todo: new exception class

            CurrentPackage = null;
            Root = null;
        }

        public void OpenSubFolder(IMixPackage mE)
        {
            SetupCurrentPackage(mE);
        }

        public void UpOneLevel()
        {
            if (CurrentPackage != null && CurrentPackage.Parent != null)
            {
                CurrentPackage = (IMixPackage)CurrentPackage.Parent;
            }
        }

        public Task OpenSubFolderAsync(IMixPackage mE)
        {
            return Task.Factory.StartNew( () =>
            {
                OpenSubFolder(mE);
            });
        }

        public Task OpenRootAsync(string filename)
        {
            return Task.Factory.StartNew(() =>
            {
                OpenRoot(filename);
            });
        }

        public Task ExtractFilesToAsync(IMixEntry[] entries, string dirPath)
        {
            return Task.Factory.StartNew(() =>
            {
                Guid id = Guid.NewGuid();
                _tasks.Add(id);
                ExtractFilesTo(entries, dirPath);
                _tasks.Remove(id);
            });
        }

        public void ExtractFilesTo(IMixEntry[] entries, string dirPath)
        {
            CurrentPackage.ExtractTo(entries, dirPath);
        }

        public void AddEntries(string[] filenames)
        {
            CurrentPackage.AddEntries(filenames);
        }
        // Private fn
        // --------
        private void SetupCurrentPackage(IMixPackage mP)
        {
            if (!mP.IsLoaded)
            {
                log.Info("Loading " + mP.SafeName.ToUpper());
                // load
                mP.LoadEntries();
                // resolve names
                //  1. get xgdb
                //  2. get xldb
                //  3. merge dbs
                //  4. resolve

                // dubeg: should put resolving logic in mixpackage.loadentries?
                // dubeg: maybe a INameResolver inject in LoadEntries() or smth, with one impl containing xcc db entries for resolving
                Dictionary<uint, string[]> xgDict;
                if (AutoDetectNameDictionary) TryAutoDetectNameDictionary(mP.SafeName);
                if (_xgdb != null)
                    xgDict = _xgdb.GetDictionary(CurrentNameDictionary);
                else
                    xgDict = new Dictionary<uint, string[]>();

                var xldbHash = MixUtil.GetLocalMixDatabaseHash(mP.MixHashType);
                if (mP.Entries.ContainsKey(xldbHash))
                {
                    XccLocalDatabase xldb = new XccLocalDatabase(mP.GetContent(xldbHash), mP.MixHashType);
                    MergeNameDictionaries(xgDict, xldb.Entries);
                }
                uint[] unresolvedIDs = mP.ResolveEntries(xgDict);
                int unresolvedCount = TryResolveNames(unresolvedIDs, mP);
                log.Info(String.Format("Resolved names {0}/{1}", mP.Entries.Count - unresolvedCount, mP.Entries.Count));
            }
            CurrentPackage = mP;
        }

        private int TryResolveNames(uint[] ids, IMixPackage mP)
        {
            int resolvedCount = 0;
            foreach (var id in ids)
            {
                foreach (var dict in _xgdb.Dictionaries)
                {
                    if (dict.Value.ContainsKey(id))
                    {
                        mP.Entries[id].SetName(dict.Value[id][0]);
                        mP.Entries[id].Description = dict.Value[id][1];
                        log.Info(String.Format("Resolved <{0}> with <{1}> in list <{2}>", id, dict.Value[id][0], dict.Key));
                        ++resolvedCount;
                        break;
                    }
                }
            }
            return ids.Length - resolvedCount;
        }

        private void MergeNameDictionaries(Dictionary<uint, string[]> xgDict, Dictionary<uint, string> xlDict)
        {
            foreach (var kvp in xlDict)
            {
                if (!xgDict.ContainsKey(kvp.Key)) xgDict.Add(kvp.Key, new string[]{kvp.Value, string.Empty});
            }
        }

        private void TryAutoDetectNameDictionary(string mixName)
        {
            mixName = mixName.ToLower();

            if (mixName.StartsWith("tibsun"))
            {
                CurrentNameDictionary = XccLists.TS;
                log.Info("Dectecting mix(ts)");
            }
            else if (mixName.StartsWith("ra2"))
            {
                CurrentNameDictionary = XccLists.RA2;
                log.Info("Dectecting mix(ra2)");
            }
            // dubeg: add more
        }
    }


}
