using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElRenderer.Algebraic;

namespace MathTests
{
    [TestClass]
    public class QuaternionsTestFixture
    {
        [TestMethod]
        public void AdditionTest()
        {
            Quaternion q = new Quaternion(0, 0, 0, 0);
            Assert.IsTrue(q + q == new Quaternion(0, 0, 0, 0), "Zero quaternion is identity");
        }
    }
}
