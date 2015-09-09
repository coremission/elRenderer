using System.Drawing;
using System;

namespace ElRenderer
{
    public static class BitmapExtensions
    {
        public static void elDrawLine(this Bitmap bitmap, int xStart, int yStart, int xEnd, int yEnd, Color color)
        {
            bool changeIterationAxis = Math.Abs(yStart - yEnd) > Math.Abs(xStart - xEnd);

            float slopeCoefficient = ((float)(yEnd - yStart)) / ((float)(xEnd - xStart));

            float error = 0f;
            
            int j = changeIterationAxis ? yStart : xStart;
            int iStart = changeIterationAxis ? yStart : xStart;
            int iEnd = changeIterationAxis ? yEnd : xEnd;

            for (int i = iStart; i <= iEnd; i++) {
                error += slopeCoefficient;
                if (error > 0.5f) {
                    j++;
                    error--;
                }

                if(changeIterationAxis)
                    bitmap.SetPixel(j, i, color);
                else
                    bitmap.SetPixel(i, j, color);
            }
        }

    }
}
