using System.Collections.Generic;
using NUnit.Framework;
using Squares.Contracts.Squares;

namespace Squares.Integration.Tests.Helpers
{
    public static class CustomAsserts
    {
        public static void SquarePointsAreSorted(IList<Square> items)
        {
            foreach (var item in items)
            {
                Assert.That(item.Points, Is.Ordered.By("X").By("Y"));
            }
        }
    }
}