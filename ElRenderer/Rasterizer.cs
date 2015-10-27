using System.Drawing;
using ElRenderer.Model;
// TODO: To use helper methods more comfortable
using static ElRenderer.Utils;
using ElRenderer.Extensions;
using ElRenderer.Algebraic;

namespace ElRenderer
{
    public class IVertex
    {
        public Int3 position;

        public int x { get { return position.x; } }
        public int y { get { return position.y; } }
        public int z { get { return position.z; } }
        
        public float u { get { return uv.x; } }
        public float v { get { return uv.y; } } 

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
                color = start.color.lerpTo(end.color, delta),
                uv = Float2.lerp(start.uv, end.uv, delta),
            };
        }

        public override string ToString()
        {
            return string.Format("pos: {0}, normal: {1}, col: {2}", position, normal, color);
        }
    }

    public class Rasterizer
    {
        private Fragment[,] zBuffer;
                
        // Contructor
        public Rasterizer(Fragment[,] zBuffer)
        {
            this.zBuffer = zBuffer;
        }

        public void Rasterize(Mesh mesh, Material material, Float3 lightDirection)
        {
            // set interpolated color
            for (int i = 0; i < mesh.Triangles.Count; i++)
            {
                Triangle t = mesh.Triangles[i];

                Vertex v1 = mesh.Vertices[t[0] - 1];
                Vertex v2 = mesh.Vertices[t[1] - 1];
                Vertex v3 = mesh.Vertices[t[2] - 1];

                //v2.normal = v1.normal;
                //v3.normal = v1.normal;

                int cc1 = (int)(getLamberComponent(v1.normal, lightDirection) * 255);
                int cc2 = (int)(getLamberComponent(v2.normal, lightDirection) * 255);
                int cc3 = (int)(getLamberComponent(v3.normal, lightDirection) * 255);

                v1.color = Color.FromArgb(cc1, cc1, cc1);//.lerpTo(Color.Green, 0.5f);
                v2.color = Color.FromArgb(cc2, cc2, cc2);//.lerpTo(Color.Blue, 0.5f);
                v3.color = Color.FromArgb(cc3, cc3, cc3);//.lerpTo(Color.Red, 0.5f);
            }
            
            for (int i = 0; i < mesh.Triangles.Count; i++)
            {
                Triangle t = mesh.Triangles[i];
                Vertex v1 = mesh.Vertices[t[0] - 1];
                Vertex v2 = mesh.Vertices[t[1] - 1];
                Vertex v3 = mesh.Vertices[t[2] - 1];
                RenderTriangle2(v1, v2, v3, material, lightDirection);
            }
        }

        private void RenderTriangle2(Vertex _v1, Vertex _v2, Vertex _v3, Material material, Float3 lightDirection)
        {
            IVertex v1 = new IVertex(_v1);
            IVertex v2 = new IVertex(_v2);
            IVertex v3 = new IVertex(_v3);

            if (v1.y > v2.y) swap(ref v1, ref v2);
            if (v1.y > v3.y) swap(ref v1, ref v3);
            if (v2.y > v3.y) swap(ref v2, ref v3);

            int triangleYHeight = v3.y - v1.y + 1;
            int firstSegmentHeight = v2.y - v1.y + 1;
            int secondSegmentHeight = v3.y - v2.y + 1;

            for (int y = v1.y; y <= v3.y; y++)
            {
                bool isFirstSegment = y < v2.y;
                int segmentStartY = isFirstSegment ? v1.y : v2.y;

                float alpha = (float)(y - v1.y) / (float)triangleYHeight;
                IVertex A = IVertex.lerp(v1, v3, alpha);

                float beta = (float)(y - segmentStartY) / (float)(isFirstSegment ? firstSegmentHeight : secondSegmentHeight);
                IVertex B = IVertex.lerp(isFirstSegment ? v1 : v2, isFirstSegment ? v2 : v3, beta);

                if (A.x > B.x)
                    swap(ref A, ref B);
                for (int x = A.x; x <= B.x; x++)
                {
                    // check extremes
                    float delta = (A.x == B.x) ? 1.0f : (float)(x - A.x) / (float)(B.x - A.x);

                    IVertex C = IVertex.lerp(A, B, delta);

                    float lc = getLamberComponent(C.normal, lightDirection);
                    int intLambert = (int)(lc * 255);
                    Color c = Color.FromArgb(intLambert, intLambert, intLambert);

                    c = tex2D(material.diffuseTexture, C.u, C.v);
                    DrawPointToFrameBuffer(x, y, C.z, c);
                }
            }
        }
        
        private float getLamberComponent(Float3 normal, Float3 lightDirection)
        {
            normal = normal.normalize();
            lightDirection = lightDirection.normalize();

            float lambertComponent = normal.dot(lightDirection.normalize());
            lambertComponent = lambertComponent < 0 ? 0 : lambertComponent;
            return Clamp(0, 255, lambertComponent);
        }

        private Color tex2D(Bitmap tex, float u, float v)
        {
            u = Clamp(0f, 1f, u);
            v = Clamp(0f, 1f, v);

            int x = (int)((1 - u) * tex.Width + 0.5f);
            int y = (int)((1 - v) * tex.Height + 0.5f);
            return tex.GetPixel(x, y);
        }

        private void DrawPointToFrameBuffer(int x, int y, float z, Color c)
        {
            // simply skip points outside screen
            if (x < 0 || y < 0 || x > Defaults.WIDTH - 1 || y > Defaults.HEIGHT - 1)
                return;

            // Z buffer test (Z axis points away from viewer/camera)
            if (z < zBuffer[x, y].z)
            {
                zBuffer[x, y].color = c;
                zBuffer[x, y].z = z;
            }
        }
    }
}
