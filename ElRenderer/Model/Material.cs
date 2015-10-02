using System.Drawing;

namespace ElRenderer.Model
{
    public class Material
    {
        public readonly Bitmap diffuseTexture;
        public readonly RenderType renderType;

        public Material(Bitmap diffuseTexture, RenderType renderType)
        {
            this.diffuseTexture = diffuseTexture;
            this.renderType = renderType;
        }
    }
}
