using System.Drawing;
using static ElRenderer.Utils;

namespace ElRenderer.Algebraic
{
    public struct Float4
    {
        public float x;
        public float y;
        public float z;
        public float w;

        public float r { get { return Clamp(0f, 1f, x); } }
        public float g { get { return Clamp(0f, 1f, y); } }
        public float b { get { return Clamp(0f, 1f, z); } }

        public Float3 xyz
        {
            get { return new Float3(this.x, this.y, this.z); }
        }

        #region Constructors
        public Float4(Float3 v, float w)
        {
            this.x = v.x;
            this.y = v.y;
            this.z = v.z;
            this.w = w;
        }

        public Float4(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
        #endregion

        public float dot(Float4 b)
        {
            return this.x * b.x + this.y * b.y + this.z * b.z + this.w * b.w;
        }
    }
}
