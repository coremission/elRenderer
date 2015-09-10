using System.Drawing;
using System;

namespace ElRenderer
{
    public static class BitmapExtensions
    {
        public static void elDrawLine(this Bitmap bitmap, int xStart, int yStart, int xEnd, int yEnd, Color color)
        {
            int y = 0;
            float k;

            // change cycle axis
            bool changeAxis = (Math.Abs(xStart - xEnd) < Math.Abs(yStart - yEnd));

            int x0 = changeAxis ? yStart : xStart;
            int x1 = changeAxis ? yEnd : xEnd;
            int y0 = changeAxis ? xStart : yStart;
            int y1 = changeAxis ? xEnd : yEnd;

            // change direction
            if(xStart > xEnd)
            {
                x0 = xEnd;
                x1 = xStart;
                y0 = yEnd;
                y1 = yStart;
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
            bitmap.SetPixel(x, bitmap.Size.Height - y, color);
        }
    }
}
