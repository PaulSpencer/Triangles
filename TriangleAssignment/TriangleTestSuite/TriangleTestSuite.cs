using NUnit.Framework;
using System;

namespace GeometricalObjects.Tests
{
    [TestFixture]
    public class TriangleTestSuite
    {
        [Test]
        public void Constructor_ThreePointsOnThePlane_ReturnsTriangle()
        {
            var a = new Point(2, 3);
            var b = new Point(2, 5);
            var c = new Point(3, 6);

            var result = new Triangle(a,b,c);

            Assert.AreEqual(a, result.A);
            Assert.AreEqual(b, result.B);
            Assert.AreEqual(c, result.C);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException),ExpectedMessage="Can't have 3 points on same line")]
        public void Constructor_SameXAxis_ThrowsArgumentException()
        {
            var a = new Point(2, 3);
            var b = new Point(2, 5);
            var c = new Point(2, 6);

            var result = new Triangle(a, b, c);
        }
    }
}
