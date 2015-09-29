using System.Collections.Generic;

namespace ElRenderer.Model
{
    public enum MeshRenderType
    {
        Default = 0,
        ColoredTriangular = 1, // Color stored per single triangle
    }

    public class Mesh
    {
        public List<Vertex> Vertices;
        public List<Triangle> Triangles;
        public MeshRenderType RenderType;

        public Mesh()
        {
            Vertices = new List<Vertex>();
            Triangles = new List<Triangle>();
            RenderType = MeshRenderType.Default;
        }

        public void RecalculateNormals()
        {
            for (int i = 0; i < Triangles.Count; i++)
            {
                Triangle t = Triangles[i];

                Vertex v1 = Vertices[t[0] - 1];
                Vertex v2 = Vertices[t[1] - 1];
                Vertex v3 = Vertices[t[2] - 1];

                v1.normal = Utils.getTriangleNormal(v1.position, v2.position, v3.position);
                v2.normal = Utils.getTriangleNormal(v2.position, v1.position, v3.position);
                v3.normal = Utils.getTriangleNormal(v3.position, v2.position, v1.position);
            }
        }
    }
}
