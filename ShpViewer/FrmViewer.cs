using Log4NetEx;
using ShpLib.V1;
using ShpLib.V2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using WinCtrls;

namespace ShpApp
{
    public partial class FrmViewer : Form
    {
        // Vars
        //--------
        private const string _MEMORY_APPENDER_NAME = "MemoryAppender";
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private int _zoom;
        private bool _invalidZoom;
        private Controller _controller;
        private FrmLogBox _logBox;
        private LogWatcher _logWatcher;
        private Bitmap _bmp;
        // Methods
        //--------
        public FrmViewer()
        {
            InitializeComponent();
            _zoom = 1;
            _invalidZoom = false;

            tbZoom.Text = _zoom.ToString();
        }

        private void FrmViewer_Load(object sender, EventArgs e)
        {
            // Log console
            _logWatcher = new LogWatcher(_MEMORY_APPENDER_NAME);
            InitLogBox();

            // Others
            _controller = new Controller();
            lbPalettes.DataSource = _controller.PalettesManager.Palettes;
            lbShps.DataSource = _controller.ShpsManager.Shps;

            if (_controller.ShpsManager.SelectedShp != null)
            {
                lbShps.SelectedItem = _controller.ShpsManager.SelectedShp;
                SetupShp(_controller.ShpsManager.SelectedShp);
            }

            if (_controller.PalettesManager.SelectedPalette != null)
            {
                SetupPalette();
            }
        }

        #region LogBox
        private void InitLogBox()
        {
            _logBox = new FrmLogBox(_logWatcher);
            _logBox.Height = 150;
            _logBox.FormClosed += _logBox_FormClosed;
            _logBox.Show(this);
            this.Move += FrmViewer_Move;
            FrmViewer_Move(this, null);
        }

        void FrmViewer_Move(object sender, EventArgs e)
        {
            if (_logBox != null)
            {
                _logBox.Top = this.Bottom;
                _logBox.Width = this.Width;
                _logBox.Left = this.Left;
            }
        }

        void _logBox_FormClosed(object sender, FormClosedEventArgs e)
        {
            _logBox = null;
        }
        #endregion

        private void SetupShp(ShpApp.ShpModel shp)
        {
            if (shp.RawShp is ShpLib.V1.ShpV1)
            {
                gbV1.Enabled = true;
                gbV2.Enabled = false;
                ShpV1 v1 = (ShpV1)shp.RawShp;
                tbA.Text = v1.Unknown1.ToString();
                tbB.Text = v1.Unknown2.ToString();
                tbC.Text = v1.Unknown3.ToString();
                tbFilesizeV1.Text = v1.FileSize.ToString();
                tbZeroV1.Text = v1.Zero.ToString();

                tbHeaderFrameCount.Text = v1.FrameCount.ToString();
                tbHeaderHeight.Text = v1.FrameHeight.ToString();
                tbHeaderWidth.Text = v1.FrameWidth.ToString();
            }
            else
            {
                gbV2.Enabled = true;
                gbV1.Enabled = false;
                ShpV2 v2 = (ShpV2)shp.RawShp;
                tbZero.Text = v2.Unknown1.ToString();

                tbHeaderFrameCount.Text = v2.FrameCount.ToString();
                tbHeaderHeight.Text = v2.FrameHeight.ToString();
                tbHeaderWidth.Text = v2.FrameWidth.ToString();
            }

            lblCount.Text = (shp.Frames.Count - 1).ToString();
            SetupFrame(shp);
        }

        private void SetupFrame(ShpModel shp)
        {
            tbIndex.Text = shp.FrameIndex.ToString();
            if (shp.RawShp is ShpLib.V1.ShpV1)
            {
                ShpV1 v1 = (ShpV1)shp.RawShp;
                tbFrameFormatV1.Text = v1.Frames[shp.FrameIndex].Format.ToString("X2");
            }
            else
            {
                ShpV2 v2 = (ShpV2)shp.RawShp;
            }
            DrawFrame(shp);
            SetFramePalette();
        }

        private void SetupPalette()
        {
            palCtrl.SetTitle(_controller.PalettesManager.SelectedPalette.Name);
            palCtrl.SetColors(_controller.PalettesManager.SelectedPalette.Colors);

            if (_bmp != null)
                SetFramePalette();
        }

        public void SetFramePalette()
        {
            Color[] colors = _controller.PalettesManager.SelectedPalette.Colors;
            ColorPalette pal = _bmp.Palette;
            SetColors(pal, colors);
            _bmp.Palette = pal;
            pbFrame.Invalidate();
        }

        public void DrawFrame(ShpModel shp)
        {
            Setbitmap(shp, out _bmp);

            if (_bmp != null)
            {
                pbFrame.Image = _bmp;
                pbFrame.Width = _bmp.Width * _zoom;
                pbFrame.Height = _bmp.Height * _zoom;
            }
        }

        private void SetColors(ColorPalette pal, Color[] colors)
        {
            for (int i = 0; i < pal.Entries.Length; i++)
            {
                pal.Entries[i] = colors[i];
            }
        }

        private Bitmap Setbitmap(ShpModel shp, out Bitmap bmp)
        {
            int frameWidth = shp.Width;
            int frameHeight = shp.Height;
            byte[] fData = shp.SelectedFrame;


            bmp = new Bitmap(frameWidth, frameHeight, PixelFormat.Format8bppIndexed);
            BitmapData bmpData = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format8bppIndexed
                );
            int offset = bmpData.Stride - bmp.Width;
            unsafe
            {
                byte* pBmp = (byte*)bmpData.Scan0;
                for (int y = 0; y < bmp.Height; y++)
                    for (int x = 0; x < bmp.Width; x++)
                        *pBmp++ = 0;

                pBmp = (byte*)bmpData.Scan0;
                for (int y = 0; y < bmp.Height; y++)
                {
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        if (fData[y * bmp.Width + x] > 0)
                            *pBmp = fData[y * bmp.Width + x];
                        ++pBmp;
                    }
                    pBmp += offset;
                }
            }

            bmp.UnlockBits(bmpData);
            return bmp;
        }

        //---------------------------------------------------------------------

        private void btnNextFrame_Click(object sender, EventArgs e)
        {
            _controller.ShpsManager.SelectedShp.SelectNextFrame();
            SetupFrame(_controller.ShpsManager.SelectedShp);
        }

        private void btnPrecedingFrame_Click(object sender, EventArgs e)
        {
            _controller.ShpsManager.SelectedShp.SelectPrecedingFrame();
            SetupFrame(_controller.ShpsManager.SelectedShp);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string _SAVE_FILE_DLG_TITLE = "Save shp";
            string _SAVE_FILE_DLG_FILTER = "Shp(td)|*.shp|Shp(ts)|*.shp";
            string _SAVE_FILE_DLG_EXT = ".shp";

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.AddExtension = true;
            dlg.DefaultExt = _SAVE_FILE_DLG_EXT;
            dlg.Filter = _SAVE_FILE_DLG_FILTER;
            dlg.Title = _SAVE_FILE_DLG_TITLE;
            dlg.OverwritePrompt = true;
            dlg.FileName = Path.GetFileName(_controller.ShpsManager.SelectedShp.Filename);
            switch (dlg.ShowDialog())
            {
                case DialogResult.OK:
                case DialogResult.Yes:
                    ShpLib.EncodingOptions option = dlg.FilterIndex == 1 ?
                        ShpLib.EncodingOptions.ShpV1 :
                        ShpLib.EncodingOptions.ShpV2;

                    _controller.ShpsManager.SaveCurrentShpTo(dlg.FileName, option);
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

        private void btnOpen_Click(object sender, EventArgs e)
        {
            string _OPEN_FILE_DLG_TITLE = "Open .shp";
            string _OPEN_FILE_DLG_FILTER = "SHP file (*.shp)|*.shp";
            string _OPEN_FILE_DLG_EXT = ".shp";

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.AddExtension = true;
            dlg.DefaultExt = _OPEN_FILE_DLG_EXT;
            dlg.Filter = _OPEN_FILE_DLG_FILTER;
            dlg.Title = _OPEN_FILE_DLG_TITLE;
            switch (dlg.ShowDialog())
            {
                case DialogResult.OK:
                case DialogResult.Yes:
                    _controller.ShpsManager.LoadShp(dlg.FileName);
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

        private void lbPalettes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbPalettes.SelectedIndex >= 0)
            {
                _controller.PalettesManager.SelectPalette(lbPalettes.SelectedIndex);
                SetupPalette();
            }
        }

        private void lbShps_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbShps.SelectedIndex >= 0)
            {
                _controller.ShpsManager.SelectShp(lbShps.SelectedIndex);
                SetupShp(_controller.ShpsManager.SelectedShp);
            }
        }

        private void tbZoom_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int value = Convert.ToInt32(tbZoom.Text);
                if (value > 0 && value < 5)
                {
                    _zoom = value;
                    SetupFrame(_controller.ShpsManager.SelectedShp);
                    SetupPalette();
                    _invalidZoom = false;
                }
                else
                {
                    log.Error("Zoom, values in range: [0,4]");
                    _invalidZoom = true;
                }
            }
            catch (Exception ex)
            {
                log.Error("Zoom, invalid value: " + tbZoom.Text);
                _invalidZoom = true;
            }
        }

        private void tbZoom_Leave(object sender, EventArgs e)
        {
            if (_invalidZoom)
                tbZoom.Text = _zoom.ToString();
        }


    }
}
