﻿using System.Drawing;
using System;
using ElRenderer.Algebraic;
using ElRenderer.Model;

namespace ElRenderer
{
    public static class BitmapExtensions
    {
        private static int _int(float f)
        {
            return (int)(f + 0.5f);
        }

        private static void swap<T>(ref T a, ref T b)
        {
            T buffer = a;
            a = b;
            b = buffer;
        }

        public static void elDrawTriangle(this Bitmap b, Float3 v1, Float3 v2, Float3 v3, Color color)
        {
            elDrawTriangle(b, v1.xy, v2.xy, v3.xy, color);
        }
        public static void elDrawTriangle(this Bitmap b, Float2 v1, Float2 v2, Float2 v3, Color color)
        {
            Box2d bb = Utils.getBoundingBox2d(v1, v2, v3).Value;

            for (int i = _int(bb.min.x); i < _int(bb.max.x); i++)
                for (int j = _int(bb.min.y); j < _int(bb.max.y); j++)
                {
                    if (Utils.getBarycentricCoordinates(v1, v2, v3, new Float2(i, j)).IsAllComponentsPositive())
                        b.elDrawPoint(i, j, color);
                }

            b.elDrawLine(v1, v2, color);
            b.elDrawLine(v2, v3, color);
            b.elDrawLine(v3, v1, color);
        }

        public static void elDrawLine(this Bitmap b, Float3 v0, Float3 v1, Color color)
        {
            b.elDrawLine(v0.x, v0.y, v1.x, v1.y, color);
        }
        public static void elDrawLine(this Bitmap b, Float2 v0, Float2 v1, Color color)
        {
            b.elDrawLine(v0.x, v0.y, v1.x, v1.y, color);
        }
        public static void elDrawLine(this Bitmap b, float xStart, float yStart, float xEnd, float yEnd, Color color)
        {
            elDrawLine(b, _int(xStart), _int(yStart), _int(xEnd), _int(yEnd), color);
        }
        public static void elDrawLine(this Bitmap bitmap, Int2 a, Int2 b, Color color)
        {
            elDrawLine(bitmap, a.x, a.y, b.x, b.y, color);
        }
        public static void elDrawLine(this Bitmap bitmap, int xStart, int yStart, int xEnd, int yEnd, Color color)
        {
            int y;
            float k;

            int x0 = xStart, x1 = xEnd, y0 = yStart, y1 = yEnd;


            // change cycle axis
            bool changeAxis = (Math.Abs(x0 - x1) < Math.Abs(y0 - y1));
            if (changeAxis)
            {
                swap(ref x0, ref y0);
                swap(ref x1, ref y1);
            }

            // change direction
            if (x0 > x1)
            {
                swap(ref x0, ref x1);
                swap(ref y0, ref y1);
            }


            if (x1 == x0)
                k = 0;
            else
                k = (float)(y1 - y0) / (float)(x1 - x0);
            
            for (int x = x0; x <= x1; x++) {
                y = (int)(k * (x - x0)) + y0;

                if(changeAxis)
                    bitmap.elDrawPoint(y, x, color);
                else
                    bitmap.elDrawPoint(x, y, color);
            }
        }
        
        public static void elDrawPoint(this Bitmap bitmap, int x, int y, Color color)
        {
            if (y > bitmap.Size.Height)
                return;
            if (x > bitmap.Size.Width)
                return;

            if (y <= 0)
                y = 1;
            if (x < 0)
                x = 0;
            if (x >= bitmap.Width)
                x = bitmap.Width - 1;

            bitmap.SetPixel(x, bitmap.Size.Height - y, color);
        }
    }
}
