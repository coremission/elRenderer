﻿using System.IO;
using ElRenderer.Model;

namespace ElRenderer.Service
{
    public static class WaveObjHelper
    {
        public static Mesh ReadMeshFromFile(string filePath)
        {
            Mesh result = new Mesh();
            string[] lines = File.ReadAllLines(filePath);

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

                    Vertex vertex = new Vertex (position);
                    result.Vertices.Add(vertex);
                }
                if (lineParts[0] == "f")
                {
                    int a = int.Parse(lineParts[1].Split('/')[0]);
                    int b = int.Parse(lineParts[2].Split('/')[0]);
                    int c = int.Parse(lineParts[3].Split('/')[0]);

                    result.Triangles.Add(new Triangle(a, b, c));
                }
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
