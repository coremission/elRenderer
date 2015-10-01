using System;

namespace ElRenderer.Model
{
    public struct Int3
    {
        public int x;
        public int y;
        public int z;

        public float getLength()
        {
            return (float)Math.Sqrt(x * x + y * y + z * z);
        }

        public Int3(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static Int3 operator -(Int3 a, Int3 b)
        {
            return new Int3(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static Int3 operator +(Int3 a, Int3 b)
        {
            return new Int3(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static bool operator ==(Int3 a, Int3 b)
        {
            return (a.x == b.x) && (a.y == b.y) && (a.z == b.z);
        }

        public static bool operator !=(Int3 a, Int3 b)
        {
            return !(a == b);
        }

        public static Int3 operator *(Int3 a, float t)
        {
            return new Int3((int)(t * a.x + 0.5f),
                            (int)(t * a.y + 0.5f),
                            (int)(t * a.z + 0.5f));
        }

        public override string ToString()
        {
            return string.Format("({0}, {1}, {2})", x, y, z);
        }

        public static explicit operator Int3(Float3 v)
        {
            return new Int3((int)(v.x + 0.5f), (int)(v.y + 0.5f), (int)(v.z + 0.5f));
        }

        public static Int3 lerp(Int3 start, Int3 end, float delta)
        {
            return start + (end - start) * delta;
        }
    }
}
