using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElRenderer.Algebraic;

namespace MathTests
{
    [TestClass]
    public class TranslationMatricesTests
    {
        [TestMethod]
        public void NoTransformTest()
        {
            Float3 p = new Float3(1, 3, 7);
            Float4x4 M = Float4x4.identity;

            Assert.AreEqual(p, M.transformPoint(p));
        }

        [TestMethod]
        public void MultiplicationTest()
        {
            Float4x4 M = Float4x4.identity;
            Float4x4 B = M * 3;
            Float4x4 C = B * (1f / 3f);

            Assert.AreEqual(M, C);
        }

        [TestMethod]
        public void SimpleTranslationTest()
        {
            Float3 p = new Float3(1, 3, 7);
            Float3 t = new Float3(1, 2, 3);

            Float4x4 M = new Float4x4(1  , 0  , 0  , 0,
                                      0  , 1  , 0  , 0,
                                      0  , 0  , 1  , 0,
                                      t.x, t.y, t.z, 1);

            Float4x4 M2 = Float4x4.getTranslationMatrix(t);

            Assert.AreEqual(M, M2);
            Assert.AreEqual(p + t, M.transformPoint(p));
        }

        [TestMethod]
        public void AddTranslationToExistingMatrixTests()
        {
            Float3 p = new Float3(0, 0, 1);
            Float3 t = new Float3(1, 3, 323);

            Float4x4 X = Float4x4.identity;
            X.setTranslation(t);
            Float4x4 minusX = Float4x4.identity;
            minusX.setTranslation(t.getOpposite());

            Assert.AreEqual(p + t, X.transformPoint(p));
            Assert.AreEqual(p, minusX.transformPoint(X.transformPoint(p)));
        }

        [TestMethod]
        public void SimpleProjectionTest()
        {
            Float3 p = new Float3(2, 4, 2);

            Float4x4 Projection = Float4x4.getProjectionMatrix(2);

            Float3 p1 = Projection.transformPoint(p);
        }
    }
}
