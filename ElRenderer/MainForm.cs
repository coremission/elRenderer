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
            
            Mesh mesh = WaveObjHelper.ReadMeshFromFile(appPath + "african_head.obj");

            renderer = new Renderer();
            renderer.RenderTo(screen, mesh);
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
