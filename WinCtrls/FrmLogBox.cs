using Log4NetEx;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinCtrls
{
    public partial class FrmLogBox : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private LogWatcher _logWatcher;

        public FrmLogBox(LogWatcher logWatcher)
        {
            InitializeComponent();
            _logWatcher = logWatcher;
            _logWatcher.Updated += _logWatcher_Updated;
        }

        void _logWatcher_Updated(object sender, EventArgs e)
        {
            if (tbConsole.InvokeRequired)
            {
                tbConsole.BeginInvoke(new Action(() => { _logWatcher_Updated(sender, e); }));
                return;
            }

            while (_logWatcher.Logs.Count > 0)
            {
                tbConsole.AppendText( _logWatcher.Logs.Dequeue() );
            }
        }
    }
}
