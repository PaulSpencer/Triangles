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
            Expect.Call(mockGeometryCalculator.IsAStraightLine(dummyPointA, dummyPointB, dummyPointC)).IgnoreArguments().Return(false);
            mockRepository.ReplayAll();

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
            Expect.Call(mockGeometryCalculator.IsAStraightLine(dummyPointA, dummyPointB, dummyPointC)).IgnoreArguments().Return(true);
            mockRepository.ReplayAll();
            var result = new Triangle(dummyPointA, dummyPointB, dummyPointC, mockGeometryCalculator);
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
            Assert.IsFalse(result.IsScalene());
        }
        
        [Test]
        public void IsScalene_WhenIsEquilateral_ReturnsFalse()
        {
            var result = SetTriangleAngles(60d, 60d, 60d);
            Assert.IsFalse(result.IsScalene());
        }

        public Triangle SetTriangleAngles(double angleA, double angleB, double angleC)
        {
            var stubGeometryCalculator = mockRepository.DynamicMock<IGeometryCalculator>();

            Expect.Call(stubGeometryCalculator.IsAStraightLine(dummyPointA, dummyPointB, dummyPointC)).Return(false);
            Expect.Call(stubGeometryCalculator.CalculateInternalAngle(dummyPointA, dummyPointB, dummyPointC)).Return(angleA);
            Expect.Call(stubGeometryCalculator.CalculateInternalAngle(dummyPointB, dummyPointC, dummyPointA)).Return(angleB);
            Expect.Call(stubGeometryCalculator.CalculateInternalAngle(dummyPointC, dummyPointA, dummyPointB)).Return(angleC);
            mockRepository.ReplayAll();
            return new Triangle(dummyPointA, dummyPointB, dummyPointC, stubGeometryCalculator);
        }
    }


}
