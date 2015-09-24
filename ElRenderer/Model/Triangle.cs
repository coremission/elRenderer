using System.Drawing;

namespace ElRenderer.Model
{
    public struct Triangle
    {
        private int[] _raw;
        public Color color;

        public int this[int i]
        {
            get { return _raw[i]; }
        }
        
        public Triangle(int a, int b, int c)
        {
            _raw = new[] { a, b, c };
            this.color = Color.White;
        }

        public Triangle(int a, int b, int c, Color color)
        {
            _raw = new[] {a, b, c};
            this.color = color;
        }
    }
}