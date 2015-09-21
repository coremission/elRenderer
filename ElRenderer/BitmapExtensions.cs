using System.Drawing;
using System;
using ElRenderer.Model;

namespace ElRenderer
{
    public static class BitmapExtensions
    {
        private static int _int(float f) { return (int)(f + 0.5f); }

        private static int lerp(int start, int end, float delta)
        {
            return (int)(start + (end - start) * delta + 0.5f);
        }

        private static T min<T>(T a, T b) where T: IComparable<T>
        {
            return a.CompareTo(b) <= 0 ? a : b;
        }

        private static T max<T>(T a, T b) where T : IComparable<T>
        {
            return a.CompareTo(b) >= 0 ? a : b;
        }

        public static void elDrawTriangle2(this Bitmap b, Float2 v1, Float2 v2, Float2 v3, Color color)
        {
            b.elDrawTriangle2((Int2)v1, (Int2)v2, (Int2)v3, color);
        }

        public static void elDrawTriangle2(this Bitmap b, Int2 v1, Int2 v2, Int2 v3, Color color)
        {
            if (v1.y > v2.y) swap(ref v1, ref v2);
            if (v1.y > v3.y) swap(ref v1, ref v3);
            if (v2.y > v3.y) swap(ref v2, ref v3);

            b.elDrawLine(v1, v2, color);
            b.elDrawLine(v2, v3, color);
            b.elDrawLine(v3, v1, color);
            
            /*
            int total_height = t2.y - t0.y;
            for (int y = t0.y; y <= t1.y; y++)
            {
                int segment_height = t1.y - t0.y + 1;
                float alpha = (float)(y - t0.y) / total_height;
                float beta = (float)(y - t0.y) / segment_height; // be careful with divisions by zero
                Vec2i A = t0 + (t2 - t0) * alpha;
                Vec2i B = t0 + (t1 - t0) * beta;
                image.set(A.x, y, red);
                image.set(B.x, y, green);
            }*/

            int triangleYHeight = v3.y - v1.y + 1;
            int segmentHeight = v2.y - v1.y + 1;

            for (int y = v1.y; y <= v2.y; y++)
            {
                float alpha = (float)(y - v1.y) / (float)triangleYHeight;
                float beta  = (float)(y - v1.y) / (float)segmentHeight;

                int ax = lerp(v1.x, v3.x, alpha);
                int bx = lerp(v1.x, v2.x, beta);

                for (int x = min(ax, bx); x <= max(ax, bx); x++)
                    b.elDrawPoint(x, y, color);
            }
            segmentHeight = v3.y - v2.y + 1;
            for (int y = v2.y; y <= v3.y; y++)
            {
                float alpha = (float)(y - v1.y) / (float) triangleYHeight;
                float beta = (float)(y - v2.y) / (float) segmentHeight;

                int ax = lerp(v1.x, v3.x, alpha);
                int bx = lerp(v2.x, v3.x, beta);

                for (int x = min(ax, bx); x <= max(ax, bx); x++)
                    b.elDrawPoint(x, y, color);
            }
            /*
            for (int y=t1.y; y<=t2.y; y++) {
                int segment_height =  t2.y-t1.y+1;
                float alpha = (float)(y-t0.y)/total_height;
                float beta  = (float)(y-t1.y)/segment_height; // be careful with divisions by zero
                Vec2i A = t0 + (t2-t0)*alpha;
                Vec2i B = t1 + (t2-t1)*beta;
                if (A.x>B.x) std::swap(A, B);
                for (int j=A.x; j<=B.x; j++) {
                    image.set(j, y, color); // attention, due to int casts t0.y+i != A.y
                }
            }*/     
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

        private static void swap<T>(ref T a, ref T b)
        {
            T buffer = a;
            a = b;
            b = buffer;
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
