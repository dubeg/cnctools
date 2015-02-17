namespace WinCtrls
{
    partial class PathCtrl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbPath = new System.Windows.Forms.TextBox();
            this.btnOrigin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbPath
            // 
            this.tbPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPath.BackColor = System.Drawing.SystemColors.Control;
            this.tbPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbPath.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPath.Location = new System.Drawing.Point(3, 5);
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(232, 15);
            this.tbPath.TabIndex = 0;
            this.tbPath.Text = "Path";
            // 
            // btnOrigin
            // 
            this.btnOrigin.Location = new System.Drawing.Point(3, 26);
            this.btnOrigin.Name = "btnOrigin";
            this.btnOrigin.Size = new System.Drawing.Size(75, 23);
            this.btnOrigin.TabIndex = 1;
            this.btnOrigin.Text = "button1";
            this.btnOrigin.UseVisualStyleBackColor = true;
            // 
            // PathCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnOrigin);
            this.Controls.Add(this.tbPath);
            this.Name = "PathCtrl";
            this.Size = new System.Drawing.Size(238, 52);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.Button btnOrigin;
    }
}
