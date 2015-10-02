using System.IO;
using ElRenderer.Model;
using System.Collections.Generic;

namespace ElRenderer.Service
{
    public static class WaveObjHelper
    {
        public static Mesh ReadMeshFromFile(string filePath)
        {
            Mesh result = new Mesh();
            string[] lines = File.ReadAllLines(filePath);

            List<Float3> normals = new List<Float3>();
            List<Float3> vPositions = new List<Float3>();

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
                    int a = int.Parse(lineParts[1].Split('/')[0]);
                    int b = int.Parse(lineParts[2].Split('/')[0]);
                    int c = int.Parse(lineParts[3].Split('/')[0]);

                    result.Triangles.Add(new Triangle(a, b, c));
                }
            }
            
            for(int i = 0; i < vPositions.Count; i++)
            {
                Vertex v = new Vertex(vPositions[i]);
                if(normals.Count > 0)
                    v.normal = normals[i].getOpposite().normalize();
                result.Vertices.Add(v); 
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
