using System;
using System.Collections.Generic;
using System.Linq;
using Squares.Contracts.Points;
using Squares.Contracts.Squares;
using Squares.Contracts.Squares.RetrieveSquares;
using Squares.Storage.Client;

namespace Squares.Handlers.SquaresHandlers
{
    public class RetrieveSquaresHandler : BaseHandler<RetrieveSquaresRequest, RetrieveSquaresResponse>
    {
        private readonly IStorage _storage;

        public RetrieveSquaresHandler(IStorage storage)
        {
            if (storage == null)
                throw new ArgumentNullException(nameof(storage));

            _storage = storage;
        }

        public override RetrieveSquaresResponse HandleCore(RetrieveSquaresRequest request)
        {
            var result = new RetrieveSquaresResponse
            {
                Squares = new List<Square>
                {
                    new Square
                    {
                        Points = new []
                        {
                            new Point("0 0"),
                            new Point("0 1"),
                            new Point("1 0"),
                            new Point("1 1"),
                        }
                    },
                    new Square
                    {
                        Points = new []
                        {
                            new Point("0 0"),
                            new Point("0 1"),
                            new Point("1 0"),
                            new Point("1 1"),
                        }
                    },
                    new Square
                    {
                        Points = new []
                        {
                            new Point("0 0"),
                            new Point("0 1"),
                            new Point("1 0"),
                            new Point("1 1"),
                        }
                    },
                    new Square
                    {
                        Points = new []
                        {
                            new Point("0 0"),
                            new Point("0 1"),
                            new Point("1 0"),
                            new Point("1 1"),
                        }
                    },
                    new Square
                    {
                        Points = new []
                        {
                            new Point("0 0"),
                            new Point("0 1"),
                            new Point("1 0"),
                            new Point("1 1"),
                        }
                    },
                    new Square
                    {
                        Points = new []
                        {
                            new Point("0 0"),
                            new Point("0 1"),
                            new Point("1 0"),
                            new Point("1 1"),
                        }
                    },
                    new Square
                    {
                        Points = new []
                        {
                            new Point("0 0"),
                            new Point("0 1"),
                            new Point("1 0"),
                            new Point("1 1"),
                        }
                    }
                }
            };

            return result;
        }
    }
}