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

        public static Box2d? getBoundingBox2d(params Float2[] points)
        {
            if (points.Length < 2)
                return null;

            Box2d result = new Box2d();

            float minX = float.PositiveInfinity;
            float minY = float.PositiveInfinity;

            float maxX = float.NegativeInfinity;
            float maxY = float.NegativeInfinity;

            for (int i = 0; i < points.Length; i++)
            {
                Float2 p = points[i];

                if (p.x < minX)
                    minX = p.x;
                if (p.y < minY)
                    minY = p.y;

                if (p.x > maxX)
                    maxX = p.x;
                if (p.y > maxY)
                    maxY = p.y;
            }

            result.min = new Float2(minX, minY);
            result.max = new Float2(maxX, maxY);

            return result;
        }

        public static Float3 getBarycentricCoordinates(Float2 t1, Float2 t2, Float2 t3, Float2 p)
        {
            float T = Utils.getTriangleArea(t1, t2, t3);
            float T1 = Utils.getTriangleArea(t2, t3, p);
            float T2 = Utils.getTriangleArea(t1, p, t3);
            float T3 = Utils.getTriangleArea(t1, t2, p);

            return new Float3(T1/T, T2/T, T3/T);
        }

        public static float getPerimeter(Float2 t1, Float2 t2, Float2 t3)
        {
            Float2[] vertices = new Float2[] { t1, t2, t3 };
            vertices.OrderBy(v => v.x);

            float a = (t2 - t1).getLength();
            float b = (t3 - t2).getLength();
            float c = (t1 - t3).getLength();

            return (a + b + c);
        }

        public static float getTriangleAreaHeron(Float2[] vertices)
        {
            return getTriangleAreaHeron(vertices[0], vertices[1], vertices[2]);
        }

        public static float getTriangleAreaHeron(Float2 t1, Float2 t2, Float2 t3)
        {
             return getPerimeter(t1, t2, t3) / 2.0f;
        }

        public static float getTriangleArea(Float2[] vertices)
        {
            return getTriangleArea(vertices[0], vertices[1], vertices[2]);
        }

        public static float getTriangleArea(Float2 t1, Float2 t2, Float2 t3)
        {
            // signed areas of trapezoids
            float a1 = (t1.y + t2.y) * (t2.x - t1.x) / 2.0f;
            float a2 = (t2.y + t3.y) * (t3.x - t2.x) / 2.0f;
            float a3 = (t1.y + t3.y) * (t1.x - t3.x) / 2.0f;

            return a1 + a2 + a3;
        }
    }
}
