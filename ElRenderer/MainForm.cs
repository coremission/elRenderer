using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElRenderer
{
    public class MainForm : Form
    {
        private const int WIDTH = 1024;
        private const int HEIGHT = 768;

        private void InitForm()
        {
            this.ClientSize = new System.Drawing.Size(WIDTH, HEIGHT);
            this.Name = "MainForm";
            this.Text = "el Renderer";
            this.BackColor = Color.Black;
        }

        Bitmap screen;

        public MainForm()
        {
            InitForm();
            screen = new Bitmap(WIDTH, HEIGHT);
            
            screen.elDrawLine(50, 50, 200, 200);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawImage(screen, 0, 0);
        }
    }
}
