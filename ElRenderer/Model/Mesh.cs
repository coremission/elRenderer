using ElRenderer.Algebraic;
using System.Collections.Generic;

namespace ElRenderer.Model
{
    public class Mesh
    {
        public List<Vertex> Vertices;
        public List<Triangle> Triangles;

        public Mesh()
        {
            Vertices = new List<Vertex>();
            Triangles = new List<Triangle>();
        }

        public void RecalculateAverageNormals()
        {
            for (int i = 0; i < Triangles.Count; i++)
            {
                Triangle t = Triangles[i];

                Vertex v1 = Vertices[t[0] - 1];
                Vertex v2 = Vertices[t[1] - 1];
                Vertex v3 = Vertices[t[2] - 1];

                Float3 v1n = Utils.getTriangleNormal(v1.position, v2.position, v3.position);
                Float3 v2n = Utils.getTriangleNormal(v2.position, v3.position, v1.position);
                Float3 v3n = Utils.getTriangleNormal(v3.position, v1.position, v2.position);

                v1.normal = v1.normal == Float3.zero ? v1n : lerpNormal(v1.normal, v1n);
                v2.normal = v2.normal == Float3.zero ? v2n : lerpNormal(v2.normal, v2n);
                v3.normal = v3.normal == Float3.zero ? v3n : lerpNormal(v3.normal, v3n);
            }
        }

        public void RecalculateNormals()
        {
            for (int i = 0; i < Triangles.Count; i++)
            {
                Triangle t = Triangles[i];

                Vertex v1 = Vertices[t[0] - 1];
                Vertex v2 = Vertices[t[1] - 1];
                Vertex v3 = Vertices[t[2] - 1];

                Float3 v1n = Utils.getTriangleNormal(v1.position, v2.position, v3.position);
                Float3 v2n = Utils.getTriangleNormal(v2.position, v3.position, v1.position);
                Float3 v3n = Utils.getTriangleNormal(v3.position, v1.position, v2.position);

                v1.normal = v1n;
                v2.normal = v2n;
                v3.normal = v3n;
            }
        }

        public Float3 lerpNormal(Float3 currentValue, Float3 newValue)
        {
            if (currentValue == Float3.zero)
                return newValue;

            return Float3.lerp(currentValue, newValue, 0.5f).normalize();
        }
    }
}
