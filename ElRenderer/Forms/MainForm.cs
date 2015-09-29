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
        private Mesh mesh;
        private Float3 viewDirection = new Float3(0, 0, 1).normalize();

        private int yAngle = 0;
        private int xAngle = 0;
        private int delta = 3;

        private void InitForm()
        {
            this.ClientSize = new Size(Defaults.WIDTH, Defaults.HEIGHT);
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

            //mesh = WaveObjHelper.ReadMeshFromFile(appPath + "3dModels\\african_head.obj");

            Float3 whereLightComesFrom = new Float3(1f, 1f, -1f);
            renderer = new Renderer(screen, BackgroundColor, whereLightComesFrom);

            //mesh = getTestBox();
            //rotateMeshAroundXY(mesh, 0, -100);
            mesh = getOverlappedTriangles();
            renderer.Render(mesh, RenderType.Regular, viewDirection);
        }

        private void rotateMeshAroundXY(Mesh mesh, float xAngle, float yAngle)
        {
            // rotation matrix
            Float3x3 R = Float3x3.getRotationMatrix(xAngle, yAngle, 0);

            for (int i = 0; i < mesh.Vertices.Count; i++)
                mesh.Vertices[i] = mesh.Vertices[i].mul(R);
        }      

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawImage(screen, 0, 0);
        }

        private Mesh getSingleTriangle()
        {
            Mesh result = new Mesh();

            result.Vertices = new List<Float3>(){
                new Float3( 1,  0, 0),
                new Float3(-1,  1, 0),
                new Float3(-1, -1, 0),
            };
            result.Triangles = new List<Triangle>(){new Triangle(1, 2, 3, Color.Red)};

            return result;
        }

        private Mesh getOverlappedTriangles()
        {
            Mesh result = new Mesh();

            result.Vertices = new List<Float3>(){
                new Float3( 2,  0, -1),
                new Float3(-1, -1,  1),
                new Float3(-1,  1,  1),

                new Float3(-2,  0, -1),
                new Float3( 1,  1,  1),
                new Float3( 1, -1,  1),
            };
            result.Triangles = new List<Triangle>() {
                new Triangle(1, 2, 3, Color.Red),
                new Triangle(4, 5, 6, Color.Green),
            };

            return result;
        }

        private Mesh getOneAboveTheOverTriangle()
        {
            Mesh result = new Mesh();

            result.Vertices = new List<Float3>(){
                new Float3( 2,  0,  -1),
                new Float3(-1, -1,  -1),
                new Float3(-1,  1,  -1),
                new Float3( 2,  0,  0),
                new Float3(-1, -1,  0),
                new Float3(-1,  1,  0),
            };
            result.Triangles = new List<Triangle>() {
                new Triangle(1, 2, 3, Color.Red),
                new Triangle(4, 5, 6, Color.Green),
            };

            return result;
        }

        private Mesh getTestBox()
        {
            Mesh result = new Mesh();

            result.Vertices = new List<Float3>(){
                    new Float3(1, -1, -1),  // 1
                    new Float3(1, -1, 1),   // 2
                    new Float3(1, 1, 1),    // 3
                    new Float3(1, 1, -1),   // 4
                    new Float3(-1, 1, -1),  // 5
                    new Float3(-1, 1, 1),   // 6
                    new Float3(-1, -1, 1),  // 7
                    new Float3(-1, -1, -1)  // 8
                };
            result.Triangles = new List<Triangle>(){
                    new Triangle(1, 2, 3, Color.Red), new Triangle(1, 3, 4, Color.Red), // right
                    new Triangle(1, 5, 8, Color.Green), new Triangle(1, 4, 5, Color.Green), // front or back
                    new Triangle(4, 3, 6, Color.White), new Triangle(4, 6, 5, Color.FloralWhite),
                    new Triangle(2, 6, 3, Color.DarkViolet), new Triangle(2, 7, 6, Color.Violet), // front or back
                    new Triangle(1, 7, 2, Color.Blue), new Triangle(1, 8, 7, Color.BlueViolet),
                    new Triangle(8, 5, 7, Color.YellowGreen), new Triangle(5, 6, 7, Color.Yellow), // left
                };

            return result;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();

            if (e.KeyCode == Keys.S)
                this.screen.Save(appPath + "screenshot.png", ImageFormat.Png);

            if (e.KeyCode == Keys.A)
            {
                mesh = getTestBox();
                yAngle = (yAngle + delta) % 360;
                rotateMeshAroundXY(mesh, xAngle, yAngle);
                renderer.ResetZBuffer();
                renderer.Render(mesh, RenderType.WireframeAboveRegular, viewDirection);
                this.Refresh();
            }
            if (e.KeyCode == Keys.W)
            {
                mesh = getTestBox();
                xAngle = (xAngle + delta) % 360;
                rotateMeshAroundXY(mesh, xAngle, yAngle);
                renderer.ResetZBuffer();
                renderer.Render(mesh, RenderType.WireframeAboveRegular, viewDirection);
                this.Refresh();
            }
        }
    }
}
