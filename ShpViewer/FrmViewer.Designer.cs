namespace ShpApp
{
    partial class FrmViewer
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pbFrame = new System.Windows.Forms.PictureBox();
            this.lblPalList = new System.Windows.Forms.Label();
            this.palCtrl = new WinCtrls.PaletteControl();
            this.lbPalettes = new System.Windows.Forms.ListBox();
            this.lbShps = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbFrame)).BeginInit();
            this.SuspendLayout();
            // 
            // pbFrame
            // 
            this.pbFrame.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.pbFrame.Location = new System.Drawing.Point(12, 12);
            this.pbFrame.Name = "pbFrame";
            this.pbFrame.Size = new System.Drawing.Size(282, 277);
            this.pbFrame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbFrame.TabIndex = 0;
            this.pbFrame.TabStop = false;
            // 
            // lblPalList
            // 
            this.lblPalList.AutoSize = true;
            this.lblPalList.Location = new System.Drawing.Point(593, 21);
            this.lblPalList.Name = "lblPalList";
            this.lblPalList.Size = new System.Drawing.Size(91, 13);
            this.lblPalList.TabIndex = 3;
            this.lblPalList.Text = "Available Palettes";
            // 
            // palCtrl
            // 
            this.palCtrl.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.palCtrl.Location = new System.Drawing.Point(300, 12);
            this.palCtrl.Name = "palCtrl";
            this.palCtrl.Size = new System.Drawing.Size(287, 432);
            this.palCtrl.TabIndex = 1;
            // 
            // lbPalettes
            // 
            this.lbPalettes.FormattingEnabled = true;
            this.lbPalettes.Location = new System.Drawing.Point(593, 37);
            this.lbPalettes.Name = "lbPalettes";
            this.lbPalettes.Size = new System.Drawing.Size(131, 407);
            this.lbPalettes.TabIndex = 2;
            // 
            // lbShps
            // 
            this.lbShps.FormattingEnabled = true;
            this.lbShps.Location = new System.Drawing.Point(12, 323);
            this.lbShps.Name = "lbShps";
            this.lbShps.Size = new System.Drawing.Size(282, 121);
            this.lbShps.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 307);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Available SHPs";
            // 
            // FrmViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 452);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbShps);
            this.Controls.Add(this.lblPalList);
            this.Controls.Add(this.lbPalettes);
            this.Controls.Add(this.palCtrl);
            this.Controls.Add(this.pbFrame);
            this.Name = "FrmViewer";
            this.Text = "Shp Viewer";
            this.Load += new System.EventHandler(this.FrmViewer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbFrame)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbFrame;
        private WinCtrls.PaletteControl palCtrl;
        private System.Windows.Forms.Label lblPalList;
        private System.Windows.Forms.ListBox lbPalettes;
        private System.Windows.Forms.ListBox lbShps;
        private System.Windows.Forms.Label label1;
    }
}

