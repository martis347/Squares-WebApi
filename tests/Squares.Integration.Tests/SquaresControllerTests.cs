using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using NUnit.Framework;
using RestSharp;
using Squares.Contracts.Points;
using Squares.Contracts.Squares;
using Squares.Integration.Tests.Helpers;

namespace Squares.Integration.Tests
{
    [TestFixture]
    public class SquaresControllerTests
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
        public void AddSquarePointsTest()
        {
            IList<Square> squares = new List<Square>();

            for (int i = 1; i <= 10; i++)
            {
                squares.Add(new Square($"-{i} -{i}.{i} -{i}.-{i} {i}.{i} {i}"));
            }
            _client.AddList("Test");

            foreach (var square in squares)
            {
                _client.AddPoints(square.Points, "Test");
            }
            var response = _client.GetSquares("Test");

            CollectionAssert.AreEquivalent(squares, response.Squares);
        }

        [Test]
        public void AddVariousPointsTest()
        {
            IList<Square> squares = new List<Square>();

            for (int i = 1; i <= 10; i++)
            {
                squares.Add(new Square($"-{i} -{i}.{i} -{i}.-{i} {i}.{i} {i}"));
            }
            _client.AddList("Test");

            foreach (var square in squares)
            {
                _client.AddPoints(square.Points, "Test");
            }

            List<Point> points = new List<Point>();
            for (int i = 1; i <= 50; i++)
            {
                points.Add(new Point($"{i * (i % 3 == 1 ? -1 : 1) / (i % 2 == 1 * i ? 2 * i : 1)} {i * (i % 2 == 1 ? -2 : 3) / (i % 3 == 1 * i ? 2 * i : 1)}")); //custom mix function
            }
            _client.AddPoints(points, "Test");

            var response = _client.GetSquares("Test");

            CollectionAssert.AreEquivalent(squares, response.Squares);
        }

        [TestCase(1, 5)]
        [TestCase(2, 10)]
        [TestCase(3, 23)]
        [TestCase(4, 28)]
        [TestCase(5, 33)]
        [TestCase(6, 46)]
        public void AddVariousPointsSquaresShareSideTest(int pointsCount, int expectedResult)
        {
            IList<Square> squares = new List<Square>();

            for (int i = 1; i <= pointsCount; i++)
            {
                squares.Add(new Square($"-{i} -{i}.{i} -{i}.-{i} {i}.{i} {i}"));
            }
            _client.AddList("Test");
            foreach (var square in squares)
            {
                _client.AddPoints(square.Points, "Test");
            }
            List<Point> points = new List<Point>();
            for (int i = 1; i <= pointsCount; i++)
            {
                points.Add(new Point($"-{i} {i + 2 * i}"));
                points.Add(new Point($"{i} {i + 2 * i}"));

                points.Add(new Point($"{i + 2 * i} -{i}"));
                points.Add(new Point($"{i + 2 * i} {i}"));

                points.Add(new Point($"-{i} -{i + 2 * i}"));
                points.Add(new Point($"{i} -{i + 2 * i}"));

                points.Add(new Point($"-{i + 2 * i} -{i}"));
                points.Add(new Point($"-{i + 2 * i} {i}"));
            }
            _client.AddPoints(points, "Test");

            points = new List<Point>();
            for (int i = 1; i <= 50; i++)
            {
                points.Add(new Point($"{i * (i % 3 == 1 ? -1 : 1) / (i % 2 == 1 * i ? 2 * i : 1)} {i * (i % 2 == 1 ? -2 : 3) / (i % 3 == 1 * i ? 2 * i : 1)}")); //custom mix function
            }
            _client.AddPoints(points, "Test");

            var response = _client.GetSquares("Test");

            var orderedSquares = response.Squares.OrderBy(c => c.Points, new Square.SquareComparer());

            Assert.AreEqual(expectedResult, response.Squares.Count);
            CollectionAssert.AreEqual(orderedSquares, response.Squares);
        }

        [TestCase(1, 10, 100)]
        [TestCase(5, 7, 100)]
        [TestCase(10, 10, 100)]
        [TestCase(1, 0, 100)]
        public void SquaresPagingTest(int pageNumber, int pageSize, int dataCount)
        {
            List<Square> squares = new List<Square>();

            for (int i = 1; i <= dataCount; i++)
            {
                int j = i * 3 * (i % 3 == 1 ? -1 : 1) / (i % 2 == 1 * i ? 2 * i : 1);
                squares.Add(new Square($"{-j} {-j}.{j} {-j}.{-j} {j}.{j} {j}"));
            }
            _client.AddList("Test");
            foreach (var square in squares)
            {
                _client.AddPoints(square.Points, "Test");
            }

            var response = _client.GetSquares("Test", pageNumber, pageSize);
            var orderedSquares = response.Squares.OrderBy(c => c.Points, new Square.SquareComparer());

            Assert.That(response.SquaresCount, Is.EqualTo(dataCount));
            Assert.That(response.Squares.Count, Is.EqualTo(pageSize));
            CollectionAssert.AreEqual(orderedSquares, response.Squares);
        }
    }
}