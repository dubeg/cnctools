﻿using Log4NetEx;
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
        private Controller _controller;
        private FrmLogBox _logBox;
        private LogWatcher _logWatcher;
        private ShpModel _currentShp;
        // Methods
        //--------
        public FrmViewer()
        {
            InitializeComponent();
            _controller = new Controller();
        }

        private void FrmViewer_Load(object sender, EventArgs e)
        {
            // Log console
            _logWatcher = new LogWatcher(_MEMORY_APPENDER_NAME);
            InitLogBox();

            // Others
            lbPalettes.DataSource = _controller.PalettesManager.Palettes;
            lbShps.DataSource = _controller.ShpsManager.Shps;

            if (_controller.PalettesManager.SelectedPalette != null)
            {
                lbPalettes.SelectedItem = _controller.PalettesManager.SelectedPalette;
                SetupPalette();
            }

            if (_controller.ShpsManager.SelectedShp != null)
            {
                _currentShp = _controller.ShpsManager.SelectedShp;
                lbShps.SelectedItem = _currentShp;
                DrawFrame();
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

        private void SetupPalette()
        {
            palCtrl.SetTitle(_controller.PalettesManager.SelectedPalette.Name);
            palCtrl.SetColors(_controller.PalettesManager.SelectedPalette.Colors);
        }

        public void DrawFrame()
        {

            Bitmap bmp;
            ShpModel shp = _controller.ShpsManager.SelectedShp;
            Setbitmap(shp, out bmp);

            // Conditional colors
            if (_controller.PalettesManager.SelectedPalette != null)
            {
                Color[] colors = _controller.PalettesManager.SelectedPalette.Colors;
                ColorPalette pal = bmp.Palette;
                SetColors(pal, colors);
                bmp.Palette = pal;
            }

            if (bmp != null)
                pbFrame.Image = bmp;
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

        private void button4_Click(object sender, EventArgs e)
        {
            _currentShp.SelectNextFrame();
            DrawFrame();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            _currentShp.SelectPrecedingFrame();
            DrawFrame();
        }

        private void button3_Click(object sender, EventArgs e)
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
            dlg.FileName = Path.GetFileName(_currentShp.Filename);
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

        private void button1_Click(object sender, EventArgs e)
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
    }
}
