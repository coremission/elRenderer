﻿using System;
using System.Drawing;
using ElRenderer.Model;
// TODO: To use helper methods more comfortable
using static ElRenderer.Utils;

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

                Vertex v1 = mesh.Vertices[t[0] - 1];
                Vertex v2 = mesh.Vertices[t[1] - 1];
                Vertex v3 = mesh.Vertices[t[2] - 1];

                Color color = t.color;
                if (mesh.RenderType == MeshRenderType.Default)
                {
                    Float3 normal = v1.normal;
                    int cc = (int)(255 * normal.dot(whereLightComesFrom.normalize()));
                    // color component must be non-negative
                    cc = cc < 0 ? 0 : cc;
                    color = Color.FromArgb(cc, cc, cc);
                }

                RenderTriangle2(v1, v2, v3, color);
            }
        }

        private void RenderTriangle2(Vertex _v1, Vertex _v2, Vertex _v3, Color color)
        {
            Int3 v1 = (Int3)_v1.position;
            Int3 v2 = (Int3)_v2.position;
            Int3 v3 = (Int3)_v3.position;

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
            segmentHeight = v3.y - v2.y + 1;
            for (int y = v2.y; y <= v3.y; y++)
            {
                float alpha = (float)(y - v1.y) / (float)triangleYHeight;
                float beta = (float)(y - v2.y) / (float)segmentHeight;
                Int3 A = Int3.lerp(v1, v3, alpha);
                Int3 B = Int3.lerp(v2, v3, beta);

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
                int cc = (int)(((float)(z + 150.0)/(float)300.0 ) * 255);
                zBuffer[x, y].color = c;// Color.FromArgb(cc, cc, cc);
                zBuffer[x, y].z = z;
            }
        }
    }
}
