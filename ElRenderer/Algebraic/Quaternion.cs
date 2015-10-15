using static ElRenderer.Algebraic.elMath;

namespace ElRenderer.Algebraic
{
    public class Quaternion
    {
        public float w;

        private float[] _v = new float[3];

        public float x;
        public float y;
        public float z;

        public Float3 v { get { return new Float3(x, y, z); } }

        public Quaternion(float w, float x, float y, float z)
        {
            this.w = w;
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Quaternion(float w, Float3 v)
        {
            this.w = w;
            this.x = v.x;
            this.y = v.y;
            this.z = v.z;
        }

        public static Quaternion operator +(Quaternion a, Quaternion b)
        {
            return new Quaternion(a.w + b.w, a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static Quaternion operator -(Quaternion a, Quaternion b)
        {
            return new Quaternion(a.w - b.w, a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static bool operator !=(Quaternion a, Quaternion b)
        {
            return !(a == b);
        }

        public static bool operator ==(Quaternion a, Quaternion b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Multiplication: ab = [a.w * b.w - a.v · b.v, a.v cross b.v + a.w * b.v + b.w * a.v] (· is vector dot product and cross is vector cross product);
        /// </summary>
        /// <param name="a">Quaternion</param>
        /// <param name="b">Quaternion</param>
        /// <returns></returns>
        public static Quaternion operator *(Quaternion a, Quaternion b)
        {
            return new Quaternion(a.w * b.w - a.v.dot(b.v),             // w part
                                  a.v.cross(b.v) + a.w * b.v + b.w * a.v// v part
                                  );
        }

        public Quaternion Conjugate()
        {
            return new Quaternion(w, v.getOpposite());
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Quaternion))
                return false;
            Quaternion b = (Quaternion)obj;

            return (NearlyEqual(this.w, b.w) && NearlyEqual(this.x, b.x) && NearlyEqual(this.y, b.y) && NearlyEqual(this.z, b.z));
        }

        public override string ToString()
        {
            return string.Format("[{0}, ({1}, {2}, {3})]", w, x, y, z);
        }

        public float Norm()
        {
            return w * w + x * x + y * y + z * z;
        }
    }
}
