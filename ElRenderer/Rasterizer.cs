﻿using System;
using System.Drawing;
using ElRenderer.Model;
// TODO: To use helper methods more comfortable
using static ElRenderer.Utils;
using ElRenderer.Extensions;

namespace ElRenderer
{
    public class IVertex
    {
        public Int3 position;

        public int x { get { return position.x; } }
        public int y { get { return position.y; } }
        public int z { get { return position.z; } }

        public Float3 normal;

        public Color color;

        public Float2 uv;

        public IVertex() { }
        public IVertex(Vertex v)
        {
            this.position = (Int3)v.position;
            this.normal = v.normal;
            this.color = v.color;
            this.uv = v.uv;
        }

        public static IVertex lerp(IVertex start, IVertex end, float delta)
        {
            return new IVertex()
            {
                position = Int3.lerp(start.position, end.position, delta),
                normal = Float3.lerp(start.normal, end.normal, delta),
                color = start.color.lerpTo(end.color, delta)
            };
        }

        public override string ToString()
        {
            return string.Format("pos: {0}, normal: {1}, col: {2}", position, normal, color);
        }
    }

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
            // set interpolated color
            for (int i = 0; i < mesh.Triangles.Count; i++)
            {
                Triangle t = mesh.Triangles[i];

                Vertex v1 = mesh.Vertices[t[0] - 1];
                Vertex v2 = mesh.Vertices[t[1] - 1];
                Vertex v3 = mesh.Vertices[t[2] - 1];

                int cc1 = getLamberComponent(v1.normal, lightDirection);
                int cc2 = getLamberComponent(v2.normal, lightDirection);
                int cc3 = getLamberComponent(v3.normal, lightDirection);

                v1.color = v1.color.lerpTo(Color.FromArgb(cc1, cc1, cc1), 0.5f);//.lerpTo(Color.Green, 0.5f);
                v2.color = v2.color.lerpTo(Color.FromArgb(cc2, cc2, cc2), 0.5f);//.lerpTo(Color.Blue, 0.5f);
                v3.color = v3.color.lerpTo(Color.FromArgb(cc3, cc3, cc3), 0.5f);//.lerpTo(Color.Red, 0.5f);
            }

            for (int i = 0; i < mesh.Triangles.Count; i++)
            {
                Triangle t = mesh.Triangles[i];
                Vertex v1 = mesh.Vertices[t[0] - 1];
                Vertex v2 = mesh.Vertices[t[1] - 1];
                Vertex v3 = mesh.Vertices[t[2] - 1];
                RenderTriangle2(v1, v2, v3, lightDirection);
            }
        }

        private void RenderTriangle2(Vertex _v1, Vertex _v2, Vertex _v3, Float3 lightDirection)
        {
            IVertex v1 = new IVertex(_v1);
            IVertex v2 = new IVertex(_v2);
            IVertex v3 = new IVertex(_v3);

            if (v1.y > v2.y) swap(ref v1, ref v2);
            if (v1.y > v3.y) swap(ref v1, ref v3);
            if (v2.y > v3.y) swap(ref v2, ref v3);

            int triangleYHeight = v3.y - v1.y + 1;
            int segmentHeight = v2.y - v1.y + 1;

            for (int y = v1.y; y <= v2.y; y++)
            {
                float alpha = (float)(y - v1.y) / (float)triangleYHeight;
                float beta = (float)(y - v1.y) / (float)segmentHeight;

                IVertex A = IVertex.lerp(v1, v3, alpha);
                IVertex B = IVertex.lerp(v1, v2, beta);

                if (A.x > B.x)
                    swap(ref A, ref B);
                for (int x = A.x; x <= B.x; x++)
                {
                    // check extremes
                    float delta = (A.x == B.x) ? 1.0f : (float)(x - A.x) / (float)(B.x - A.x);

                    IVertex C = IVertex.lerp(A, B, delta);

                    int lc = getLamberComponent(C.normal, lightDirection);
                    Color c = Color.FromArgb(lc, lc, lc);

                    DrawPointToFrameBuffer(x, y, C.z, c);
                }
            }

            segmentHeight = v3.y - v2.y + 1;
            for (int y = v2.y; y <= v3.y; y++)
            {
                float alpha = (float)(y - v1.y) / (float)triangleYHeight;
                float beta = (float)(y - v2.y) / (float)segmentHeight;
                IVertex A = IVertex.lerp(v1, v3, alpha);
                IVertex B = IVertex.lerp(v2, v3, beta);

                if (A.x > B.x)
                    swap(ref A, ref B);
                for (int x = A.x; x <= B.x; x++)
                {
                    // check extremes
                    float delta = (A.x == B.x) ? 1.0f : (float)(x - A.x) / (float)(B.x - A.x);

                    IVertex C = IVertex.lerp(A, B, delta);

                    int lc = getLamberComponent(C.normal, lightDirection);
                    Color c = Color.FromArgb(lc, lc, lc);

                    DrawPointToFrameBuffer(x, y, C.z, c);
                }
            }
        }
        
        private int getLamberComponent(Float3 normal, Float3 lightDirection)
        {
            normal = normal.normalize();
            lightDirection = lightDirection.normalize();

            int lambertComponent = (int)(255 * normal.dot(lightDirection.normalize()));
            lambertComponent = lambertComponent < 0 ? 0 : lambertComponent;
            return lambertComponent;
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
