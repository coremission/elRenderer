
namespace ElRenderer.Algebraic
{
    public class Quaternion
    {
        public float w;
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

        public static Quaternion operator +(Quaternion a, Quaternion b)
        {
            return new Quaternion(a.w + b.w, a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static bool operator !=(Quaternion a, Quaternion b)
        {
            return !(a == b);
        }

        public static bool operator ==(Quaternion a, Quaternion b)
        {
            return (a.w == b.w && a.x == b.x && a.y == b.y && a.z == b.z);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
}
