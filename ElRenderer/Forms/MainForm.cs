﻿using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using ElRenderer.Service;
using ElRenderer.Model;

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
        private Float3 whereLightComesFrom = new Float3(0f, 0f, -1f);

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
            appPath = Application.ExecutablePath;
            appPath = appPath.Substring(0, appPath.LastIndexOf('\\') + 1);
        }

        public MainForm()
        {
            InitForm();
            screen = new Bitmap(Defaults.WIDTH, Defaults.HEIGHT);
            renderer = new Renderer(screen, BackgroundColor, whereLightComesFrom);

            mesh = WaveObjHelper.ReadMeshFromFile(appPath + "3dModels\\african_head.obj");

            //mesh = Test_Data.getOverlappedTriangles();
            RenderType renderType = RenderType.Regular;

            renderer.Render(mesh, renderType, viewDirection);
        }

        private void rotateMeshAroundXY(Mesh mesh, float xAngle, float yAngle)
        {
            // rotation matrix
            Float3x3 R = Float3x3.getRotationMatrix(xAngle, yAngle, 0);

            for (int i = 0; i < mesh.Vertices.Count; i++)
            {
                Vertex v = mesh.Vertices[i];
                v.position = v.position.mul(R);
            }
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
