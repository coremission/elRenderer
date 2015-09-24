using System.Drawing;

namespace ElRenderer.Model
{
    public struct Fragment
    {
        public float z;
        public Color color;

        public Fragment(Color color)
        {
            this.z = float.PositiveInfinity;
            this.color = color;
        }

        public override string ToString()
        {
            return string.Format("(z: {0}, c: {1}", z, color);
        }
    }
}
