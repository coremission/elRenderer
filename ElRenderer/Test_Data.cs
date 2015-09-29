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
            result.Triangles = new List<Triangle>() { new Triangle(1, 2, 3, Color.Red) };

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
                new Triangle(1, 2, 3, Color.Red),
                new Triangle(4, 5, 6, Color.Green),
            };

            result.RenderType = MeshRenderType.ColoredTriangular;
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
                new Triangle(1, 2, 3, Color.Red),
                new Triangle(4, 5, 6, Color.Green),
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
                    new Triangle(1, 2, 3, Color.Red), new Triangle(1, 3, 4, Color.Red), // right
                    new Triangle(1, 5, 8, Color.Green), new Triangle(1, 4, 5, Color.Green), // front or back
                    new Triangle(4, 3, 6, Color.White), new Triangle(4, 6, 5, Color.FloralWhite),
                    new Triangle(2, 6, 3, Color.DarkViolet), new Triangle(2, 7, 6, Color.Violet), // front or back
                    new Triangle(1, 7, 2, Color.Blue), new Triangle(1, 8, 7, Color.BlueViolet),
                    new Triangle(8, 5, 7, Color.YellowGreen), new Triangle(5, 6, 7, Color.Yellow), // left
                };

            return result;
        }
    }
}
