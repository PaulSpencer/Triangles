using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace GeometricalObjects
{
    [TestFixture]
    public class GeometryCalculatorTestSuite
    {
        [Test]
        [TestCase(3.0, 4.0, 3.0, 6.0, 2.0)]
        [TestCase(8.0, 4.0, 3.0, 4.0, 5.0)]
        [TestCase(3.0, 4.0, 3.0, 4.0, 0.0)]
        [TestCase(3.0, 4.0, 7.0, 7.0, 5.0)]
        public void TestCalculator_TwoPointsOnThePlane_DistanceBetweenPoints(double vertex1x, double vertex1y, double vertex2x, double vertex2y, double expectedResult)
        {
            var vertex1 = new Point(vertex1x, vertex1y);
            var vertex2 = new Point(vertex2x, vertex2y);

            var result = new GeometryCalculator().GetSide(vertex1,vertex2);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase(3.0, 4.0, 5.0, 6.0)]
        public void CalculateArea_ThreeSides_TriangleArea(double sideOne, double sideTwo, double sideThree, double expectedResult)
        {
            var result = new GeometryCalculator().CalculateArea(sideOne,sideTwo,sideThree);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase(12d, 6d, 4d)]
        [TestCase(48d, 12d, 8d)]
        public void GetHeight_GiveAreaAndBase_ReturnsHeight(double area, double baseSide, double expectedResult)
        {
            var result = new GeometryCalculator().GetHeight(area, baseSide);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Base side cannot be of lenght 0!")]
        public void GetHeight_GiveZeroBaseSide_ThrowsArgumentException()
        {
            var result = new GeometryCalculator().GetHeight(3.0, 0.0);
        }

        [Test]
        public void GetInternalAngle_PassTriangleVertices_ReturnInternalAngle()
        {
            TestInternalAngle(new Point(0d, 0d), new Point(3d, 0d), new Point(0d, 4d), 90d);
            TestInternalAngle(new Point(5d,0d), new Point(0d,5d),new Point(0d,0d), 45d);
        }


        private void TestInternalAngle(Point vertexOfInternalAngle, Point vertexTwo, Point vertexThree, double expectedResult)
        {
            var result = new GeometryCalculator().GetInternalAngle(vertexOfInternalAngle, vertexTwo, vertexThree);
            Assert.AreEqual(expectedResult, result);
        }

    }
}
