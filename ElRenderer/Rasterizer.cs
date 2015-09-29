using System;
using System.Drawing;
using ElRenderer.Model;

namespace ElRenderer
{
    public class Rasterizer
    {
        private Float3 whereLightComesFrom;
        private Fragment[,] zBuffer;
                
        // Contructor
        public Rasterizer(Fragment[,] zBuffer, Float3 whereLightComesFrom)
        {
            this.zBuffer = zBuffer;
            this.whereLightComesFrom = whereLightComesFrom;
        }

        public void Rasterize(Mesh mesh)
        {
            for (int i = 0; i < mesh.Triangles.Count; i++)
            {
                Triangle t = mesh.Triangles[i];

                Float3 v1 = mesh.Vertices[t[0] - 1];
                Float3 v2 = mesh.Vertices[t[1] - 1];
                Float3 v3 = mesh.Vertices[t[2] - 1];
                Float3 normal = Utils.getTriangleNormal(v1, v2, v3);

                int cc = (int)(255 * normal.dot(whereLightComesFrom.normalize()));

                // TODO: it is like backface culling
                if (cc < 0)
                    continue;

                Color c = Color.FromArgb(cc, cc, cc);

                RenderTriangle2(v1, v2, v3, t.color);
            }
        }

        #region Helper
        private static void swap<T>(ref T a, ref T b)
        {
            T buffer = a;
            a = b;
            b = buffer;
        }

        private int _int(float f)
        {
            return (int)(f + 0.5f); 
        }

        private int lerp(int start, int end, float delta)
        {
            return (int)(start + (end - start) * delta + 0.5f);
        }

        private T min<T>(T a, T b) where T : IComparable<T>
        {
            return a.CompareTo(b) <= 0 ? a : b;
        }

        private T max<T>(T a, T b) where T : IComparable<T>
        {
            return a.CompareTo(b) >= 0 ? a : b;
        }

        #endregion

        private void RenderTriangle2(Float3 v1, Float3 v2, Float3 v3, Color color)
        {
            RenderTriangle2((Int3)v1, (Int3)v2, (Int3)v3, color);
        }

        private void RenderTriangle2(Int3 v1, Int3 v2, Int3 v3, Color color)
        {
            if (v1.y > v2.y) swap(ref v1, ref v2);
            if (v1.y > v3.y) swap(ref v1, ref v3);
            if (v2.y > v3.y) swap(ref v2, ref v3);

            int triangleYHeight = v3.y - v1.y + 1;
            int segmentHeight = v2.y - v1.y + 1;

            for (int y = v1.y; y <= v2.y; y++)
            {
                float alpha = (float)(y - v1.y) / (float)triangleYHeight;
                float beta = (float)(y - v1.y) / (float)segmentHeight;
                Int3 A = Int3.lerp(v1, v3, alpha);
                Int3 B = Int3.lerp(v1, v2, beta);

                if (A.x > B.x)
                    swap(ref A, ref B);
                for (int x = A.x; x <= B.x; x++)
                {
                    // check extremes
                    float delta = (A.x == B.x) ? 1.0f : (float)(x - A.x) / (float)(B.x - A.x);

                    if (delta < 0 || delta > 1)
                        throw new ArgumentException("delta < 0");
                
                    Int3 C = Int3.lerp(A, B, delta);

                    DrawPointToFrameBuffer(x, y, C.z, color);
                }
            }
            return;
            segmentHeight = v3.y - v2.y + 1;
            for (int y = v2.y; y <= v3.y; y++)
            {
                float alpha = (float)(y - v1.y) / (float)triangleYHeight;
                float beta = (float)(y - v2.y) / (float)segmentHeight;

                Int3 A = Int3.lerp(v1, v3, alpha);
                Int3 B = Int3.lerp(v2, v3, beta);

                for (int x = min(A.x, B.x); x <= max(A.x, B.x); x++)
                {
                    // check extremes
                    float delta = (A.x == B.x) ? 1.0f : (float)(x - A.x) / (float)Math.Abs(A.x - B.x);
                    Int3 C = Int3.lerp(A, B, delta);

                    DrawPointToFrameBuffer(x, y, C.z, color);
                }
            }
        }

        private void DrawPointToFrameBuffer(int x, int y, float z, Color c)
        {
            // simply skip points outside screen
            if (x < 0 || y < 0 || x > Defaults.WIDTH - 1 || y > Defaults.HEIGHT - 1)
                return;

            float bZ = zBuffer[x, y].z;

            if(z < bZ && bZ < float.PositiveInfinity)
            {
                ;
            }
            if(zBuffer[x, y].z < float.PositiveInfinity)
            {
                ;
            }
            // Z buffer test (Z axis points away from viewer/camera)
            if (z < zBuffer[x, y].z)
            {
                zBuffer[x, y].color = c;
                zBuffer[x, y].z = z;
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
