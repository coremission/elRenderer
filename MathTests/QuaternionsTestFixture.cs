using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElRenderer.Algebraic;

namespace MathTests
{
    [TestClass]
    public class QuaternionsTestFixture
    {
        private Quaternion getRandomQ()
        {
            var r = new System.Random();
            return new Quaternion(r.Next(-101, 100), r.Next(-555, 100), r.Next(-100, 100), r.Next(-100, 100));
        }

        [TestMethod]
        public void AdditionTest()
        {
            Quaternion q = new Quaternion(0, 0, 0, 0);
            Assert.IsTrue(q + q == new Quaternion(0, 0, 0, 0));
        }

        [TestMethod]
        [TestCategory("Multiplication")]
        public void MultiplicationAssotiativityTest()
        {
            Quaternion q1 = getRandomQ();
            Quaternion q2 = getRandomQ();

            Assert.AreNotEqual(q1 * q2, q2 * q1);
        }

        [TestMethod]
        [TestCategory("Multiplication")]
        public void MultiplicationAntiAssotiativityTest()
        {
            Quaternion q1 = getRandomQ();
            Quaternion q2 = getRandomQ();

            Assert.AreEqual(q1 * q2, (q2 * q1));
        }

        [TestMethod]
        [TestCategory("Norm")]
        public void NormOfQuaternionAndItsConjugate()
        {
            Quaternion q = getRandomQ();

            Assert.AreEqual(q.Norm(), q.Conjugate().Norm());
        }

        [TestMethod]
        [TestCategory("Norm")]
        public void NormOfQuaternion_ByMultiplyOnItsConjugate()
        {
            Quaternion q = getRandomQ();

            Assert.AreEqual(q.Norm(), (q * q.Conjugate()).w);
        }

        [TestMethod]
        [TestCategory("Norm")]
        public void NormOfMultiplication_MultipliedNorms()
        {
            Quaternion q1 = getRandomQ();
            Quaternion q2 = getRandomQ();

            Assert.AreEqual((q1 * q2).Norm(), q1.Norm() * q2.Norm());
        }

        [TestMethod]
        public void ConjugateOfAddition_AdditionOfConjugates()
        {
            Quaternion q1 = getRandomQ();
            Quaternion q2 = getRandomQ();

            Assert.AreEqual((q1 + q2).Conjugate(), q1.Conjugate() + q2.Conjugate());
        }

        [TestMethod]
        public void ConjugateOfSubtraction_SubtractionOfConjugates()
        {
            Quaternion q1 = getRandomQ();
            Quaternion q2 = getRandomQ();

            Assert.AreEqual((q1 - q2).Conjugate(), q1.Conjugate() - q2.Conjugate());
        }

        public void ConjugateOfMultiplication_MultiplicationOfConjugates()
        {
            Quaternion q1 = getRandomQ();
            Quaternion q2 = getRandomQ();

            Assert.AreEqual((q1 * q2).Conjugate(), q1.Conjugate() * q2.Conjugate());
        }

    }
}
