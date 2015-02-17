using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MixManager
{
    static class Program
    {
        static readonly string DLG_DLL = "Ookii.Dialogs.dll";
        static readonly string CTRLS_DLL = "Ctrls.dll";
        //static readonly string ORA_DLL = "OpenRA-min.dll";
        static readonly string LOG4NET_DLL = "log4net.dll";
        static readonly string THREADING_DLL = "System.Threading.dll";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {


            List<MixDllInfo> dlls = new List<MixDllInfo>();
            List<MixDllInfo> missingDlls = new List<MixDllInfo>();
            dlls.Add(new MixDllInfo(DLG_DLL, false));
            dlls.Add(new MixDllInfo(CTRLS_DLL, false));
            dlls.Add(new MixDllInfo(LOG4NET_DLL, false));
            dlls.Add(new MixDllInfo(THREADING_DLL, false));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            foreach (var dll in dlls)
                dll.Exists = System.IO.File.Exists(dll.Name);

            if ((missingDlls = dlls.FindAll(d => !d.Exists)).Count > 0)
            {
                XmlConfigurator.Configure();
                FrmDllError frm = new FrmDllError();
                frm.ShowMissingDlls(missingDlls);
                Application.Run(frm);
            }
            else
            {
                Application.Run(new FrmMixManager());
            }
            
        }
    }
    public class MixDllInfo
    {
        readonly string EXE_DIR = System.IO.Path.GetFullPath("./");

        public string Name { get; private set; }
        public string FullName { get; private set; }
        public bool Exists { get; set; }
        public MixDllInfo(string name, bool exists)
        {
            Name = name;
            FullName = System.IO.Path.Combine(EXE_DIR, name);
            Exists = exists;
        }
    }
}
