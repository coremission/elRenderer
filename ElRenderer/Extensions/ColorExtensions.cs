using System.Drawing;

namespace ElRenderer.Extensions
{
    public static class ColorExtensions
    {
        public static Color lerpTo(this Color start, Color end, float delta)
        {
            return add(start, mul(sub(end, start), delta));
        }
        
        public static Color add(Color a, Color b)
        {
            return Color.FromArgb(Utils.Clamp(0, 255, a.R + b.R),
                                  Utils.Clamp(0, 255, a.G + b.G),
                                  Utils.Clamp(0, 255, a.B + b.B)
                                  );
        }

        public static Color sub(Color a, Color b)
        {
            return Color.FromArgb(Utils.Clamp(0, 255, a.R - b.R),
                                  Utils.Clamp(0, 255, a.G - b.G),
                                  Utils.Clamp(0, 255, a.B - b.B)
                                  );
        }

        public static Color mul(Color a, float s)
        {
            return Color.FromArgb(Utils.Clamp(0, 255, (int)(a.R * s + 0.5)),
                                  Utils.Clamp(0, 255, (int)(a.G * s + 0.5)),
                                  Utils.Clamp(0, 255, (int)(a.B * s + 0.5))
                                  );
        }
    }
}
