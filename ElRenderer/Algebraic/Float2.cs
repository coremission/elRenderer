using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElRenderer.Model
{
    public struct Float2
    {
        public float x;
        public float y;

        public float getLength()
        {
            return (float)Math.Sqrt(x * x + y * y);
        }

        public Float2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public static Float2 operator -(Float2 a, Float2 b)
        {
            return new Float2(a.x - b.x, a.y - b.y);
        }

        public static Float2 operator +(Float2 a, Float2 b)
        {
            return new Float2(a.x + b.x, a.y + b.y);
        }

        public override string ToString()
        {
            return string.Format("({0}, {1})", x, y);
        }
    }
}
