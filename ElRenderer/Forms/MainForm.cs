using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using ElRenderer.Service;
using ElRenderer.Model;
using ElRenderer.Algebraic;

namespace ElRenderer
{
    public class MainForm : Form
    {
        Bitmap screen;
        private string appPath;
        private readonly Color BackgroundColor = Color.Black;
        private Renderer renderer;
        private Mesh mesh;
        // vector from viewport to object
        private Float3 viewDirection = new Float3(0, 0, 1).normalize();
        // where lights come from, world coordinates, vector from origin to light source
        private Float3 lightDirection = new Float3(1f, 3, -2).normalize();

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
            renderer = new Renderer(screen, BackgroundColor);

            mesh = WaveObjHelper.ReadMeshFromFile(appPath + @"3dModels\bear.obj");
            //mesh.RecalculateNormals();

            Bitmap texture = Paloma.TargaImage.LoadTargaImage(appPath + @"3dModels\bear.tga");

            SceneObject sObject = new SceneObject {    mesh = mesh,
                                                       material = new Material(texture, RenderType.RegularWithWireframe),
                                                       uniformScale = 20f,
                                                       rotation = new Float3(40, 150, 0),
                                                       localPosition = new Float3(0, -210, 550)
                                                   };

            renderer.Render(sObject, viewDirection, lightDirection, true);
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
