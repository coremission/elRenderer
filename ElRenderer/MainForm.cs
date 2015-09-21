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
        private Float3 lightDirection = new Float3(0, -1, 0).normalize();
        private Color getRandomColor()
        {
            Random randomGen = new Random();
            KnownColor[] names = (KnownColor[])Enum.GetValues(typeof(KnownColor));
            KnownColor randomColorName = names[randomGen.Next(names.Length)];
            Color randomColor = Color.FromKnownColor(randomColorName);
            return randomColor;
        }

        public MainForm()
        {
            appPath = Application.ExecutablePath;
            appPath = appPath.Substring(0, appPath.LastIndexOf('\\') + 1);

            InitForm();
            screen = new Bitmap(WIDTH, HEIGHT);
            FillScreen(BackgroundColor);

            Color color = Color.MediumAquamarine;

            
            //Float3[] t0 = new [] {new Float3(10, 70, 0), new Float3(50, 160, 0), new Float3(70, 80, 0)};
            //Float3[] t1 = new [] {new Float3(180, 50, 0), new Float3(150, 1, 0), new Float3(70, 180, 0)};
            //Float3[] t2 = new [] {new Float3(180, 150, 0), new Float3(120, 160, 0), new Float3(130, 180, 0)};

            //screen.elDrawTriangle(t0[0], t0[1], t0[2], Color.Red);
            //screen.elDrawTriangle(t1[0], t1[1], t1[2], Color.White);
            //screen.elDrawTriangle(t2[0], t2[1], t2[2], Color.GreenYellow);

            Mesh mesh = WaveObjHelper.ReadMeshFromFile(appPath + "african_head.obj");

            float scaleFactor = 300;
            Float3x3 S = Float3x3.identity*scaleFactor;
            Float3x3 R = Float3x3.getRotationMatrix(0, 0, 0);

            for (int i = 0; i < mesh.Vertices.Count; i++)
            {
                Float3 scaled = mesh.Vertices[i].mul(S);
                scaled = scaled.mul(R);

                mesh.Vertices[i] = new Float3(scaled.x + WIDTH / 2, scaled.y + HEIGHT / 2, scaled.z);

            }
            
            int trianglesCount = mesh.Triangles.Count;
            
            for (int i = 0; i < trianglesCount; i++)
            {
                Triangle t = mesh.Triangles[i];

                Float3 v1 = mesh.Vertices[t[0] - 1];
                Float3 v2 = mesh.Vertices[t[1] - 1];
                Float3 v3 = mesh.Vertices[t[2] - 1];
                Float3 normal = (v3 - v1).cross(v2 - v1).normalize();

                int cc = (int)(255 * normal.dot(lightDirection));

                // TODO: it is like backface culling
                if (cc < 0)
                    continue;

                Color c = Color.FromArgb(cc, cc, cc);

                screen.elDrawTriangle(v1, v2, v3, c);
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
