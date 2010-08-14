using NUnit.Framework;
using System;
using Rhino.Mocks;

namespace GeometricalObjects.Tests
{
    [TestFixture]
    public class TriangleTestSuite
    {
        MockRepository mockRepository;
        IGeometryCalculator stubGeometryCalculator;
        IGeometryCalculator mockGeometryCalculator;
        Point dummyPoint;
        Point dummyPointA;
        Point dummyPointB;
        Point dummyPointC;

        [SetUp]
        public void SetUp()
        {
            mockRepository = new MockRepository();

            mockGeometryCalculator = mockRepository.DynamicMock<IGeometryCalculator>();

            dummyPoint = new Point(1, 1);
            dummyPointA = new Point(1, 2);
            dummyPointB = new Point(2, 3);
            dummyPointC = new Point(3, 4);
        }

        [Test]
        public void Constructor_ThreePointsOnThePlane_ReturnsTriangle()
        {
            Expect.Call(mockGeometryCalculator.IsAStraightLine(dummyPointA, dummyPointB, dummyPointC)).IgnoreArguments().Return(false);
            mockRepository.ReplayAll();

            var result = GeometryShapeFactory.CreateTriangle(dummyPointA, dummyPointB, dummyPointC, mockGeometryCalculator);
            
            Assert.NotNull(result);

            Assert.AreEqual(dummyPointA, result.Vertex1);
            Assert.AreEqual(dummyPointB, result.Vertex2);
            Assert.AreEqual(dummyPointC, result.Vertex3);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Can't have 3 points on same line")]
        public void Constructor_InAStraightLine_ThrowsArgumentException()
        {
            Expect.Call(mockGeometryCalculator.IsAStraightLine(dummyPointA, dummyPointB, dummyPointC)).IgnoreArguments().Return(true);
            mockRepository.ReplayAll();

            var result = GeometryShapeFactory.CreateTriangle(dummyPointA, dummyPointB, dummyPointC, mockGeometryCalculator);
        }


        [Test]
        public void IsEquilateral_WhenAllAnglesEqual_ReturnsTrue()
        {
            var result = SetTriangleAngles(60d, 60d, 60d);

            Assert.IsTrue(result.IsEquilateral());
        }

        [Test]
        public void IsEquilateral_WhenNotAllAnglesEqual_ReturnsFalse()
        {
            var result = SetTriangleAngles(60d, 70d, 50d);
            Assert.IsFalse(result.IsEquilateral());
        }

        [Test]
        public void IsIsocolese_WhenAllAnglesEqual_ReturnsFalse()
        {
            var result = SetTriangleAngles(60d, 60d, 60d);
            Assert.IsFalse(result.IsIsosceles());
        }

        [Test]
        public void IsIsocolese_WhenTwoAnglesEqualButNotThree_ReturnsTrue()
        {
            var result = SetTriangleAngles(40d, 70d, 70d);
            Assert.IsTrue(result.IsIsosceles());
        }

        [Test]
        public void IsIsocolese_WhenNoSidesEqual_ReturnsFalse()
        {
            var result = SetTriangleAngles(40d, 60d, 80d);
            Assert.IsFalse(result.IsIsosceles());
        }

        [Test]
        public void IsScalene_WhenNoSidesEqual_ReturnsTrue()
        {
            var result = SetTriangleAngles(40d, 60d, 80d);
            Assert.IsTrue(result.IsScalene());
        }

        [Test]
        public void IsScalene_WhenIsIscocolese_ReturnsFalse()
        {
            var result = SetTriangleAngles(40d, 70d, 70d);
            Assert.AreEqual(result.IsScalene(), false);
        }

        [Test]
        public void IsScalene_WhenIsEquilateral_ReturnsFalse()
        {
            var result = SetTriangleAngles(60d, 60d, 60d);

            Assert.IsFalse(result.IsScalene());
        }

        ////[Test]
        ////[ExpectedException(typeof(ArgumentException), ExpectedMessage = "All internal angles must add up to 180 degrees")]
        ////public void Constructor_InternalAnglesNotEqualTo180Degrees_ThrowException()
        ////{
        ////    var result = SetTriangleAngles(100d, 100d, 100d);
        ////}

        public Triangle SetTriangleAngles(double angleA, double angleB, double angleC)
        {
            Expect.Call(mockGeometryCalculator.IsAStraightLine(dummyPointA, dummyPointB, dummyPointC)).Return(false);
            Expect.Call(mockGeometryCalculator.GetInternalAngle(dummyPointA, dummyPointB, dummyPointC)).Return(angleA);
            Expect.Call(mockGeometryCalculator.GetInternalAngle(dummyPointB, dummyPointA, dummyPointC)).Return(angleB);
            Expect.Call(mockGeometryCalculator.GetInternalAngle(dummyPointC, dummyPointA, dummyPointB)).Return(angleC);
            
            mockRepository.ReplayAll();

            var triangle = GeometryShapeFactory.CreateTriangle(dummyPointA, dummyPointB, dummyPointC, mockGeometryCalculator);

            Assert.NotNull(triangle);

            return triangle;
        }
    }


}
