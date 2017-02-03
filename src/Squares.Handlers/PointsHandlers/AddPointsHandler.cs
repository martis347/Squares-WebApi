using System;
using System.Linq;
using Squares.Contracts.Points;
using Squares.Contracts.Points.AddPoints;
using Squares.Storage.Client;

namespace Squares.Handlers.PointsHandlers
{
    public class AddPointsHandler : BaseHandler<AddPointsRequest, AddPointsResponse>
    {
        private readonly IStorage<Point> _storage;

        public AddPointsHandler(IStorage<Point> storage)
        {
            if (storage == null)
                throw new ArgumentNullException(nameof(storage));

            _storage = storage;
        }

        public override AddPointsResponse HandleCore(AddPointsRequest request)
        {
            var result = new AddPointsResponse();

            _storage.AddToList(request.Points.OrderBy(c => c.X).ThenBy(c => c.Y).ToList(), request.ListName);

            return result;
        }
    }
}