using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ElRenderer.Model;

namespace ElRenderer.Pipeline
{
    public class elGPU
    {
        public Bitmap Draw(Mesh mesh, int screenWidth, int screenHeight, params object[] uniforms)
        {
            float scaleFactor = 300;
            Float3x3 S = Float3x3.identity * scaleFactor;
            Float3x3 R = Float3x3.getRotationMatrix(0, 0, 0);

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

                screen.elDrawTriangle(v1, v2, v3, getRandomColor());
                //screen.elDrawLine(v1.x, v1.y, v2.x, v2.y, color);
                //screen.elDrawLine(v2.x, v2.y, v3.x, v3.y, color);
                //screen.elDrawLine(v3.x, v3.y, v1.x, v1.y, color);
            }
            return null;
        }
    }
}
