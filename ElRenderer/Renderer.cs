using System;
using System.Drawing;
using ElRenderer.Model;

namespace ElRenderer
{
    public enum RenderType
    {
        Regular,
        Wireframe,
        WireframeAboveRegular,
        Points
    }
    public class Renderer
    {
        private Float3 lightDirection;
        private Fragment[,] zBuffer = new Fragment[Defaults.WIDTH, Defaults.HEIGHT];
        private Rasterizer rasterizer;
        // Constructor
        public Renderer(Color backGroundColor, Float3 normalizedLightDirection)
        {
            this.lightDirection = normalizedLightDirection;

            for (int x = 0; x < Defaults.WIDTH; x++)
                for (int y = 0; y < Defaults.HEIGHT; y++)
                    zBuffer[x, y] = new Fragment(backGroundColor);

            rasterizer = new Rasterizer(zBuffer, lightDirection);
        }

        public void DrawTestTriangles(Bitmap screen)
        {
            Int2[] t0 = new[] { new Int2(10, 70), new Int2(50, 160), new Int2(70, 80) };
            Int2[] t1 = new[] { new Int2(180, 50), new Int2(150, 1), new Int2(70, 180) };
            Int2[] t2 = new[] { new Int2(180, 150), new Int2(120, 160), new Int2(130, 180) };
        }

        public void RenderTo(Bitmap screen, Mesh mesh, RenderType renderType)
        {
            Color wireFrameColor = Color.LightGreen;
            // Vertex uniforms

            // scale matrix
            Float3x3 S = Float3x3.identity * 50;
            // rotation matrix
            Float3x3 R = Float3x3.getRotationMatrix(0, 0, 0);

            Float3x3 Combined = S * R;

            // VERTEX SHADER
            for (int i = 0; i < mesh.Vertices.Count; i++)
            {
                Float3 v = mesh.Vertices[i].mul(Combined);
                mesh.Vertices[i] = new Float3(v.x + Defaults.WIDTH / 2, v.y + Defaults.HEIGHT / 2, v.z);
            }

            switch(renderType)
            {
                case RenderType.Regular:
                    RenderRegular(screen, mesh);
                    return;
                case RenderType.Wireframe:
                    RenderWireframe(screen, mesh, wireFrameColor);
                    return;
                case RenderType.WireframeAboveRegular:
                    RenderRegular(screen, mesh);
                    RenderWireframe(screen, mesh, wireFrameColor);
                    return;
            }
            
        }

        private void RenderRegular(Bitmap screen, Mesh mesh)
        {
            rasterizer.Rasterize(mesh);

            // FRAGMENT SHADER
            for (int x = 0; x < Defaults.WIDTH; x++)
                for (int y = 0; y < Defaults.HEIGHT; y++)
                    screen.elDrawPoint(x, y, zBuffer[x, y].color);
        }

        private void RenderWireframe(Bitmap screen, Mesh mesh, Color color)
        {
            for (int i = 0; i < mesh.Triangles.Count; i++)
            {
                Triangle t = mesh.Triangles[i];

                Float3 v1 = mesh.Vertices[t[0] - 1];
                Float3 v2 = mesh.Vertices[t[1] - 1];
                Float3 v3 = mesh.Vertices[t[2] - 1];

                screen.elDrawLine(v1.xy, v2.xy, color);
                screen.elDrawLine(v2.xy, v3.xy, color);
                screen.elDrawLine(v3.xy, v1.xy, color);
            }
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
