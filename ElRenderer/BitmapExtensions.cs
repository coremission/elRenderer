using System.Drawing;
using System;

namespace ElRenderer
{
    public static class BitmapExtensions
    {
        public static void elDrawLine(this Bitmap bitmap, int xStart, int yStart, int xEnd, int yEnd) {

            float slopeCoefficient = ((float)(yEnd - yStart)) / ((float)(xEnd - xStart));
            float error = 0f;
            int y = yStart;

            for (int x = xStart; x <= xEnd; x++) {
                error += slopeCoefficient;
                if (error > 0.5f) {
                    y++;
                    error--;
                }

                bitmap.SetPixel(x, y, Color.White);
            }
        }

    }
}
