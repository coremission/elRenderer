using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
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
        private readonly Color BackgroundColor = Color.Black;
        private void InitForm()
        {
            this.ClientSize = new System.Drawing.Size(WIDTH, HEIGHT);
            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen; 
            this.Name = "MainForm";
            this.Text = "el Renderer";
            this.BackColor = BackgroundColor;
        }

        Bitmap screen;
        private string appPath;

        public MainForm()
        {
            appPath = Application.ExecutablePath;
            appPath = appPath.Substring(0, appPath.LastIndexOf('\\') + 1);

            InitForm();
            screen = new Bitmap(WIDTH, HEIGHT);
            FillScreen(BackgroundColor);

            Color color = Color.MediumAquamarine;

            Float3[] t0 = new [] {new Float3(10, 70, 0), new Float3(50, 160, 0), new Float3(70, 80, 0)};
            Float3[] t1 = new [] {new Float3(180, 50, 0), new Float3(150, 1, 0), new Float3(70, 180, 0)};
            Float3[] t2 = new [] {new Float3(180, 150, 0), new Float3(120, 160, 0), new Float3(130, 180, 0)};


            screen.elDrawTriangle(t0[0], t0[1], t0[2], Color.Red);
            screen.elDrawTriangle(t1[0], t1[1], t1[2], Color.White);
            screen.elDrawTriangle(t2[0], t2[1], t2[2], Color.GreenYellow);

            return;
            Mesh mesh = WaveObjHelper.ReadMeshFromFile(appPath + "african_head.obj");

            float scaleFactor = 300;
            Float3x3 S = Float3x3.identity*scaleFactor;
            Float3x3 R = Float3x3.getRotationMatrix(-30, 0, 0);

            for (int i = 0; i < mesh.Vertices.Count; i++)
            {
                Float3 scaled = mesh.Vertices[i].mul(S);
                scaled = scaled.mul(R);

                mesh.Vertices[i] = new Float3(scaled.x + WIDTH / 2, scaled.y + HEIGHT / 2, scaled.z);

            }
            
            for (int i = 0; i < mesh.Vertices.Count; i++)
            {
                Float3 v = mesh.Vertices[i];
                screen.elDrawPoint(v.x, v.y, color);
            }

            int c = mesh.Triangles.Count;
            
            for (int i = 0; i < c; i++)
            {
                Triangle t = mesh.Triangles[i];

                Float3 v1 = mesh.Vertices[t[0] - 1];
                Float3 v2 = mesh.Vertices[t[1] - 1];
                Float3 v3 = mesh.Vertices[t[2] - 1];

                screen.elDrawLine(v1.x, v1.y, v2.x, v2.y, color);
                screen.elDrawLine(v2.x, v2.y, v3.x, v3.y, color);
                screen.elDrawLine(v3.x, v3.y, v1.x, v1.y, color);
            }
        }

        private void FillScreen(Color color)
        {
            for (int i = 0; i < WIDTH; i++)
                for(int j = 0; j < HEIGHT; j++)
                    screen.SetPixel(i, j, color);
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

            if (e.KeyCode == Keys.S)
                this.screen.Save(appPath + "screenshot.png", ImageFormat.Png);
        }
    }
}
