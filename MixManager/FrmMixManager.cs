using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MixManager.Models;
using System.Threading;
using Misc.Log4net;
using Misc;

namespace MixManager
{
    public partial class FrmMixManager : Form
    {
        // Const
        // --------
        readonly string REL_PATH_IMAGE_REMOVE = Path.GetFullPath("Icons/remove.png");
        readonly string REL_PATH_IMAGE_EXTRACT = Path.GetFullPath("Icons/extract.png");
        readonly string REL_PATH_IMAGE_ADD = Path.GetFullPath("Icons/add.png");
        readonly string _OPEN_FILE_DLG_TITLE = "Open .mix";
        readonly string _OPEN_FILE_DLG_FILTER = "Mix file (*.mix)|*.mix";
        readonly string _OPEN_FILE_DLG_EXT = ".mix";
        readonly string _EXTRACT_TO_DLG_TITLE = "Extract To";
        // Vars
        // --------
        MixController _mixController;
        List<IMixEntry> _cache;
        List<IMixEntry> _results;
        ContextMenuStrip _entryContextMenu;

        FrmLogBox _logBox;
        LogWatcher _logWatcher;
        WinCtrls.PathCtrl pCtrl;
        // Methods
        // --------
        public FrmMixManager()
        {
            InitializeComponent();
            pCtrl = new WinCtrls.PathCtrl();
            this.Controls.Add(pCtrl);

            _entryContextMenu = new ContextMenuStrip();
            _results = new List<IMixEntry>();

            this.Move += FrmMixManager_Move;
            this.Resize += FrmMixManager_Move;
        }

        private void FrmMixManager_Load(object sender, EventArgs e)
        {
            _logWatcher = new LogWatcher();
            CreateLogBox();
            _mixController = new MixController();
            
            InitializeGridView();
            InitializeEntryContextMenu();
            ToggleBrowser();
        }

        private void CreateLogBox()
        {
            _logBox = new FrmLogBox(_logWatcher);
            _logBox.Height = 150;
            _logBox.FormClosed += _logBox_FormClosed;
            _logBox.Show(this);
            FrmMixManager_Move(this, null);
        }

        void _logBox_FormClosed(object sender, FormClosedEventArgs e)
        {
            _logBox = null;
        }

        void FrmMixManager_Move(object sender, EventArgs e)
        {
            if (_logBox != null)
            {
                _logBox.Top = this.Bottom;
                _logBox.Width = this.Width;
                _logBox.Left = this.Left;
            }
        }

        void tsExtract_Click(object sender, EventArgs e)
        {
            ExtractFilesWithDialog();
        }

        void tsRemove_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ExtractSelectedEntriesTo(string dirPath)
        {
            IMixEntry[] entries = new IMixEntry[gvBrowser.SelectedRows.Count];
            for (int i = 0; i < entries.Length; i++)
            {
                entries[i] = _results.Count > 0 ? _results[gvBrowser.SelectedRows[i].Index] : _cache[gvBrowser.SelectedRows[i].Index];
            }

            btnClose.Enabled = false;
            Task t = _mixController.ExtractFilesToAsync(entries, dirPath);
            t.ContinueWith((t1) => { OnTaskExtractCompleted(); });
        }

        private void OnTaskExtractCompleted()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    btnClose.Enabled = true;
                }));
            }
        } 

        private void ExtractFilesWithDialog()
        {
            using (Ookii.Dialogs.VistaFolderBrowserDialog dlg = new Ookii.Dialogs.VistaFolderBrowserDialog())
            {
                dlg.ShowNewFolderButton = true;
                dlg.UseDescriptionForTitle = true;
                dlg.Description = _EXTRACT_TO_DLG_TITLE;
                switch (dlg.ShowDialog())
                {
                    case DialogResult.Yes:
                    case DialogResult.OK:
                        ExtractSelectedEntriesTo(dlg.SelectedPath);
                        break;
                    case DialogResult.Abort:
                    case DialogResult.Cancel:
                    case DialogResult.Ignore:
                    case DialogResult.No:
                    case DialogResult.None:
                    case DialogResult.Retry:
                    default:
                        break;
                }
            }
        }

        private void InitializeEntryContextMenu()
        { 
            ToolStripMenuItem tsRemove = new ToolStripMenuItem("Remove", null);
            tsRemove.Click += tsRemove_Click;
            tsRemove.Enabled = false;

            ToolStripMenuItem tsExtract = new ToolStripMenuItem("Extract", null);
            tsExtract.Click += tsExtract_Click;

            try
            {
                tsRemove.Image = Image.FromFile(REL_PATH_IMAGE_REMOVE);
                tsExtract.Image = Image.FromFile(REL_PATH_IMAGE_EXTRACT);
            }
            catch (Exception) { }

            _entryContextMenu.Items.Add(tsExtract);
            _entryContextMenu.Items.Add(tsRemove);
        }

        private void InitializeGridView()
        {
            gvBrowser.BackgroundColor = Color.White;
            gvBrowser.ShowCellErrors = true;
            gvBrowser.AutoGenerateColumns = false;
            gvBrowser.CellBorderStyle = DataGridViewCellBorderStyle.None;
            gvBrowser.ColumnHeadersVisible = false;

            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.BackColor = Color.FromArgb(30, 30, 30);
            style.ForeColor = Color.FromArgb(43, 145, 175);
            

            DataGridViewTextBoxColumn colIndex = new DataGridViewTextBoxColumn();
            colIndex.HeaderText = "i";
            colIndex.Name = "colIndex";
            colIndex.DefaultCellStyle = style;
            
            colIndex.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
            gvBrowser.Columns.Add(colIndex);

            DataGridViewTextBoxColumn colName = new DataGridViewTextBoxColumn();
            colName.HeaderText = "Fullname";
            colName.Name = "colFullname";
            colName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            gvBrowser.Columns.Add(colName);

            DataGridViewTextBoxColumn colDesc = new DataGridViewTextBoxColumn();
            colDesc.HeaderText = "Desc";
            colDesc.Name = "colDesc";
            colDesc.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            gvBrowser.Columns.Add(colDesc);

            DataGridViewTextBoxColumn colType = new DataGridViewTextBoxColumn();
            colType.HeaderText = "Type";
            colType.Name = "colType";
            colType.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;

            gvBrowser.Columns.Add(colType);
            gvBrowser.CellContextMenuStripNeeded += gvBrowser_CellContextMenuStripNeeded;
        }

        private void UpdateEntries()
        {
            int totalRow = 0;
            gvBrowser.Rows.Clear();
            if (_mixController.CurrentPackage != null)
            {
                //gvBrowser.DataSource = _mixController.CurrentFolder.Entries;
                var entries = _mixController.CurrentPackage.Entries.Values.OrderBy(a => a.Type ).ThenBy(c => c.Offset);
                _cache = new List<IMixEntry>();
                _results.Clear();
                foreach (var entry in entries)
                {
                    _cache.Add(entry);
                }
                // TODO
                gvBrowser.RowCount = totalRow = _cache.Count;
            }

            lblTotalRows.Text = totalRow.ToString();
            lblHiddenRows.Text = "0";
        }

        private void UpdatePath()
        {
            if (_mixController.CurrentPackage != null)
                pCtrl.CurrentPath = _mixController.CurrentPackage.GetPath();
            else
                pCtrl.CurrentPath = "";
        }

        private void UpdateState(bool toggleBrowser)
        {
            UpdateEntries();
            UpdatePath();
            if(toggleBrowser)
                ToggleBrowser();
        }

        private void OpenRoot(string mixFilename)
        {
            Task t = _mixController.OpenRootAsync(mixFilename);
            t.ContinueWith((t1) => OnFolderReady());
        }

        private void OpenSubFolderAsync()
        {
            IMixEntry fE = (IMixEntry)gvBrowser.Rows[gvBrowser.CurrentCell.RowIndex].Tag;
            if (fE is IMixPackage)
            {
                ToggleBrowser();
                Task t = _mixController.OpenSubFolderAsync((IMixPackage)fE);
                t.ContinueWith((t1) => OnFolderReady());
            }
        }

        private void OnFolderReady()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(()=> {
                    UpdateState(true);
                    gvBrowser.Focus();
                }));
            }
        }

        private void OpenFileWithDialog()
        {
            new Thread(() => DialogHelper.FocusFileDialog(_OPEN_FILE_DLG_TITLE)).Start();
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.ValidateNames = true;
                dlg.Title = _OPEN_FILE_DLG_TITLE;
                dlg.AddExtension = true;
                dlg.AutoUpgradeEnabled = true;
                dlg.CheckFileExists = true;
                dlg.DefaultExt =  _OPEN_FILE_DLG_EXT;
                dlg.Filter = _OPEN_FILE_DLG_FILTER;
                dlg.Multiselect = false;

                switch (dlg.ShowDialog())
                {
                    case DialogResult.Yes:
                    case DialogResult.OK:
                        _mixController.OpenRoot(dlg.FileName);
                        UpdateState(true);
                        break;
                    case DialogResult.Abort:
                    case DialogResult.Cancel:
                    case DialogResult.Ignore:
                    case DialogResult.No:
                    case DialogResult.None:
                    default:
                        break;
                }
            }
            gvBrowser.Focus();
        }

        private void ToggleBrowser()
        {
            bool enabled = _mixController.Root != null;

            pCtrl.Enabled = enabled;
            tbSearch.Enabled = enabled;
            gvBrowser.Enabled = enabled;
            btnBack.Enabled = enabled;

            Color colColor;
            if (gvBrowser.Enabled)
                colColor = Color.White;
            else
                colColor = SystemColors.Control;
            foreach (DataGridViewColumn c in gvBrowser.Columns)
            {
                c.DefaultCellStyle.BackColor = colColor;
            }
        }

        private void gvBrowser_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            string fF = files.FirstOrDefault();
            OpenRoot(fF);
        }

        private void gvBrowser_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            _mixController.UpOneLevel();
            UpdateState(false);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileWithDialog();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            _mixController.CloseRoot();
            UpdateState(true);
        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            Misc.Util.Animate(btnOptions, Misc.Util.Effect.Slide, 150, 180);
        }

        private void btnToggleConsole_Click(object sender, EventArgs e)
        {
            if (_logBox == null)
                CreateLogBox();
            else
            {
                _logBox.Visible = !_logBox.Visible;
            }
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            _results.Clear();
            gvBrowser.RowCount = 0;
            foreach (var entry in _cache)
            {
                if (entry.SafeName.StartsWith(tbSearch.Text))
                {

                    _results.Add(entry);
                }
            }
            gvBrowser.RowCount = _results.Count;
            lblHiddenRows.Text = (_cache.Count - _results.Count).ToString();
        }

        private void tbSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
                gvBrowser.Focus();
            else
                if (e.KeyCode == Keys.Escape)
                    tbSearch.Clear();
        }

        private void gvBrowser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                OpenSubFolderAsync();
            }
        }

        private void gvBrowser_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            IMixEntry entry = _results.Count > 0 ? _results[e.RowIndex] : _cache[e.RowIndex];
            switch (e.ColumnIndex)
            {
                case 0:
                    e.Value = e.RowIndex.ToString("D4") + "   ";
                    gvBrowser.Rows[e.RowIndex].Tag = entry;
                    if (!entry.NameResolved) 
                        gvBrowser.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255,252,0);
                    break;
                case 1:
                    e.Value = entry.FullName;
                    break;
                case 2:
                    e.Value = entry.Description;
                    break;
                case 3:
                    e.Value = entry.Type;
                    break;
                default:
                    break;
            }
        }

        private void gvBrowser_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //gvBrowser.Rows[e.RowIndex].ReadOnly = true;
        }

        private void gvBrowser_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e){}

        private void gvBrowser_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                OpenSubFolderAsync();
            }
        }

        private void FrmMixManager_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.F))
            {
                tbSearch.Focus();
                tbSearch.SelectAll();
                e.SuppressKeyPress = true;
            }
            if (e.KeyData == (Keys.Control | Keys.O))
            {
                OpenFileWithDialog();
            }
        }

        private void gvBrowser_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            bool needContextMenu = false;
            if (e.RowIndex > -1)
            {
                if (gvBrowser.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow r in gvBrowser.SelectedRows)
                    {
                        if (r.Index == e.RowIndex)
                        {
                            needContextMenu = true;
                        }
                    }
                    if (!needContextMenu)
                    {
                        gvBrowser.ClearSelection();
                        gvBrowser.Rows[e.RowIndex].Selected = true;
                        needContextMenu = true;
                    }
                }
            }
            if(needContextMenu)
                e.ContextMenuStrip = _entryContextMenu;
        }
    }
}
