using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinCtrls
{
    public partial class PathCtrl : UserControl
    {
        private List<Button> _btns;
        private Point _btnOrigin;
        private string _cPath;
        public string CurrentPath {
            get { return _cPath; }
            set {
                if (value != null && _cPath != value)
                {
                    _cPath = value;
                    UpdateState();
                }
            }
        }

        public PathCtrl()
        {
            InitializeComponent();
            _btns = new List<Button>();
            _btnOrigin = new Point(btnOrigin.Location.X, btnOrigin.Location.Y);
            this.Controls.Remove(btnOrigin);
            btnOrigin.Dispose();
            _cPath = string.Empty;
        }


        private void UpdateState()
        {
            UpdateButtons();
            tbPath.Text = _cPath;
        }

        private void UpdateButtons()
        {
            ClearButtons();
            string[] dirs = _cPath.Split('\\');
            Point nLocation = new Point(_btnOrigin.X, _btnOrigin.Y);
            for (int i = 0; i < dirs.Length - 1; i++)
            {
                Button b = new Button();
                b.AutoSize = true;
                b.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
                b.Text = dirs[i];
                b.Enabled = false;
                _btns.Add(b);
                Controls.Add(b);

                b.Location = new Point(nLocation.X, nLocation.Y);
                nLocation = new Point(b.Location.X + b.Width, b.Location.Y);
            }
        }

        private void ClearButtons()
        {
            for (int i = 0; i < _btns.Count; i++)
            {
                Button b = _btns[i];
                _btns.RemoveAt(i);
                this.Controls.Remove(b);
                b.Dispose();
            }
        }
    }
}
