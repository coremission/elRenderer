using System;

namespace ElRenderer.Model
{
    public struct Float3
    {
        public float x;
        public float y;
        public float z;

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

        public float dot(Float3 other)
        {
            return this.x * other.x + this.y * other.y + this.z * other.z;
        }

        public static Float3 operator -(Float3 a, Float3 b)
        {
            return new Float3(a.x - b.x,
                              a.y - b.y,
                              a.z - b.z);
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
    }
}