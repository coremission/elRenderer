using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElRenderer.Model;

namespace ElRenderer
{
    public static class Utils
    {
        public static Box3d? getBoundingBox(params Float3[] points)
        {
            if (points.Length < 2)
                return null;

            Box3d result = new Box3d();

            float minX = float.PositiveInfinity;
            float minY = float.PositiveInfinity;
            float minZ = float.PositiveInfinity;

            float maxX = float.NegativeInfinity;
            float maxY = float.NegativeInfinity;
            float maxZ = float.NegativeInfinity;

            for (int i = 0; i < points.Length; i++)
            {
                Float3 p = points[i];

                if (p.x < minX)
                    minX = p.x;
                if (p.y < minY)
                    minY = p.y;
                if (p.z < minZ)
                    minZ = p.z;

                if (p.x > maxX)
                    maxX = p.x;
                if (p.y > maxY)
                    maxY = p.y;
                if (p.z > maxZ)
                    maxZ = p.z;
            }

            result.min = new Float3(minX, minY, minZ);
            result.max = new Float3(maxX, maxY, maxZ);

            return result;
        }

        public static Float3 getBarycentricCoordinates(Float3 t1, Float3 t2, Float3 t3, Float3 p)
        {
            return new Float3(0, 0, 0);
        }
    }
}
