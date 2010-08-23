using NUnit.Framework;

using System;
using System.Drawing;
using GeometricalObjects;

namespace GeometricalObjects.Tests
{
    [TestFixture]
    public class TriangleTestSuite
    {
        #region Unit-tests: Ctor with Points

        //[Test]
        //[TestCase(3, 0, 0, 3)]
        //public void Constructor_ThreePointsOnThePlane_ReturnsTriangle(int point1x, int point1y, int point2x, int point2y)
        //{
        //    var origin = new Point(0, 0);
        //    var point1 = new Point(point1x, point1y);
        //    var point2 = new Point(point2x, point2y);

        //    new Triangle(origin, point1, point2);
        //}

        //[Test]
        //[TestCase(3, 3, 6, 6)]
        //[ExpectedException(typeof(ArgumentException), ExpectedMessage = "Can't have 3 points on same line")]
        //public void Constructor_InAStraightLine_ThrowsArgumentException(int point1x, int point1y, int point2x, int point2y)
        //{
        //    var origin = new Point(0, 0);
        //    var point1 = new Point(point1x, point1y);
        //    var point2 = new Point(point2x, point2y);

        //    new Triangle(origin, point1, point2);
        //}

        //[Test]
        //[TestCase(0, 3, 3, 0)]
        //public void IsEquilateral_DifferentAngles_ReturnsFalse(int point1x, int point1y, int point2x, int point2y)
        //{
        //    var origin = new Point(0, 0);
        //    var point1 = new Point(point1x, point1y);
        //    var point2 = new Point(point2x, point2y);

        //    var result = new Triangle(origin, point1, point2);

        //    Assert.IsFalse(result.IsEquilateral());
        //} 

        #endregion

        [Test]
        [TestCase(3, 4, 5)]
        public void Constructor_WithThreeProperSides_ProperTriangle(double side1, double side2, double side3)
        {
            new Triangle(side1, side2, side3);
        }

        [Test]
        [TestCase(3, 4, 8)]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_WithInvalidSideInput_ThrowsException(double side1, double side2, double side3)
        {
            new Triangle(side1, side2, side3);
        }

   
        [Test]
        [TestCase(2,2,2)]
        public void IsEquilateral_WhenAllSidesEqual_ReturnsTrue(double side1, double side2, double side3)
        {
            var triangle = new Triangle(side1, side2, side3);
            Assert.IsTrue(triangle.IsEquilateral());
        }

        [Test]
        [TestCase(2, 2, 3)]
        [TestCase(2, 3, 2)]
        [TestCase(3, 2, 2)]
        [TestCase(4, 2, 3)]
        public void IsEquilateral_WhenSidesNotEqual_ReturnsFalse(double side1, double side2, double side3)
        {
            var triangle = new Triangle(side1, side2, side3);
            Assert.IsFalse(triangle.IsEquilateral());
        }

        [Test]
        [TestCase(2, 4, 3)]
        [TestCase(18, 44, 30)]
        [TestCase(5, 8, 7)]
        public void IsIsocolese_WhenNoSidesEqual_ReturnsFalse(double side1, double side2, double side3)
        {
            var triangle = new Triangle(side1, side2, side3);
            Assert.IsFalse(triangle.IsIsosceles());
        }

        [Test]
        [TestCase(2, 4, 4)]
        [TestCase(5, 5, 3)]
        [TestCase(8, 8, 7)]
        public void IsIsocolese_WhenTwoSidesEqual_ReturnsTrue(double side1, double side2, double side3)
        {
            var triangle = new Triangle(side1, side2, side3);
            Assert.IsTrue(triangle.IsIsosceles());
        }

        [Test]
        [TestCase(2, 4, 3)]
        [TestCase(18, 44, 30)]
        [TestCase(5, 8, 7)]
        public void IsScalene_WhenNoSidesEqual_ReturnsTrue(double side1, double side2, double side3)
        {
            var triangle = new Triangle(side1, side2, side3);
            Assert.IsTrue(triangle.IsScalene());
        }

        [Test]
        [TestCase(4, 4, 3)]
        [TestCase(18, 44, 44)]
        [TestCase(7, 8, 7)]
        public void IsScalene_WhenSidesEqual_ReturnsFalse(double side1, double side2, double side3)
        {
            var triangle = new Triangle(side1, side2, side3);
            Assert.IsFalse(triangle.IsScalene());
        }

        [Test]
        [TestCase(2, 4, 3)]
        [TestCase(18, 44, 30)]
        [TestCase(5, 8, 7)]
        public void IsScalene_WhenIsIscocolese_ReturnsFalse(double side1, double side2, double side3)
        {
            var triangle = new Triangle(side1, side2, side3);
            Assert.AreEqual(triangle.IsScalene(), !triangle.IsIsosceles());
        }

        [Test]
        [TestCase(2, 4, 3)]
        [TestCase(18, 44, 30)]
        [TestCase(5, 8, 7)]
        public void IsScalene_WhenIsEquilateral_ReturnsFalse(double side1, double side2, double side3)
        {
            var triangle = new Triangle(side1, side2, side3);
            Assert.AreEqual(triangle.IsScalene(), !triangle.IsEquilateral());
        }
    }
}
