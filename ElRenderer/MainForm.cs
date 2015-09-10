using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using ElRenderer.Service;
using ElRenderer.Model;

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
            this.BackColor = Color.White;
        }

        Bitmap screen;

        public MainForm()
        {
            string appPath = Application.ExecutablePath;

            Mesh mesh = WaveObjHelper.ReadMeshFromFile(appPath.Substring(0, appPath.LastIndexOf('\\') + 1) + "african_head.obj");


            InitForm();
            screen = new Bitmap(WIDTH, HEIGHT);


            for (int i = 0; i < mesh.Vertices.Count; i++)
            {
                Vector3 v = mesh.Vertices[i];
                v.x *= 300;
                v.y *= 300;
                v.x += (WIDTH / 2);
                v.y += (HEIGHT / 2);

                screen.elDrawPoint((int)v.x, (int)v.y, Color.Black);
            }
            // check point
            //screen.elDrawLine(77, 77, 77, 77, Color.Aquamarine);

            // check down left to right
            //screen.elDrawLine(50, 50, 200, 100, Color.Yellow);

            //screen.elDrawLine(50, 250, 160, 350, Color.Beige);
            //screen.elDrawLine(540, 50, 50, 50, Color.Blue);
            
            //screen.elDrawLine(50, 60, 540, 60, Color.Blue);

            //screen.elDrawLine(60, 60, 160, 760, Color.Blue);

            //screen.elDrawLine(50, 60, 300, 100, Color.Blue);
            //screen.elDrawLine(150, 165, 300, 200, Color.GreenYellow);
            //screen.elDrawLine(140, 40, 150, 300, Color.Red);

            //screen.elDrawLine(500, 20, 500, 3300, Color.Yellow);
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
