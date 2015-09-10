using System;
using System.Collections.Generic;
using System.Text;
using ElRenderer.Model;

namespace ElRenderer_ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Vector3 v = new Vector3(1, 2, 3);

            Matrix3x3 M = new Matrix3x3(1, 0, 0,
                                        0, 1, 0,
                                        0, 0, 1);

            Console.WriteLine(v);
            Console.WriteLine(M);
            Console.WriteLine(M.transpose());

            Console.WriteLine(M.transpose().mul(v));
        }
    }
}
