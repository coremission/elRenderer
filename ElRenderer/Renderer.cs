using System;
using System.Drawing;
using ElRenderer.Model;

namespace ElRenderer
{
    public class Renderer
    {
        private Float3 lightDirection = new Float3(0, -1, 0).normalize();
        private Fragment[,] zBuffer = new Fragment[Defaults.HEIGHT, Defaults.WIDTH];
        private Rasterizer rasterizer;
        // Constructor
        public Renderer(Color backGroundColor)
        {
            for (int y = 0; y < Defaults.HEIGHT; y++)
                for (int x = 0; x < Defaults.WIDTH; x++)
                    zBuffer[y, x] = new Fragment(backGroundColor);

            rasterizer = new Rasterizer(zBuffer, lightDirection);
        }

        public void DrawTestTriangles(Bitmap screen)
        {
            Int2[] t0 = new[] { new Int2(10, 70), new Int2(50, 160), new Int2(70, 80) };
            Int2[] t1 = new[] { new Int2(180, 50), new Int2(150, 1), new Int2(70, 180) };
            Int2[] t2 = new[] { new Int2(180, 150), new Int2(120, 160), new Int2(130, 180) };
        }

        public void RenderTo(Bitmap screen, Mesh mesh)
        {
            // Vertex uniforms

            // scale matrix
            Float3x3 S = Float3x3.identity * 30;
            // rotation matrix
            Float3x3 R = Float3x3.getRotationMatrix(0, 0, 0);

            Float3x3 Combined = S * R;

            // VERTEX SHADER
            for (int i = 0; i < mesh.Vertices.Count; i++)
            {
                Float3 v = mesh.Vertices[i].mul(Combined);
                mesh.Vertices[i] = new Float3(v.x + Defaults.WIDTH / 2, v.y + Defaults.HEIGHT / 2, v.z);
            }

            rasterizer.Rasterize(mesh);

            // FRAGMENT SHADER
            for (int x = 0; x < Defaults.HEIGHT; x++)
                for (int y = 0; y < Defaults.WIDTH; y++)
                    screen.elDrawPoint(x, y, zBuffer[x, y].color);
        }

        private Color getRandomColor()
        {
            Random randomizer = new Random();
            int r = randomizer.Next();
            int g = randomizer.Next();
            int b = randomizer.Next();
            return Color.FromArgb(r % 255, g % 255, b % 255);
        }
    }
}
