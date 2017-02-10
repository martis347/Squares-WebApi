using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using NUnit.Framework;
using RestSharp;
using Squares.Contracts.Points;
using Squares.Integration.Tests.Helpers;

namespace Squares.Integration.Tests
{
    [TestFixture]
    public class PointsControllerTests
    {
        private RestClient _client;
        private readonly string _baseUrl = ConfigurationManager.AppSettings["WebApiServiceUrl"];

        [OneTimeSetUp]
        public void Setup()
        {
            StartWebApi.Start(_baseUrl);
            _client = new RestClient { BaseUrl = new Uri(_baseUrl) };
        }

        [TearDown]
        public void Dispose()
        {
            var lists = _client.GetLists();
            foreach (var list in lists.ListNames)
            {
                _client.DeleteList(list);
            }
        }

        [Test]
        public void AddOnePointTest()
        {
            IList<Point> points = new List<Point> {
                new Point
                {
                    X = 5,
                    Y = 5
                }
            };
            _client.AddList("Test");
            _client.AddPoints(points, "Test");
            var response = _client.GetPoints("Test");

            CollectionAssert.AreEquivalent(points, response.Points);
        }

        [TestCase(1)]
        [TestCase(10)]
        public void AddMultiplePointsTest(int listsCount)
        {
            for (int j = 0; j < listsCount; j++)
            {
                IList<Point> points = new List<Point>();
                for (int i = 1; i <= 20 * listsCount; i += listsCount)
                {
                    points.Add(new Point($"{i * 5 * (i % 3 == 1 ? -1 : 1) / (i % 2 == 1 * i ? 2 * i : 1)} {i * -5 * (i % 2 == 1 ? -2 : 3) / (i % 3 == 1 * i ? 2 * i : 1)}")); //custom mix function
                }
                _client.AddList($"Test-{j}");
                _client.AddPoints(points, $"Test-{j}");
                var response = _client.GetPoints($"Test-{j}");

                CollectionAssert.AreEquivalent(points, response.Points);
                Assert.That(response.Points, Is.Ordered.By("X").By("Y"));
            }
        }

        [Test]
        public void DeleteOnePointTest()
        {
            IList<Point> points = new List<Point>();
            for (int i = 1; i <= 20; i++)
            {
                points.Add(new Point($"{i * 5 * (i % 3 == 1 ? -1 : 1) / (i % 2 == 1 * i ? 2 * i : 1)} {i * -5 * (i % 2 == 1 ? -2 : 3) / (i % 3 == 1 * i ? 2 * i : 1)}")); //custom mix function
            }
            List<Point> pointsToDelete = new List<Point> { points.First() };

            _client.AddList("Test");
            _client.AddPoints(points, "Test");
            _client.DeletePoints(pointsToDelete, "Test");

            var response = _client.GetPoints("Test");

            CollectionAssert.AreEquivalent(points.Except(pointsToDelete), response.Points);
            Assert.That(response.Points, Is.Ordered.By("X").By("Y"));
        }

        [TestCase(1, 20)]
        [TestCase(20, 1)]
        [TestCase(10, 20)]
        public void DeleteMultiplePointsTest(int listsCount, int pointsCount)
        {
            for (int j = 1; j <= listsCount; j++)
            {
                IList<Point> points = new List<Point>();
                for (int i = 1; i <= pointsCount * j; i += j)
                {
                    points.Add(new Point($"{i * 5 * (i % 3 == 1 ? -1 : 1) / (i % 2 == 1 * i ? 2 * i : 1)} {i * -5 * (i % 2 == 1 ? -2 : 3) / (i % 3 == 1 * i ? 2 * i : 1)}")); //custom mix function
                }
                List<Point> pointsToDelete = new List<Point>();
                for (int i = 0; i < pointsCount; i+= 3)
                {
                    pointsToDelete.Add(points[i]);
                }

                _client.AddList($"Test-{j}");
                _client.AddPoints(points, $"Test-{j}");
                _client.DeletePoints(pointsToDelete, $"Test-{j}");

                var response = _client.GetPoints($"Test-{j}");

                CollectionAssert.AreEquivalent(points.Except(pointsToDelete).ToList(), response.Points);
                Assert.That(response.Points, Is.Ordered.By("X").By("Y"));
            }
        }

        [TestCase(1, 100, 1000)]
        [TestCase(5, 7, 100)]
        [TestCase(10, 100, 1000)]
        [TestCase(1, 0, 1000)]
        public void GetPointsPagingTest(int pageNumber, int pageSize, int dataCount)
        {
            List<Point> points = new List<Point>();
            for (int i = 1; i <= dataCount; i++)
            {
                points.Add(new Point($"{i * (i % 3 == 1 ? -1 : 1) / (i % 2 == 1 * i ? 2 * i : 1)} {i * (i % 2 == 1 ? -2 : 3) / (i % 3 == 1 * i ? 2 * i : 1)}")); //custom mix function
            }

            _client.AddList("Test");
            _client.AddPoints(points, "Test");

            var response = _client.GetPoints("Test", pageNumber, pageSize);

            Assert.That(response.PointsCount, Is.EqualTo(dataCount));
            Assert.That(response.Points.Count, Is.EqualTo(pageSize));
            CollectionAssert.AreEquivalent(
                points.OrderBy(c => c.X).ThenBy(c => c.Y).ToList()
                .GetRange(
                    (pageNumber - 1) * pageSize, pageSize), response.Points);
            Assert.That(response.Points, Is.Ordered.By("X").By("Y"));
        }
    }
}