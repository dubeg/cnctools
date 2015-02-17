using log4net.Appender;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Misc.Log4net
{
    public class TextBoxAppender : AppenderSkeleton
    {
        private TextBox _textBox;
        public string FormName { get; set; }
        public string TextBoxName { get; set; }

        protected override void Append(LoggingEvent loggingEvent)
        {
            if (_textBox == null)
            {
                if (String.IsNullOrEmpty(FormName) ||
                    String.IsNullOrEmpty(TextBoxName))
                    return;

                Form form = Application.OpenForms[FormName];
                if (form == null)
                    return;

                _textBox = (TextBox)form.Controls.Find(TextBoxName,true).FirstOrDefault();
                if (_textBox == null)
                    return;
                _textBox.Clear();
                form.FormClosing += (s, e) => _textBox = null;
            }

            if (_textBox.InvokeRequired)
            {
                _textBox.BeginInvoke(new Action(() => { Append(loggingEvent); }));
                return;
            }
            StringWriter sb = new StringWriter();
            Layout.Format(sb, loggingEvent);
            _textBox.AppendText(sb.ToString());
            sb.Dispose();
        }
    }
}
