namespace ElRenderer.Model
{
    public struct Vector3
    {
        public float x;
        public float y;
        public float z;

        public Vector3(float x, float y, float z)
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

        public float dot(Vector3 other)
        {
            return this.x * other.x + this.y * other.y + this.z * other.z;
        }

        public float length()
        {
            return this.dot(this);
        }
    }
}