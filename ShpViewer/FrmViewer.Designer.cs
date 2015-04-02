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
            this.lbPalettes = new System.Windows.Forms.ListBox();
            this.lbShps = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.palCtrl = new WinCtrls.PaletteControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblB = new System.Windows.Forms.Label();
            this.lblC = new System.Windows.Forms.Label();
            this.tbA = new System.Windows.Forms.TextBox();
            this.tbB = new System.Windows.Forms.TextBox();
            this.tbC = new System.Windows.Forms.TextBox();
            this.tbZero = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbFrame)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbFrame
            // 
            this.pbFrame.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.pbFrame.Location = new System.Drawing.Point(305, 12);
            this.pbFrame.Name = "pbFrame";
            this.pbFrame.Size = new System.Drawing.Size(282, 277);
            this.pbFrame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbFrame.TabIndex = 0;
            this.pbFrame.TabStop = false;
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
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(593, 12);
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
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(593, 92);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "Close";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(593, 41);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 9;
            this.button3.Text = "Save";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(481, 295);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(63, 23);
            this.button4.TabIndex = 10;
            this.button4.Text = "->";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.btnNextFrame_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(354, 295);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(63, 23);
            this.button5.TabIndex = 11;
            this.button5.Text = "<-";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.btnPrecedingFrame_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(423, 297);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(52, 20);
            this.textBox1.TabIndex = 12;
            // 
            // palCtrl
            // 
            this.palCtrl.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.palCtrl.Location = new System.Drawing.Point(9, 12);
            this.palCtrl.Name = "palCtrl";
            this.palCtrl.Size = new System.Drawing.Size(287, 389);
            this.palCtrl.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbZero);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(305, 508);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(360, 57);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Shp V2";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbC);
            this.groupBox2.Controls.Add(this.tbB);
            this.groupBox2.Controls.Add(this.tbA);
            this.groupBox2.Controls.Add(this.lblC);
            this.groupBox2.Controls.Add(this.lblB);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(305, 397);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(360, 100);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Shp V1";
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
            // lblB
            // 
            this.lblB.AutoSize = true;
            this.lblB.Location = new System.Drawing.Point(15, 50);
            this.lblB.Name = "lblB";
            this.lblB.Size = new System.Drawing.Size(34, 13);
            this.lblB.TabIndex = 1;
            this.lblB.Text = "B ( y )";
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
            // tbA
            // 
            this.tbA.Location = new System.Drawing.Point(70, 24);
            this.tbA.Name = "tbA";
            this.tbA.Size = new System.Drawing.Size(100, 20);
            this.tbA.TabIndex = 3;
            // 
            // tbB
            // 
            this.tbB.Location = new System.Drawing.Point(70, 47);
            this.tbB.Name = "tbB";
            this.tbB.Size = new System.Drawing.Size(100, 20);
            this.tbB.TabIndex = 4;
            // 
            // tbC
            // 
            this.tbC.Location = new System.Drawing.Point(70, 70);
            this.tbC.Name = "tbC";
            this.tbC.Size = new System.Drawing.Size(100, 20);
            this.tbC.TabIndex = 5;
            // 
            // tbZero
            // 
            this.tbZero.Location = new System.Drawing.Point(70, 22);
            this.tbZero.Name = "tbZero";
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
            // FrmViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 608);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lbShps);
            this.Controls.Add(this.lbPalettes);
            this.Controls.Add(this.palCtrl);
            this.Controls.Add(this.pbFrame);
            this.Name = "FrmViewer";
            this.Text = "Shp Viewer";
            this.Load += new System.EventHandler(this.FrmViewer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbFrame)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbFrame;
        private WinCtrls.PaletteControl palCtrl;
        private System.Windows.Forms.ListBox lbPalettes;
        private System.Windows.Forms.ListBox lbShps;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbZero;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbC;
        private System.Windows.Forms.TextBox tbB;
        private System.Windows.Forms.TextBox tbA;
        private System.Windows.Forms.Label lblC;
        private System.Windows.Forms.Label lblB;
        private System.Windows.Forms.Label label1;
    }
}

