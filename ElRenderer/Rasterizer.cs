﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;
using ElRenderer.Model;

namespace ElRenderer
{
    public class Rasterizer
    {
        private Float3 lightDirection;
        private Fragment[,] zBuffer;
                
        // Contructor
        public Rasterizer(Fragment[,] zBuffer, Float3 lightDirection)
        {
            this.zBuffer = zBuffer;
            this.lightDirection = lightDirection;
        }

        public void Rasterize(Mesh mesh)
        {
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

                RenderTriangle2(v1, v2, v3, c);
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

                for (int x = min(A.x, B.x); x <= max(A.x, B.x); x++)
                {
                    // check extremes
                    float delta = (A.x == B.x) ? 1.0f : (float)(x - A.x) / (float)(A.x - B.x);
                    Int3 C = Int3.lerp(A, B, delta);

                    DrawPointToFrameBuffer(x, y, C.z, color);
                }
            }
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
                    float delta = (A.x == B.x) ? 1.0f : (float)(x - A.x) / (float)(A.x - B.x);
                    Int3 C = Int3.lerp(A, B, delta);

                    DrawPointToFrameBuffer(x, y, C.z, color);
                }
            }
        }

        private void DrawPointToFrameBuffer(int x, int y, float z, Color c)
        {
            // Z buffer test
            if (zBuffer[x, y].z < z)
            {
                zBuffer[x, y].color = c;
                zBuffer[x, y].z = z;
            }
        }
    }
}