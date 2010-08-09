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
        Point dummyPoint;
        Point dummyPointA;
        Point dummyPointB;
        Point dummyPointC;

        [SetUp]
        public void SetUp()
        {
            mockRepository = new MockRepository();
            stubGeometryCalculator = MockRepository.GenerateStub<IGeometryCalculator>();
            dummyPoint = new Point(1, 1);
            dummyPointA = new Point(1, 2);
            dummyPointB = new Point(2, 3);
            dummyPointC = new Point(3, 4);
        }

        [Test]
        public void Constructor_ThreePointsOnThePlane_ReturnsTriangle()
        {
            var mockGeometryCalculator = mockRepository.DynamicMock<IGeometryCalculator>();
            Expect.Call(mockGeometryCalculator.IsAStraightLine(dummyPoint, dummyPoint, dummyPoint)).IgnoreArguments().Return(false);
            
            var result = new Triangle(dummyPointA, dummyPointB, dummyPointC, mockGeometryCalculator);

            Assert.AreEqual(dummyPointA, result.A);
            Assert.AreEqual(dummyPointB, result.B);
            Assert.AreEqual(dummyPointC, result.C);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException),ExpectedMessage="Can't have 3 points on same line")]
        public void Constructor_InAStraightLine_ThrowsArgumentException()
        {
            var mockGeometryCalculator = mockRepository.DynamicMock<IGeometryCalculator>();
            Expect.Call(mockGeometryCalculator.IsAStraightLine(dummyPoint, dummyPoint, dummyPoint)).IgnoreArguments().Return(true);
            var result = new Triangle(dummyPoint, dummyPoint, dummyPoint, mockGeometryCalculator);
        }


        //[Test]
        //public void IsEquilateral_WhenAllAnglesEqual_ReturnsTrue()
        //{
        //    var mockGeometryCalculator = mockRepository.DynamicMock<IGeometryCalculator>();
        //    Expect.Call(mockGeometryCalculator.IsAStraightLine(dummyPoint, dummyPoint, dummyPoint)).IgnoreArguments().Return(true);
        
        //    var result
        //}
    }
}
