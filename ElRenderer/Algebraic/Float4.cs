using System.Drawing;
using static ElRenderer.Utils;

namespace ElRenderer.Model
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
    }
}
