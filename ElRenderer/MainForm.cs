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
            
            screen.elDrawLine(450, 50, 200, 100, Color.Yellow);
            
            screen.elDrawLine(200, 140, 200, 340, Color.Tomato);
            screen.elDrawLine(50, 60, 300, 100, Color.Blue);
            screen.elDrawLine(150, 165, 300, 200, Color.GreenYellow);
            screen.elDrawLine(40, 40, 150, 500, Color.Red);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawImage(screen, 0, 0);
        }
    }
}
