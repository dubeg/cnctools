namespace WinCtrls
{
    partial class PaletteControl
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
            this.pbColors = new System.Windows.Forms.PictureBox();
            this.lblName = new System.Windows.Forms.Label();
            this.pbSelectedColor = new System.Windows.Forms.PictureBox();
            this.lblSelectedIndex = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lblIndex = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbColors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSelectedColor)).BeginInit();
            this.SuspendLayout();
            // 
            // pbColors
            // 
            this.pbColors.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.pbColors.Location = new System.Drawing.Point(16, 32);
            this.pbColors.Name = "pbColors";
            this.pbColors.Size = new System.Drawing.Size(260, 348);
            this.pbColors.TabIndex = 0;
            this.pbColors.TabStop = false;
            this.pbColors.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbColors_MouseMove);
            this.pbColors.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbColors_MouseUp);
            this.pbColors.Resize += new System.EventHandler(this.pbColors_Resize);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(14, 10);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(72, 13);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Palette  name";
            // 
            // pbSelectedColor
            // 
            this.pbSelectedColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pbSelectedColor.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.pbSelectedColor.Location = new System.Drawing.Point(17, 387);
            this.pbSelectedColor.Name = "pbSelectedColor";
            this.pbSelectedColor.Size = new System.Drawing.Size(59, 13);
            this.pbSelectedColor.TabIndex = 2;
            this.pbSelectedColor.TabStop = false;
            // 
            // lblSelectedIndex
            // 
            this.lblSelectedIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSelectedIndex.AutoSize = true;
            this.lblSelectedIndex.Location = new System.Drawing.Point(82, 387);
            this.lblSelectedIndex.Name = "lblSelectedIndex";
            this.lblSelectedIndex.Size = new System.Drawing.Size(33, 13);
            this.lblSelectedIndex.TabIndex = 3;
            this.lblSelectedIndex.Text = "Index";
            // 
            // tbName
            // 
            this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbName.Location = new System.Drawing.Point(92, 7);
            this.tbName.Name = "tbName";
            this.tbName.ReadOnly = true;
            this.tbName.Size = new System.Drawing.Size(184, 20);
            this.tbName.TabIndex = 4;
            // 
            // lblIndex
            // 
            this.lblIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblIndex.AutoSize = true;
            this.lblIndex.Location = new System.Drawing.Point(243, 387);
            this.lblIndex.Name = "lblIndex";
            this.lblIndex.Size = new System.Drawing.Size(14, 13);
            this.lblIndex.TabIndex = 5;
            this.lblIndex.Text = "X";
            // 
            // PaletteControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.lblIndex);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.lblSelectedIndex);
            this.Controls.Add(this.pbSelectedColor);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.pbColors);
            this.Name = "PaletteControl";
            this.Size = new System.Drawing.Size(297, 409);
            this.Load += new System.EventHandler(this.PaletteControl_Load);
            this.Resize += new System.EventHandler(this.PaletteControl_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pbColors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSelectedColor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbColors;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.PictureBox pbSelectedColor;
        private System.Windows.Forms.Label lblSelectedIndex;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lblIndex;
    }
}
