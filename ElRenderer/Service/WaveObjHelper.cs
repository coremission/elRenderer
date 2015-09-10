using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
                
                string[] lineParts = lines[i].Split(' ');

                if (lineParts[0] != "v")
                    continue;

                Vector3 v = new Vector3(float.Parse(lineParts[1]), float.Parse(lineParts[2]), float.Parse(lineParts[3]));

                result.Vertices.Add(v);
            }
            
            return result;
        }

        public static Mesh ReadMeshFromStream(Stream stream)
        {
            return new Mesh();
        }


    }
}
