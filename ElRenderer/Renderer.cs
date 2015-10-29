using System;
using System.Drawing;
using ElRenderer.Model;
using ElRenderer.Algebraic;

namespace ElRenderer
{
    [Flags]
    public enum RenderType
    {
        Regular = 1,
        Wireframe = 2,
        RegularWithWireframe = 3,
        Normals = 4,
        RegularWithNormals = 5,
        RegularWithWireframeAndNormals = 7
    }

    public class Renderer
    {
        private Bitmap bitmap;
        private Color backGroundColor;
        private Fragment[,] zBuffer = new Fragment[Defaults.WIDTH, Defaults.HEIGHT];
        private Rasterizer rasterizer;
        private bool backFaceCulling = false;

        public void ResetZBuffer()
        {
            for (int x = 0; x < Defaults.WIDTH; x++)
            {
                for (int y = 0; y < Defaults.HEIGHT; y++)
                {
                    zBuffer[x, y] = new Fragment(backGroundColor);
                    bitmap.SetPixel(x, y, backGroundColor);
                }
            }

            rasterizer = new Rasterizer(zBuffer);
        }

        // Constructor
        public Renderer(Bitmap bitmap, Color backGroundColor)
        {
            this.bitmap = bitmap;
            this.backGroundColor = backGroundColor;
            ResetZBuffer();
        }

        public void DrawTestTriangles(Bitmap bitmap)
        {
            Int2[] t0 = new[] { new Int2(10, 70), new Int2(50, 160), new Int2(70, 80) };
            Int2[] t1 = new[] { new Int2(180, 50), new Int2(150, 1), new Int2(70, 180) };
            Int2[] t2 = new[] { new Int2(180, 150), new Int2(120, 160), new Int2(130, 180) };
        }

        public void Render(SceneObject sObject, Float3 viewDirection, Float3 lightDirection)
        {
            Mesh mesh = sObject.mesh;
            Color wireFrameColor = Color.LightGreen;
            RenderType renderType = sObject.material.renderType;

            // Vertex uniforms

            // scale matrix
            Float3x3 S = Float3x3.identity * sObject.uniformScale;
            // rotation matrix
            Float3x3 R = Float3x3.getRotationMatrix(sObject.rotation);
            Float3x3 CombinedLinear = S * R;
            // projection/translation
            Float4x4 PT = new Float4x4(CombinedLinear);
            PT.setTranslation(sObject.localPosition);
            PT.setProjection(1);
            // BACK FACE CULLING
            if (backFaceCulling)
            {
                for (int i = mesh.Triangles.Count - 1; i >= 0; i--)
                {
                    Triangle t = mesh.Triangles[i];

                    Float3 v1 = mesh.Vertices[t[0] - 1].position;
                    Float3 v2 = mesh.Vertices[t[1] - 1].position;
                    Float3 v3 = mesh.Vertices[t[2] - 1].position;
                    Float3 normal = Utils.getTriangleNormalR(v1, v2, v3);

                    // remove faced back triangles
                    if (viewDirection.dot(normal) >= 0)
                        mesh.Triangles.Remove(t);
                }
            }
            // VERTEX SHADER
            for (int i = 0; i < mesh.Vertices.Count; i++)
            {
                Vertex v = mesh.Vertices[i];
                Float3 p = PT.transformPoint(v.position);
                // TODO: Transforming normals while NON UNIFORM TRANSFORMS
                v.normal = v.normal.mul(R);

                // TODO: place to center of screen
                v.position = new Float3(p.x + Defaults.WIDTH / 2, p.y + Defaults.HEIGHT / 2, p.z);
            }

            if((renderType & RenderType.Regular) != 0)
                RenderRegular(mesh, sObject.material, lightDirection);

            if ((renderType & RenderType.Wireframe) != 0)
                RenderWireframe(mesh, wireFrameColor);

            if ((renderType & RenderType.Normals) != 0)
                DrawVertexNormals(mesh, Color.Red);
        }

        private void RenderRegular(Mesh mesh, Material material, Float3 lightDirection)
        {
            rasterizer.Rasterize(mesh, material, lightDirection);

            // FRAGMENT SHADER
            for (int x = 0; x < Defaults.WIDTH; x++)
                for (int y = 0; y < Defaults.HEIGHT; y++)
                    bitmap.elDrawPoint(x, y, zBuffer[x, y].color);
        }

        private void RenderWireframe(Mesh mesh, Color color)
        {
            for (int i = 0; i < mesh.Triangles.Count; i++)
            {
                Triangle t = mesh.Triangles[i];

                Float3 v1 = mesh.Vertices[t[0] - 1].position;
                Float3 v2 = mesh.Vertices[t[1] - 1].position;
                Float3 v3 = mesh.Vertices[t[2] - 1].position;

                bitmap.elDrawLine(v1.xy, v2.xy, color);
                bitmap.elDrawLine(v2.xy, v3.xy, color);
                bitmap.elDrawLine(v3.xy, v1.xy, color);
            }
        }

        private void DrawVertexNormals(Mesh mesh, Color color)
        {
            for (int i = 0; i < mesh.Vertices.Count; i++)
            {
                Vertex v = mesh.Vertices[i];

                bitmap.elDrawLine(v.position, (v.position + v.normal * 30), color);
            }
        }
    }
}
