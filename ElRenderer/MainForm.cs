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

            // check point
            screen.elDrawLine(77, 77, 77, 77, Color.Aquamarine);

            // check down left to right
            screen.elDrawLine(50, 50, 200, 100, Color.Yellow);

            screen.elDrawLine(50, 250, 160, 350, Color.Beige);
            screen.elDrawLine(540, 50, 50, 50, Color.Blue);
            
            screen.elDrawLine(50, 60, 540, 60, Color.Blue);

            screen.elDrawLine(60, 60, 160, 760, Color.Blue);

            screen.elDrawLine(50, 60, 300, 100, Color.Blue);
            screen.elDrawLine(150, 165, 300, 200, Color.GreenYellow);
            screen.elDrawLine(140, 40, 150, 300, Color.Red);

            screen.elDrawLine(500, 20, 500, 3300, Color.Yellow);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawImage(screen, 0, 0);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
    }
}
