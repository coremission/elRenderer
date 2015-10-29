using ElRenderer.Algebraic;

namespace ElRenderer.Model
{
    public class SceneObject
    {
        public Mesh mesh;
        public Material material;
        public float uniformScale = 1;
        public Algebraic.Float3 rotation;
        public Float3 localPosition;
    }
}
