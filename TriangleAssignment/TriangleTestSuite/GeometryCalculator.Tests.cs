using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Drawing;

namespace GeometricalObjects
{
    [TestFixture]
    public class GeometryCalculatorTestSuite
    {
        [Test]
        [TestCase(3, 4, 3, 6, 2)]
        [TestCase(8, 4, 3, 4, 5)]
        [TestCase(3, 4, 3, 4, 0)]
        [TestCase(3, 4, 7, 7, 5)]
        public void TestCalculator_TwoPointsOnThePlane_DistanceBetweenPoints(int vertex1x, int vertex1y, int vertex2x, int vertex2y, double expectedResult)
        {
            var vertex1 = new Point(vertex1x, vertex1y);
            var vertex2 = new Point(vertex2x, vertex2y);

            var result = new GeometryCalculator().GetSide(vertex1,vertex2);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase(3, 4, 5, 6)]
        public void CalculateArea_ThreeSides_TriangleArea(int sideOne, int sideTwo, int sideThree, double expectedResult)
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
            TestInternalAngle(new Point(0, 0), new Point(5, 0), new Point(0, 5), 90d);
            TestInternalAngle(new Point(5, 0), new Point(0, 5), new Point(0, 0), 45d);
            TestInternalAngle(new Point(0, 0), new Point(3, 0), new Point(0, 4), 90d);
            
        }


        private void TestInternalAngle(Point vertexOfInternalAngle, Point vertexTwo, Point vertexThree, double expectedResult)
        {
            var result = new GeometryCalculator().GetInternalAngle(vertexOfInternalAngle, vertexTwo, vertexThree);
            Assert.AreEqual(expectedResult, result);
        }

    }
}
