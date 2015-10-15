using ElRenderer.Algebraic;
using ElRenderer.Model;
using System.Collections.Generic;
using System.Drawing;

namespace ElRenderer
{
    public static class Test_Data
    {
        public static Mesh getSingleTriangle()
        {
            Mesh result = new Mesh();

            result.Vertices = new List<Vertex>(){
                new Vertex(new Float3( 1,  0, 0)),
                new Vertex(new Float3(-1,  1, 0)),
                new Vertex(new Float3(-1, -1, 0)),
            };
            result.Triangles = new List<Triangle>() { new Triangle(1, 2, 3) };

            return result;
        }

        public static Mesh getOverlappedTriangles()
        {
            Mesh result = new Mesh();

            result.Vertices = new List<Vertex>(){
                new Vertex(new Float3( 2,  0, -1)),
                new Vertex(new Float3(-1, -1,  1)),
                new Vertex(new Float3(-1,  1,  1)),

                new Vertex(new Float3(-2,  0, -1)),
                new Vertex(new Float3( 1,  1,  1)),
                new Vertex(new Float3( 1, -1,  1)),
            };
            result.Triangles = new List<Triangle>() {
                new Triangle(1, 2, 3),
                new Triangle(4, 5, 6),
            };

            return result;
        }

        public static Mesh getOneAboveTheOverTriangle()
        {
            Mesh result = new Mesh();

            result.Vertices = new List<Vertex>(){
                new Vertex(new Float3( 2,  0,  -1)),
                new Vertex(new Float3(-1, -1,  -1)),
                new Vertex(new Float3(-1,  1,  -1)),
                new Vertex(new Float3( 2,  0,  0)),
                new Vertex(new Float3(-1, -1,  0)),
                new Vertex(new Float3(-1,  1,  0)),
            };
            result.Triangles = new List<Triangle>() {
                new Triangle(1, 2, 3),
                new Triangle(4, 5, 6),
            };

            return result;
        }

        public static Mesh getTestBox()
        {
            Mesh result = new Mesh();

            result.Vertices = new List<Vertex>(){
                    new Vertex(new Float3(1, -1, -1)),  // 1
                    new Vertex(new Float3(1, -1, 1)),   // 2
                    new Vertex(new Float3(1, 1, 1)),    // 3
                    new Vertex(new Float3(1, 1, -1)),   // 4
                    new Vertex(new Float3(-1, 1, -1)),  // 5
                    new Vertex(new Float3(-1, 1, 1)),   // 6
                    new Vertex(new Float3(-1, -1, 1)),  // 7
                    new Vertex(new Float3(-1, -1, -1))  // 8
                };
            result.Triangles = new List<Triangle>(){
                    new Triangle(1, 2, 3), new Triangle(1, 3, 4),
                    new Triangle(1, 5, 8), new Triangle(1, 4, 5),
                    new Triangle(4, 3, 6), new Triangle(4, 6, 5),
                    new Triangle(2, 6, 3), new Triangle(2, 7, 6),
                    new Triangle(1, 7, 2), new Triangle(1, 8, 7),
                    new Triangle(8, 5, 7), new Triangle(5, 6, 7),
                };

            return result;
        }
    }
}
