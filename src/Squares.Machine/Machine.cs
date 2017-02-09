using System;
using System.Collections.Generic;
using System.Linq;
using Squares.Contracts;
using Squares.Contracts.Points;
using Squares.Contracts.Points.AddPoints;
using Squares.Contracts.Points.RemovePoint;
using Squares.Contracts.Squares;
using Squares.Storage.Client;

namespace Squares.Machine
{
    public class Machine : IMachine<BaseRequest>
    {
        private readonly IStorage<Square> _squaresStorage;
        private readonly IStorage<Point> _pointsStorage;

        public Machine(IStorage<Square> squaresStorage, IStorage<Point> pointsStorage)
        {
            if (squaresStorage == null)
                throw new ArgumentNullException(nameof(squaresStorage));
            if (pointsStorage == null)
                throw new ArgumentNullException(nameof(pointsStorage));

            _squaresStorage = squaresStorage;
            _pointsStorage = pointsStorage;
        }

        public bool CreateList(string listName)
        {
            _squaresStorage.CreateList(listName);
            _pointsStorage.CreateList(listName);

            return true;
        }

        public bool RemoveList(string listName)
        {
            _squaresStorage.RemoveList(listName);
            _pointsStorage.RemoveList(listName);

            return true;
        }

        public bool AddPoints(BaseRequest request)
        {
            return AddPoints((AddPointsRequest)request);
        }

        public bool RemovePoints(BaseRequest request)
        {
            return RemovePoints((RemovePointsRequest)request);
        }

        private List<Square> ProcessPoints(IList<Point> points)
        {
            Dictionary<int, List<int>> pointsDictionary = new Dictionary<int, List<int>>();
            IEnumerable<int> xValues = pointsDictionary.Keys;
            List<Square> squares = new List<Square>();

            foreach (var requestPoint in points)
            {
                if (pointsDictionary.ContainsKey(requestPoint.X))
                {
                    pointsDictionary[requestPoint.X].Add(requestPoint.Y);
                }
                else
                {
                    pointsDictionary.Add(requestPoint.X, new List<int> { requestPoint.Y });
                }
            }

            int iterations = 1;

            foreach (var xValue1 in pointsDictionary.Keys)
            {
                foreach (var xValue2 in xValues.Skip(iterations++))
                {
                    var intersect = pointsDictionary[xValue1].Intersect(pointsDictionary[xValue2]).ToList();
                    int count = 1;
                    foreach (var item1 in intersect)
                    {
                        foreach (var item2 in intersect.Skip(count++))
                        {
                            if (Math.Abs(item1 - item2) == Math.Abs(xValue1 - xValue2))
                            {
                                var square = new Square
                                {
                                    Points = new[]
                                    {
                                        new Point
                                        {
                                            X = xValue1,
                                            Y = item1
                                        },
                                        new Point
                                        {
                                            X = xValue2,
                                            Y = item1
                                        },
                                        new Point
                                        {
                                            X = xValue1,
                                            Y = item2
                                        },
                                        new Point
                                        {
                                            X = xValue2,
                                            Y = item2
                                        }
                                    }
                                };
                                squares.Add(square);

                                foreach (var point in square.Points)
                                {
                                    points.Remove(points.FirstOrDefault(c => c.X == point.X && c.Y == point.Y));
                                }
                            }
                        }
                    }
                }
            }

            return squares;
        }

        private bool AddPoints(AddPointsRequest request)
        {
            _pointsStorage.AddToList(request.Points, request.ListName);

            var squares = ProcessPoints(_pointsStorage.RetrieveItems(request.ListName, 0, 0));

            squares.ForEach(c => _pointsStorage.RemoveFromList(c.Points, request.ListName));

            _squaresStorage.AddToList(squares, request.ListName);

            return true;
        }

        private bool RemovePoints(RemovePointsRequest request)
        {
            IList<Square> allSquares = _squaresStorage.RetrieveItems(request.ListName, 0, 0);
            List<Square> squaresToRemove = new List<Square>();
            foreach (var point in request.Points)
            {
                squaresToRemove.AddRange(allSquares.Where(c => c.Points.Any(p => p.X == point.X && p.Y == point.Y)));
            }

            squaresToRemove.ForEach(c => _pointsStorage.AddToList(c.Points, request.ListName));
            _pointsStorage.RemoveFromList(request.Points, request.ListName);

            return true;
        }
    }
}