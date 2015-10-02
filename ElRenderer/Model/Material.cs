using System.Drawing;

namespace ElRenderer.Model
{
    public class Material
    {
        public Bitmap diffuseTexture;
        public readonly RenderType renderType = RenderType.Regular;

        public Material(Bitmap diffuseTexture)
        {
            this.diffuseTexture = diffuseTexture;
        }
    }
}
