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

        public Float3 mul(Float3x3 m)
        {
            Float3[] columns = m.Columns;
            return new Float3(this.dot(columns[0]), this.dot(columns[1]), this.dot(columns[2]));
        }

        public float length()
        {
            return this.dot(this);
        }
    }
}