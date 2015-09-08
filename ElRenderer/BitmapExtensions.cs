using System.Drawing;

namespace ElRenderer
{
    public static class BitmapExtensions
    {
        public static void elDrawLine(this Bitmap bitmap, int xStart, int yStart, int xEnd, int yEnd) {
            bitmap.SetPixel(xStart, yStart, Color.White);
            bitmap.SetPixel(xEnd, yEnd, Color.White);

            for (int i = 300; i < 500; i++)
            {
                bitmap.SetPixel(100, i, Color.White);
            }
        }

    }
}
