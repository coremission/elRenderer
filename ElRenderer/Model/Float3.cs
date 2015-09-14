namespace ElRenderer.Model
{
    public struct Float3
    {
        public float x;
        public float y;
        public float z;

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

        public override string ToString()
        {
            return string.Format("({0}, {1}, {2})", x, y, z);
        }

        public float dot(Float3 other)
        {
            return this.x * other.x + this.y * other.y + this.z * other.z;
        }

        public Float3 mul(Float3x3 m)
        {
            Float3[] columns = m.Columns;
            return new Float3(this.dot(columns[0]), this.dot(columns[1]), this.dot(columns[2]));
        }

        public float length()
        {
            return this.dot(this);
        }

        public static Float3 getBarycentric(Float3 p, Float3 a, Float3 b, Float3 c)
        {
            return new Float3(0, 0, 0);
        }
    }
}