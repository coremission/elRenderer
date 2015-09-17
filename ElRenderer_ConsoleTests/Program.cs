using System;
using ElRenderer.Model;
using ElRenderer;

namespace ElRenderer_ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var t1 = new Float2(0, 0);
            var t2 = new Float2(0, 3);
            var t3 = new Float2(4, 0);
            var p = new Float2(1, 1);

            Float2[] triangle1 = new[] {t1, t2, t3};

            float T2 = Utils.getTriangleArea(t1, p, t3);
            float T = Utils.getTriangleArea(t1, t2, t3);
            float T1 = Utils.getTriangleArea(t2, t3, p);
            
            float T3 = Utils.getTriangleArea(t1, t2, p);

            Console.WriteLine(string.Format("T: {0}\nT1: {1}\nT2: {2}\nT3: {3}", T, T1, T2, T3));
            Float3 c = Utils.getBarycentricCoordinates(t1, t2, t3, p);
            Console.WriteLine(c);
            Console.WriteLine(c.x + c.y + c.z); 
            return;

            Float3 v = new Float3(1, 2, 3);

            Float3x3 M = new Float3x3(1, 0, 0,
                                      0, 1, 0,
                                      0, 0, 1);
            Float3x3 A = new Float3x3(2, 1, 0,
                                      3, 1, 0,
                                      1, 0, 1);
            Console.WriteLine("M: " + M);
            Console.WriteLine("A: " + A);
            Console.WriteLine("M*A: " + 2 * M * A);
        }
    }
}
