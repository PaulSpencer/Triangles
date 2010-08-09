using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace GeometricalObjects.Tests
{
	[TestFixture]
	public class PointTests
	{
        [Test]
        public void Constructor_PassedXandY_SetsXandY()
        {
            var result = new Point(1,2);
            Assert.AreEqual(result.X, 1);
            Assert.AreEqual(result.Y, 2);
        }

        [Test]
        public void Equals_NotAPoint_ReturnsFalse()
        {
            var original = new Point(1, 2);
            var nonPoint = new Random();
            Assert.IsFalse(original.Equals(nonPoint));
        }

        [Test]
        [TestCase(1, 2, 3, 4)]
        [TestCase(4, 5, 99, 100)]
        public void Equals_DifferentPoints_ReturnsFalse(int x1, int y1, int x2, int y2)
        {
            var original = new Point(x1, y1);
            var nonMatchingPoint = new Point(x2, y2);
            Assert.IsFalse(original.Equals(nonMatchingPoint));
            Assert.IsFalse(original == nonMatchingPoint);
            Assert.IsTrue(original != nonMatchingPoint);
        }

        [Test]
        [TestCase(1, 2, 1, 2)]
        [TestCase(2, 3, 2, 3)]
        public void Equals_SamePoints_ReturnsTrue(int x1, int y1, int x2, int y2)
        {
            var original = new Point(x1, y1);
            var matchingPoint = new Point(x2, y2);
            Assert.IsTrue(original.Equals(matchingPoint));
            Assert.IsTrue(original == matchingPoint);
            Assert.IsFalse(original != matchingPoint);
        }

        [Test]
        [TestCase(1, 2, "X = 1 Y = 2")]
        [TestCase(2, 3, "X = 2 Y = 3")]
        public void ToString_WithTwoPoints_ReturnsDescriptiveText(int x, int y, string expected)
        {
            var point = new Point(x,y);
            Assert.AreEqual(expected, point.ToString());
        }
	}
}
