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
            this.lbPalettes = new System.Windows.Forms.ListBox();
            this.lbShps = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.tbIndex = new System.Windows.Forms.TextBox();
            this.gbV2 = new System.Windows.Forms.GroupBox();
            this.tbZero = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gbV1 = new System.Windows.Forms.GroupBox();
            this.tbZeroV1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbFilesizeV1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbC = new System.Windows.Forms.TextBox();
            this.tbB = new System.Windows.Forms.TextBox();
            this.tbA = new System.Windows.Forms.TextBox();
            this.lblC = new System.Windows.Forms.Label();
            this.lblB = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblSlashCount = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.gbHeader = new System.Windows.Forms.GroupBox();
            this.tbHeaderHeight = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbHeaderWidth = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbHeaderFrameCount = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.palCtrl = new WinCtrls.PaletteControl();
            this.pnlFrame = new System.Windows.Forms.Panel();
            this.pbFrame = new WinCtrls.PictureBoxIndexed();
            this.gbFrameV1 = new System.Windows.Forms.GroupBox();
            this.tbFrameFormatV1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.gbV2.SuspendLayout();
            this.gbV1.SuspendLayout();
            this.gbHeader.SuspendLayout();
            this.pnlFrame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFrame)).BeginInit();
            this.gbFrameV1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbPalettes
            // 
            this.lbPalettes.FormattingEnabled = true;
            this.lbPalettes.Location = new System.Drawing.Point(9, 407);
            this.lbPalettes.Name = "lbPalettes";
            this.lbPalettes.Size = new System.Drawing.Size(287, 95);
            this.lbPalettes.TabIndex = 2;
            this.lbPalettes.SelectedIndexChanged += new System.EventHandler(this.lbPalettes_SelectedIndexChanged);
            // 
            // lbShps
            // 
            this.lbShps.FormattingEnabled = true;
            this.lbShps.Location = new System.Drawing.Point(9, 508);
            this.lbShps.Name = "lbShps";
            this.lbShps.Size = new System.Drawing.Size(287, 95);
            this.lbShps.TabIndex = 6;
            this.lbShps.SelectedIndexChanged += new System.EventHandler(this.lbShps_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(746, 533);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Open";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.button2.Enabled = false;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(746, 613);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "Close";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(746, 562);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 9;
            this.button3.Text = "Save";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(524, 293);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(63, 23);
            this.button4.TabIndex = 10;
            this.button4.Text = "->";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.btnNextFrame_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(305, 294);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(63, 23);
            this.button5.TabIndex = 11;
            this.button5.Text = "<-";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.btnPrecedingFrame_Click);
            // 
            // tbIndex
            // 
            this.tbIndex.Location = new System.Drawing.Point(394, 296);
            this.tbIndex.Name = "tbIndex";
            this.tbIndex.Size = new System.Drawing.Size(52, 20);
            this.tbIndex.TabIndex = 12;
            // 
            // gbV2
            // 
            this.gbV2.Controls.Add(this.tbZero);
            this.gbV2.Controls.Add(this.label2);
            this.gbV2.Location = new System.Drawing.Point(305, 508);
            this.gbV2.Name = "gbV2";
            this.gbV2.Size = new System.Drawing.Size(360, 88);
            this.gbV2.TabIndex = 13;
            this.gbV2.TabStop = false;
            this.gbV2.Text = "Shp V2";
            // 
            // tbZero
            // 
            this.tbZero.Location = new System.Drawing.Point(70, 22);
            this.tbZero.Name = "tbZero";
            this.tbZero.ReadOnly = true;
            this.tbZero.Size = new System.Drawing.Size(100, 20);
            this.tbZero.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "A ( zero )";
            // 
            // gbV1
            // 
            this.gbV1.Controls.Add(this.tbZeroV1);
            this.gbV1.Controls.Add(this.label5);
            this.gbV1.Controls.Add(this.tbFilesizeV1);
            this.gbV1.Controls.Add(this.label3);
            this.gbV1.Controls.Add(this.tbC);
            this.gbV1.Controls.Add(this.tbB);
            this.gbV1.Controls.Add(this.tbA);
            this.gbV1.Controls.Add(this.lblC);
            this.gbV1.Controls.Add(this.lblB);
            this.gbV1.Controls.Add(this.label1);
            this.gbV1.Location = new System.Drawing.Point(305, 397);
            this.gbV1.Name = "gbV1";
            this.gbV1.Size = new System.Drawing.Size(360, 100);
            this.gbV1.TabIndex = 14;
            this.gbV1.TabStop = false;
            this.gbV1.Text = "Shp V1";
            // 
            // tbZeroV1
            // 
            this.tbZeroV1.Location = new System.Drawing.Point(254, 47);
            this.tbZeroV1.Name = "tbZeroV1";
            this.tbZeroV1.ReadOnly = true;
            this.tbZeroV1.Size = new System.Drawing.Size(100, 20);
            this.tbZeroV1.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(199, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Zero";
            // 
            // tbFilesizeV1
            // 
            this.tbFilesizeV1.Location = new System.Drawing.Point(254, 24);
            this.tbFilesizeV1.Name = "tbFilesizeV1";
            this.tbFilesizeV1.ReadOnly = true;
            this.tbFilesizeV1.Size = new System.Drawing.Size(100, 20);
            this.tbFilesizeV1.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(199, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Filesize";
            // 
            // tbC
            // 
            this.tbC.Location = new System.Drawing.Point(70, 70);
            this.tbC.Name = "tbC";
            this.tbC.ReadOnly = true;
            this.tbC.Size = new System.Drawing.Size(100, 20);
            this.tbC.TabIndex = 5;
            // 
            // tbB
            // 
            this.tbB.Location = new System.Drawing.Point(70, 47);
            this.tbB.Name = "tbB";
            this.tbB.ReadOnly = true;
            this.tbB.Size = new System.Drawing.Size(100, 20);
            this.tbB.TabIndex = 4;
            // 
            // tbA
            // 
            this.tbA.Location = new System.Drawing.Point(70, 24);
            this.tbA.Name = "tbA";
            this.tbA.ReadOnly = true;
            this.tbA.Size = new System.Drawing.Size(100, 20);
            this.tbA.TabIndex = 3;
            // 
            // lblC
            // 
            this.lblC.AutoSize = true;
            this.lblC.Location = new System.Drawing.Point(15, 73);
            this.lblC.Name = "lblC";
            this.lblC.Size = new System.Drawing.Size(52, 13);
            this.lblC.TabIndex = 2;
            this.lblC.Text = "C ( delta )";
            // 
            // lblB
            // 
            this.lblB.AutoSize = true;
            this.lblB.Location = new System.Drawing.Point(15, 50);
            this.lblB.Name = "lblB";
            this.lblB.Size = new System.Drawing.Size(34, 13);
            this.lblB.TabIndex = 1;
            this.lblB.Text = "B ( y )";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "A ( x )";
            // 
            // lblSlashCount
            // 
            this.lblSlashCount.AutoSize = true;
            this.lblSlashCount.Location = new System.Drawing.Point(452, 299);
            this.lblSlashCount.Name = "lblSlashCount";
            this.lblSlashCount.Size = new System.Drawing.Size(12, 13);
            this.lblSlashCount.TabIndex = 15;
            this.lblSlashCount.Text = "/";
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(470, 299);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(13, 13);
            this.lblCount.TabIndex = 16;
            this.lblCount.Text = "0";
            // 
            // gbHeader
            // 
            this.gbHeader.Controls.Add(this.tbHeaderHeight);
            this.gbHeader.Controls.Add(this.label7);
            this.gbHeader.Controls.Add(this.tbHeaderWidth);
            this.gbHeader.Controls.Add(this.label8);
            this.gbHeader.Controls.Add(this.tbHeaderFrameCount);
            this.gbHeader.Controls.Add(this.label9);
            this.gbHeader.Location = new System.Drawing.Point(305, 322);
            this.gbHeader.Name = "gbHeader";
            this.gbHeader.Size = new System.Drawing.Size(360, 69);
            this.gbHeader.TabIndex = 14;
            this.gbHeader.TabStop = false;
            this.gbHeader.Text = "Header";
            // 
            // tbHeaderHeight
            // 
            this.tbHeaderHeight.Location = new System.Drawing.Point(254, 40);
            this.tbHeaderHeight.Name = "tbHeaderHeight";
            this.tbHeaderHeight.ReadOnly = true;
            this.tbHeaderHeight.Size = new System.Drawing.Size(100, 20);
            this.tbHeaderHeight.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(199, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Height";
            // 
            // tbHeaderWidth
            // 
            this.tbHeaderWidth.Location = new System.Drawing.Point(254, 17);
            this.tbHeaderWidth.Name = "tbHeaderWidth";
            this.tbHeaderWidth.ReadOnly = true;
            this.tbHeaderWidth.Size = new System.Drawing.Size(100, 20);
            this.tbHeaderWidth.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(199, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Width";
            // 
            // tbHeaderFrameCount
            // 
            this.tbHeaderFrameCount.Location = new System.Drawing.Point(74, 17);
            this.tbHeaderFrameCount.Name = "tbHeaderFrameCount";
            this.tbHeaderFrameCount.ReadOnly = true;
            this.tbHeaderFrameCount.Size = new System.Drawing.Size(100, 20);
            this.tbHeaderFrameCount.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(19, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Frames";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(12, 618);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(53, 13);
            this.lblMessage.TabIndex = 17;
            this.lblMessage.Text = "Message.";
            // 
            // palCtrl
            // 
            this.palCtrl.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.palCtrl.Location = new System.Drawing.Point(9, 12);
            this.palCtrl.Name = "palCtrl";
            this.palCtrl.Size = new System.Drawing.Size(287, 389);
            this.palCtrl.TabIndex = 1;
            // 
            // pnlFrame
            // 
            this.pnlFrame.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.pnlFrame.Controls.Add(this.pbFrame);
            this.pnlFrame.Location = new System.Drawing.Point(305, 15);
            this.pnlFrame.Name = "pnlFrame";
            this.pnlFrame.Size = new System.Drawing.Size(282, 273);
            this.pnlFrame.TabIndex = 18;
            // 
            // pbFrame
            // 
            this.pbFrame.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pbFrame.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            this.pbFrame.Location = new System.Drawing.Point(3, 3);
            this.pbFrame.Name = "pbFrame";
            this.pbFrame.Size = new System.Drawing.Size(72, 66);
            this.pbFrame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbFrame.TabIndex = 0;
            this.pbFrame.TabStop = false;
            // 
            // gbFrameV1
            // 
            this.gbFrameV1.Controls.Add(this.tbFrameFormatV1);
            this.gbFrameV1.Controls.Add(this.label4);
            this.gbFrameV1.Location = new System.Drawing.Point(605, 15);
            this.gbFrameV1.Name = "gbFrameV1";
            this.gbFrameV1.Size = new System.Drawing.Size(188, 114);
            this.gbFrameV1.TabIndex = 14;
            this.gbFrameV1.TabStop = false;
            this.gbFrameV1.Text = "Frame v1";
            // 
            // tbFrameFormatV1
            // 
            this.tbFrameFormatV1.Location = new System.Drawing.Point(70, 22);
            this.tbFrameFormatV1.Name = "tbFrameFormatV1";
            this.tbFrameFormatV1.ReadOnly = true;
            this.tbFrameFormatV1.Size = new System.Drawing.Size(100, 20);
            this.tbFrameFormatV1.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Format";
            // 
            // FrmViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(833, 649);
            this.Controls.Add(this.gbFrameV1);
            this.Controls.Add(this.pnlFrame);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.gbHeader);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.lblSlashCount);
            this.Controls.Add(this.gbV1);
            this.Controls.Add(this.gbV2);
            this.Controls.Add(this.tbIndex);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lbShps);
            this.Controls.Add(this.lbPalettes);
            this.Controls.Add(this.palCtrl);
            this.Name = "FrmViewer";
            this.Text = "Shp Viewer";
            this.Load += new System.EventHandler(this.FrmViewer_Load);
            this.gbV2.ResumeLayout(false);
            this.gbV2.PerformLayout();
            this.gbV1.ResumeLayout(false);
            this.gbV1.PerformLayout();
            this.gbHeader.ResumeLayout(false);
            this.gbHeader.PerformLayout();
            this.pnlFrame.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbFrame)).EndInit();
            this.gbFrameV1.ResumeLayout(false);
            this.gbFrameV1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private WinCtrls.PaletteControl palCtrl;
        private System.Windows.Forms.ListBox lbPalettes;
        private System.Windows.Forms.ListBox lbShps;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox tbIndex;
        private System.Windows.Forms.GroupBox gbV2;
        private System.Windows.Forms.TextBox tbZero;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gbV1;
        private System.Windows.Forms.TextBox tbC;
        private System.Windows.Forms.TextBox tbB;
        private System.Windows.Forms.TextBox tbA;
        private System.Windows.Forms.Label lblC;
        private System.Windows.Forms.Label lblB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSlashCount;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.TextBox tbZeroV1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbFilesizeV1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gbHeader;
        private System.Windows.Forms.TextBox tbHeaderHeight;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbHeaderWidth;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbHeaderFrameCount;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Panel pnlFrame;
        private WinCtrls.PictureBoxIndexed pbFrame;
        private System.Windows.Forms.GroupBox gbFrameV1;
        private System.Windows.Forms.TextBox tbFrameFormatV1;
        private System.Windows.Forms.Label label4;
    }
}

