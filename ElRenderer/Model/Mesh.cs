using System;
using System.Collections.Generic;

namespace ElRenderer.Model
{
    public class Mesh
    {
        public List<Float3> Vertices;
        public List<Triangle> Triangles;
 
        public Mesh()
        {
            Vertices = new List<Float3>();
            Triangles = new List<Triangle>();
        }
    }
}
