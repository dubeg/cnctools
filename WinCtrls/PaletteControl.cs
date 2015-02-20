using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinCtrls
{
    public partial class PaletteControl : UserControl
    {
        private Color[] _colors;
        private string _paletteName;

        private int _selectedIndex;
        private int _rows = 32;
        private int _cols =  8;
        private bool _paletteLoaded;

        /// <summary>
        /// Ctor
        /// </summary>
        public PaletteControl()
        {
            InitializeComponent();
            _selectedIndex = 0;
        }


        public void SetTitle(string title)
        {
            _paletteName = title;
            tbName.Text = _paletteName;
        }


        public void SetColors(Color[] colors)
        {
            _colors = (Color[])colors.Clone();
            DrawPalette();
            _paletteLoaded = true;
        }


        private void DrawPalette()
        {
            Graphics g;
            Bitmap bmp;
            int rectWidth, rectHeight;
            SolidBrush b = new SolidBrush(Color.White);
            Rectangle rect;

            bmp = new Bitmap(pbColors.Width, pbColors.Height);
            rectWidth = pbColors.Width / _cols;
            rectHeight = pbColors.Height / _rows;
            rect = new Rectangle(0, 0, rectWidth, rectHeight);
            g = Graphics.FromImage(bmp);

            for (int x = 0; x < _cols; x++)
            {
                for (int y = 0; y < _rows; ++y)
                {
                    b.Color = _colors[x * _rows + y];
                    rect.Location = new Point(x * rectWidth, y * rectHeight);
                    g.FillRectangle(b, rect);
                }
            }

            pbColors.Image = bmp;
            b.Dispose();
            g.Dispose();
        }


        private Point ConvertMousePointToPalettePoint(Point p)
        {
            
            int rectWidth, rectHeight, x,y;
            rectWidth = pbColors.Width / (_cols);
            rectHeight = pbColors.Height / (_rows);
            x = (p.X / rectWidth);
            y = (p.Y / rectHeight);
            return new Point(x,y);   
        }
        private int GetIndexFromMousePoint(Point p)
        {
            Point palPoint = ConvertMousePointToPalettePoint(p);
            return palPoint.X * _rows + palPoint.Y;
        }
        private void SelectColor(int colorIndex)
        {
            _selectedIndex = colorIndex;

            Bitmap bmp = new Bitmap(pbSelectedColor.Width, pbSelectedColor.Height);
            Graphics g = Graphics.FromImage(bmp);
            SolidBrush b = new SolidBrush( _colors[_selectedIndex] );
            g.FillRectangle(b, new Rectangle(0, 0, bmp.Width, bmp.Height));
            pbSelectedColor.Image = bmp;

            g.Dispose();
            b.Dispose();
        }
        
        private void DrawPaletteColorHover(Point p)
        {
            Graphics g = Graphics.FromImage(pbColors.Image);
        }



        /// <summary>
        /// On mouse up, Select the color
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbColors_MouseUp(object sender, MouseEventArgs e)
        {
            if (_paletteLoaded)
            {
                int index;
                index = GetIndexFromMousePoint(e.Location);
                SelectColor(index);
            }
        }


        /// <summary>
        /// Mouse:Move
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbColors_MouseMove(object sender, MouseEventArgs e)
        {
            if (_paletteLoaded)
            {

                lblIndex.Text = GetIndexFromMousePoint(e.Location).ToString();

                Point p = ConvertMousePointToPalettePoint(e.Location);
                DrawPaletteColorHover(p);
            }
        }

        private void PaletteControl_Resize(object sender, System.EventArgs e)
        {
            pbColors.Location = new Point(pbColors.Location.X, pbColors.Location.Y);

            //int width = this.Width - (pbColors.Location.X * 2);
            //int height = this.Height - (pbColors.Location.Y * 2);

            //pbColors.Width = width - (width >> 3);
            //pbColors.Height = height - (height >> 5);
        }

        private void PaletteControl_Load(object sender, System.EventArgs e)
        {
            
        }

        private void pbColors_Resize(object sender, System.EventArgs e)
        {
        }

        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);
            int width = this.Width - (pbColors.Location.X * 2);
            int height = this.Height - (pbColors.Location.Y * 2);

            pbColors.Width = width - (0x07 & width);
            pbColors.Height = height - (0x0F & height);
        }
        
    }
}
