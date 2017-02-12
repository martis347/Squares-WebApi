using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Squares.Contracts.Points;

namespace Squares.Comparison.Tests
{
    [TestFixture]
    public class PointsComparisonTests
    {
        [Test]
        public void PointsEqualTest()
        {
            var point1 = new Point("0 0");
            var point2 = new Point("0 0");

            Assert.IsTrue(point1.Equals(point2));
        }

        [TestCase("0 1")]
        [TestCase("1 0")]
        [TestCase("-1 0")]
        public void PointsNotEqualTest(string pointValue)
        {
            var point1 = new Point("0 0");
            var point2 = new Point(pointValue);

            Assert.IsFalse(point1.Equals(point2));
        }

        [TestCase(0, "0 0")]
        [TestCase(-1, "-1 10")]
        [TestCase(-1, "0 -1")]
        [TestCase(-1, "0 -50")]
        [TestCase(1, "1 1")]
        [TestCase(1, "0 1")]
        [TestCase(1, "1 50")]
        public void PointsCompareTest(int expectedResult, string pointValue)
        {
            var point1 = new Point("0 0");
            var point2 = new Point(pointValue);

            Assert.AreEqual(expectedResult, point2.CompareTo(point1));
        }

        [Test]
        public void PointsSortTest()
        {
            List<Point> points = new List<Point>
            {
                new Point("0 0"),
                new Point("-1 5"),
                new Point("8 -5"),
                new Point("1 20"),
                new Point("-1 17"),
                new Point("8 -4"),
                new Point("2 0"),
                new Point("-3 5"),
                new Point("3 -5")
            };

            List<Point> expectedPoints = new List<Point>
            {
                new Point("-3 5"),
                new Point("-1 5"),
                new Point("-1 17"),
                new Point("0 0"),
                new Point("1 20"),
                new Point("2 0"),
                new Point("3 -5"),
                new Point("8 -5"),
                new Point("8 -4")
            };

            var sortedPoints = points.OrderBy(c => c.X).ThenBy(c => c.Y);

            CollectionAssert.AreEqual(expectedPoints, sortedPoints);
        }
    }
}