using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MixManager
{
    public partial class FrmDllError : Form
    {
        ReadOnlyTextBox tbDlls2;

        public FrmDllError()
        {
            InitializeComponent();
            tbDlls2 = new ReadOnlyTextBox();
            tbDlls2.BackColor = tbDlls.BackColor;
            tbDlls2.ForeColor = tbDlls.ForeColor;
            tbDlls2.Multiline = tbDlls.Multiline;
            tbDlls2.Height = tbDlls.Height;
            tbDlls2.Width = tbDlls.Width;
            tbDlls2.Location = tbDlls.Location;
            tbDlls2.BorderStyle = tbDlls.BorderStyle;
            tbDlls2.TextAlign = tbDlls.TextAlign;

            Controls.Remove(tbDlls);
            tbDlls.Dispose();
            panel1.Controls.Add(tbDlls2);
        }

        public void ShowMissingDlls(List<MixDllInfo> dlls)
        {
            string[] lines = new string[dlls.Count];
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = dlls[i].Name;
            }
            tbDlls2.Lines = lines;
            tbDlls2.SelectionStart = tbDlls2.GetFirstCharIndexFromLine(tbDlls2.Lines.Length - 1);
            tbDlls2.SelectionLength = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    public class ReadOnlyTextBox : TextBox
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);

        public ReadOnlyTextBox()
        {
            this.ReadOnly = true;
            this.GotFocus += TextBoxGotFocus;
            this.Cursor = Cursors.Arrow; // mouse cursor like in other controls
        }

        private void TextBoxGotFocus(object sender, EventArgs args)
        {
            HideCaret(this.Handle);
        }
    }
}
