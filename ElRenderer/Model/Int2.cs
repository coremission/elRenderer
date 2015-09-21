using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElRenderer.Model
{
    public struct Int2
    {
        public int x;
        public int y;

        public float getLength()
        {
            return (float)Math.Sqrt(x * x + y * y);
        }

        public Int2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Int2 operator -(Int2 a, Int2 b)
        {
            return new Int2(a.x - b.x, a.y - b.y);
        }

        public static Int2 operator +(Int2 a, Int2 b)
        {
            return new Int2(a.x + b.x, a.y + b.y);
        }

        public override string ToString()
        {
            return string.Format("({0}, {1})", x, y);
        }

        public static explicit operator Int2(Float2 v)
        {
            return new Int2((int)(v.x + 0.5f), (int)(v.y + 0.5f));
        }
    }
}
