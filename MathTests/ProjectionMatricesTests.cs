using ElRenderer.Algebraic;
using NUnit.Framework;

namespace MathTests
{
    [TestFixture]
    public class ProjectionMatricesTests
    {
        [Test]
        public void SimpleProjectionTest()
        {
            Float3 p = new Float3(2, 4, 2);

            const float n = 3f;
            const float f = 100f;
            const float b = -10;
            const float t = 10;
            const float r = 10;
            const float l = -10;

            Float4x4 Projection = Float4x4.getProjectionMatrix(n, f, t, b, r, l);

            Float3 p2 = Projection.transformPoint(new Float3(r, b, n));
            Float3 p1 = Projection.transformPoint(p);
            Assert.IsTrue(p1.x < 1f);
        }
    }
}