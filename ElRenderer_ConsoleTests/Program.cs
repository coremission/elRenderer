using System;
using ElRenderer.Model;

namespace ElRenderer_ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
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
