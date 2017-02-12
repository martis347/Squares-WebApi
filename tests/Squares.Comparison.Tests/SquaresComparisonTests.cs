using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Squares.Contracts.Squares;

namespace Squares.Comparison.Tests
{
    [TestFixture]
    public class SquaresComparisonTests
    {
        [Test]
        public void SquaresEqualTest()
        {
            var square1 = new Square("1 1.1 -1.-1 1.-1 -1");
            var square2 = new Square("1 1.1 -1.-1 1.-1 -1");

            Assert.IsTrue(square1.Equals(square2));
        }

        [TestCase("1 1.1 -2.-1 1.-1 -1")]
        [TestCase("1 -1.1 -1.-1 1.-1 -1")]
        [TestCase("1 1.1 -1.-1 1.0 -1")]
        [TestCase("1 1.1 -1.-1 1.-1 1")]
        public void SquaresNotEqualTest(string squareValue)
        {
            var square1 = new Square("1 1.1 -1.-1 1.-1 -1");
            var square2 = new Square(squareValue);

            Assert.IsFalse(square1.Equals(square2));
        }

        [TestCase(0, "1 1.1 -1.-1 1.-1 1")]
        [TestCase(-1, "1 1.1 -1.-1 1.-1 -1")]
        [TestCase(-1, "1 1.-1 -1.-1 1.-1 1")]
        [TestCase(-1, "1 1.-1 -1.-1 1.-1 0")]
        [TestCase(-1, "-1 1.-1 -1.-1 1.-1 0")]
        [TestCase(1, "2 1.1 -1.-1 1.-1 1")]
        [TestCase(1, "2 1.1 -1.1 1.-1 2")]
        [TestCase(1, "2 1.1 -1.-1 1.-1 1")]
        [TestCase(1, "2 1.1 -1.-1 1.0 1")]
        public void SquaresCompareTest(int expectedResult, string squareValue)
        {
            var square = new Square("1 1.1 -1.-1 1.-1 1");
            var square2 = new Square(squareValue);

            Assert.AreEqual(expectedResult, square2.CompareTo(square));
        }

        [Test]
        public void SquaresSortTest()
        {
            List<Square> squares = new List<Square>
            {
                new Square("1 1.1 -1.-1 1.-1 -1"),
                new Square("1 -1.-5 1.-1 1.-1 1"),
                new Square("1 4.1 -1.-1 -1.0 -1"),
                new Square("3 1.1 -1.-5 -1.1 -8"),
                new Square("1 1.1 -4.-1 -1.1 0"),
                new Square("-1 -1.1 1.-1 -1.-1 -1"),
                new Square("1 -1.1 -1.1 10.1 -1"),
                new Square("2 -1.3 -1.1 -1.1 -1"),
                new Square("1 -1.2 -1.12 1.0 -1"),
                new Square("0 -1.1 0.1 -2.1 1"),
                new Square("1 2.-1 1.-1 1.-1 -4"),
                new Square("1 15.-1 1.0 1.-1 1"),
                new Square("1 1.-1 1.-1 1.-1 -1"),
                new Square("-4 0.-1 1.-1 -1.-1 1"),
                new Square("-1 1.-1 -1.-1 1.-7 1"),
                new Square("1 1.1 1.1 -1.-4 -1"),
                new Square("1 1.1 1.2 -1.-11 -1"),
                new Square("1 1.0 1.1 -1.-1 -1"),
                new Square("-1 1.-1 1.-3 1.1 -1")
            };

            List<Square> squaresExpected = new List<Square>
            {
                new Square("-4 0.-1 1.-1 -1.-1 1"),
                new Square("-1 -1.1 1.-1 -1.-1 -1"),
                new Square("-1 1.-1 -1.-1 1.-7 1"),
                new Square("-1 1.-1 1.-3 1.1 -1"),
                new Square("0 -1.1 0.1 -2.1 1"),
                new Square("1 -1.-5 1.-1 1.-1 1"),
                new Square("1 -1.1 -1.1 10.1 -1"),
                new Square("1 -1.2 -1.12 1.0 -1"),
                new Square("1 1.-1 1.-1 1.-1 -1"),
                new Square("1 1.0 1.1 -1.-1 -1"),
                new Square("1 1.1 -4.-1 -1.1 0"),
                new Square("1 1.1 -1.-1 1.-1 -1"),
                new Square("1 1.1 1.1 -1.-4 -1"),
                new Square("1 1.1 1.2 -1.-11 -1"),
                new Square("1 2.-1 1.-1 1.-1 -4"),
                new Square("1 4.1 -1.-1 -1.0 -1"),
                new Square("1 15.-1 1.0 1.-1 1"),
                new Square("2 -1.3 -1.1 -1.1 -1"),
                new Square("3 1.1 -1.-5 -1.1 -8"),
            };

            var squaresSorted = squares.OrderBy(c => c.Points, new Square.SquareComparer());

            CollectionAssert.AreEqual(squaresExpected, squaresSorted);
        }
    }
}
