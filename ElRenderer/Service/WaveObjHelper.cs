using System.IO;
using ElRenderer.Model;
using System.Collections.Generic;
using ElRenderer.Algebraic;

namespace ElRenderer.Service
{
    public static class WaveObjHelper
    {
        private class WObjTriangle
        {
            public int v1;
            public int v2;
            public int v3;

            public int n1;
            public int n2;
            public int n3;

            public int uv1;
            public int uv2;
            public int uv3;

            public WObjTriangle(int v1, int v2, int v3,
                                int n1, int n2, int n3,
                                int uv1, int uv2, int uv3)
            {
                this.v1 = v1;
                this.v2 = v2;
                this.v3 = v3;

                this.n1 = n1;
                this.n2 = n2;
                this.n3 = n3;

                this.uv1 = uv1;
                this.uv2 = uv2;
                this.uv3 = uv3;
            }

            public override string ToString()
            {
                return string.Format("vi[{0}, {1}, {2}]; ni[{3}, {4}, {5}]; uvi[{6}, {7}, {8}];", v1, v2, v3, n1, n2, n3, uv1, uv2, uv3);
            }
        }

        public static Mesh ReadMeshFromFile(string filePath)
        {
            Mesh result = new Mesh();
            string[] lines = File.ReadAllLines(filePath);

            List<Float3> normals = new List<Float3>();
            List<Float3> vPositions = new List<Float3>();
            List<Float2> uvs = new List<Float2>();
            List<WObjTriangle> wTriangles = new List<WObjTriangle>();

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Length == 0)
                    continue;

                string[] lineParts = ReplaceMultipleSpaces(lines[i]).Split(' ');
                
                if (lineParts[0] == "v")
                {
                    Float3 position = new Float3(float.Parse(lineParts[1], System.Globalization.CultureInfo.InvariantCulture),
                                                 float.Parse(lineParts[2], System.Globalization.CultureInfo.InvariantCulture),
                                                 float.Parse(lineParts[3], System.Globalization.CultureInfo.InvariantCulture)
                                                 );

                    vPositions.Add(position);
                }
                if (lineParts[0] == "vt")
                {
                    Float2 uv = new Float2(float.Parse(lineParts[1], System.Globalization.CultureInfo.InvariantCulture),
                                                 float.Parse(lineParts[2], System.Globalization.CultureInfo.InvariantCulture));
                    uvs.Add(uv);
                }
                if (lineParts[0] == "vn")
                {
                    Float3 normal = new Float3(float.Parse(lineParts[1], System.Globalization.CultureInfo.InvariantCulture),
                                                float.Parse(lineParts[2], System.Globalization.CultureInfo.InvariantCulture),
                                                float.Parse(lineParts[3], System.Globalization.CultureInfo.InvariantCulture)
                                                );
                    normals.Add(normal);
                }
                if (lineParts[0] == "f")
                {
                    string[] f1 = lineParts[1].Split('/');
                    string[] f2 = lineParts[2].Split('/');
                    string[] f3 = lineParts[3].Split('/');

                    int v1 = int.Parse(f1[0]);
                    int v2 = int.Parse(f2[0]);
                    int v3 = int.Parse(f3[0]);

                    int t1 = -1;
                    int t2 = -1;
                    int t3 = -1;

                    if (!string.IsNullOrEmpty(f1[1]))
                        t1 = int.Parse(f1[1]);
                    if (!string.IsNullOrEmpty(f2[1]))
                        t2 = int.Parse(f2[1]);
                    if (!string.IsNullOrEmpty(f3[1]))
                        t3 = int.Parse(f3[1]);

                    int n1 = 0, n2 = 0, n3 = 0;
                    // if normals specified
                    if (f1.Length > 2) {
                        n1 = int.Parse(f1[2]);
                        n2 = int.Parse(f2[2]);
                        n3 = int.Parse(f3[2]);
                    }

                    wTriangles.Add(new WObjTriangle(v1, v2, v3, n1, n2, n3, t1, t2, t3));
                    result.Triangles.Add(new Triangle(v1, v2, v3));
                }
            }

            result.Vertices = new List<Vertex>();
            for(int i = 0; i < vPositions.Count; i++)
            {
                result.Vertices.Add(new Vertex(vPositions[i]));
            }

            for(int i = 0; i < wTriangles.Count; i++)
            {
                WObjTriangle wTriangle = wTriangles[i];

                Vertex v1 = result.Vertices[wTriangle.v1 - 1];
                Vertex v2 = result.Vertices[wTriangle.v2 - 1];
                Vertex v3 = result.Vertices[wTriangle.v3 - 1];

                if(normals.Count > 0)
                {
                    v1.normal = normals[wTriangle.n1 - 1];
                    v2.normal = normals[wTriangle.n2 - 1];
                    v3.normal = normals[wTriangle.n3 - 1];
                }
                
                v1.uv = uvs[wTriangle.uv1 - 1];
                v2.uv = uvs[wTriangle.uv2 - 1];
                v3.uv = uvs[wTriangle.uv3 - 1];
            }

            return result;
        }

        private static string ReplaceMultipleSpaces(string s)
        {
            return System.Text.RegularExpressions.Regex.Replace(s, @"\s+", " ");
        }

        public static Mesh ReadMeshFromStream(Stream stream)
        {
            return new Mesh();
        }
    }
}
