using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Windows.Forms;
using ElRenderer.Service;
using ElRenderer.Model;
using System.Collections.Generic;

namespace ElRenderer
{
    public class MainForm : Form
    {
        Bitmap screen;
        private string appPath;
        private readonly Color BackgroundColor = Color.Black;
        private Renderer renderer;

        private void InitForm()
        {
            this.ClientSize = new System.Drawing.Size(Defaults.WIDTH, Defaults.HEIGHT);
            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen; 
            this.Name = "MainForm";
            this.Text = "el Renderer";
            this.BackColor = BackgroundColor;
        }
                
        public MainForm()
        {
            appPath = Application.ExecutablePath;
            appPath = appPath.Substring(0, appPath.LastIndexOf('\\') + 1);

            InitForm();
            screen = new Bitmap(Defaults.WIDTH, Defaults.HEIGHT);
            FillScreen(BackgroundColor);
            
            Mesh mesh = WaveObjHelper.ReadMeshFromFile(appPath + "3dModels\\african_head.obj");

            renderer = new Renderer(BackgroundColor, new Float3(0, 1.5f, 0).normalize());

            mesh = new Mesh();

            mesh.Vertices = new List<Float3>(){
                new Float3(1, 0, -1),
                new Float3(1, 0, 1),
                new Float3(1, 2, 1),
                new Float3(1, 2, -1),
                new Float3(-1, 2, -1),
                new Float3(-1, 2, 1),
                new Float3(-1, 0, 1),
                new Float3(-1, 0, -1)
            };
            mesh.Triangles = new List<Triangle>(){
                new Triangle(1, 2, 3), new Triangle(1, 3, 4),
                new Triangle(1, 8, 5), new Triangle(1, 5, 4),
                new Triangle(4, 3, 6), new Triangle(4, 6, 5),                
            };

            renderer.RenderTo(screen, mesh, RenderType.WireframeAboveRegular);
        }

        private void FillScreen(Color color)
        {
            for (int i = 0; i < Defaults.WIDTH; i++)
                for(int j = 0; j < Defaults.HEIGHT; j++)
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
