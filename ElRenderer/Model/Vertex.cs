using System.Drawing;

namespace ElRenderer.Model
{
    public class Vertex
    {
        public Float3 position;

        public Float3 normal;

        public Color color;

        public Float2 uv;

        public Vertex(Float3 position)
        {
            this.position = position;
            this.normal = Float3.zero;
            this.color = Color.Black;
            this.uv = new Float2(0, 0);
        }

        public override string ToString()
        {
            return string.Format("pos: {0}, normal: {1}, uv: {2}, color: {3}", position, normal, uv, color);
        }
    }
}
