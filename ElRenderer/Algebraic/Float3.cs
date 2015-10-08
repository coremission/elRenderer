using System;
using System.Drawing;
using static ElRenderer.Utils;

namespace ElRenderer.Model
{
    public struct Float3
    {
        public float x;
        public float y;
        public float z;

        public float r { get { return Clamp(0f, 1f, x); } }
        public float g { get { return Clamp(0f, 1f, y); } }
        public float b { get { return Clamp(0f, 1f, z); } }

        public Float2 xy
        {
            get { return new Float2(x, y); }
        }

        public Float3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static Float3 identity
        {
            get
            {
                return new Float3(1, 1, 1);
            }
        }

        public static Float3 zero
        {
            get
            {
                return new Float3(0, 0, 0);
            }
        }

        public float[] ToFloatArray()
        {
            return new float[]{ x, y, z };
        }

        public bool IsAllComponentsPositive()
        {
            return this.x > 0 && this.y > 0 && this.z > 0;
        }

        public override string ToString()
        {
            return string.Format("({0}, {1}, {2})", x, y, z);
        }

        public static Float3 operator - (Float3 a, Float3 b)
        {
            return new Float3(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static Float3 operator + (Float3 a, Float3 b)
        {
            return new Float3(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static Float3 operator * (Float3 M, float scalar){
            return new Float3(M.x * scalar, M.y * scalar, M.z * scalar);
        }

        public static Float3 operator *(float scalar, Float3 M)
        {
            return M * scalar;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Float3))
                return false;

            Float3 b = (Float3)obj;

            return (float.Equals(this.x, b.x) &&
                    float.Equals(this.y, b.y) &&
                    float.Equals(this.z, b.z));
        }

        public static bool operator ==(Float3 a, Float3 b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Float3 a, Float3 b)
        {
            return !(a == b);
        }

        public float dot(Float3 other)
        {
            return this.x * other.x + this.y * other.y + this.z * other.z;
        }

        public Float3 cross(Float3 other)
        {
            //c  = a cross b
            //cx = aybz − azby
            //cy = azbx − axbz
            //cz = axby − aybx

            return new Float3(this.y * other.z - this.z * other.y,
                              this.z * other.x - this.x * other.z,
                              this.x * other.y - this.y * other.x);
        }

        public Float3 mul(Float3x3 m)
        {
            Float3[] columns = m.Columns;
            return new Float3(this.dot(columns[0]), this.dot(columns[1]), this.dot(columns[2]));
        }

        public float length()
        {
            return (float)Math.Sqrt(this.dot(this));
        }

        public Float3 normalize()
        {
            float l = length();
            return new Float3(this.x / l, this.y / l, this.z / l);
        }

        public Float3 getOpposite()
        {
            return new Float3(-x, -y, -z);
        }

        public static Float3 lerp(Float3 start, Float3 end, float delta)
        {
            return start + (end - start) * delta;
        }

        public Color ToColor()
        {
            return Color.FromArgb((int)(r * 255),
                                  (int)(g * 255),
                                  (int)(b * 255)
            );
        }
    }
}