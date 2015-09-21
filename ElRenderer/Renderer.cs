using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;
using ElRenderer.Model;

namespace ElRenderer
{
    public class Renderer
    {
        private Float3 lightDirection = new Float3(0, -1, 0).normalize();

        public void RenderTo(Bitmap screen, Mesh mesh)
        {
            Int2[] t0 = new[] { new Int2(10, 70), new Int2(50, 160), new Int2(70, 80) };
            Int2[] t1 = new[] { new Int2(180, 50), new Int2(150, 1), new Int2(70, 180) };
            Int2[] t2 = new[] { new Int2(180, 150), new Int2(120, 160), new Int2(130, 180) };

            //screen.elDrawTriangle2(t0[0], t0[1], t0[2], Color.Red);
            //screen.elDrawTriangle2(t1[0], t1[1], t1[2], Color.White);
            //screen.elDrawTriangle2(t2[0], t2[1], t2[2], Color.GreenYellow);

                      
            float scaleFactor = 300;
            Float3x3 S = Float3x3.identity * scaleFactor;
            Float3x3 R = Float3x3.getRotationMatrix(0, 0, 0);

            for (int i = 0; i < mesh.Vertices.Count; i++)
            {
                Float3 scaled = mesh.Vertices[i].mul(S);
                scaled = scaled.mul(R);

                mesh.Vertices[i] = new Float3(scaled.x + Defaults.WIDTH / 2, scaled.y + Defaults.HEIGHT / 2, scaled.z);

            }

            int trianglesCount = mesh.Triangles.Count;

            for (int i = 0; i < trianglesCount; i++)
            {
                Triangle t = mesh.Triangles[i];

                Float3 v1 = mesh.Vertices[t[0] - 1];
                Float3 v2 = mesh.Vertices[t[1] - 1];
                Float3 v3 = mesh.Vertices[t[2] - 1];
                Float3 normal = (v3 - v1).cross(v2 - v1).normalize();

                int cc = (int)(255 * normal.dot(lightDirection.getOpposite()));

                // TODO: it is like backface culling
                if (cc < 0)
                    continue;

                Color c = Color.FromArgb(cc, cc, cc);

                screen.elDrawTriangle2(v1.xy, v2.xy, v3.xy, c);
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
