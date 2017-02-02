using System;
using Squares.Contracts.Points;
using Squares.Contracts.Points.RemovePoint;
using Squares.Storage.Client;

namespace Squares.Handlers.PointsHandlers
{
    public class RemovePointsHandler : BaseHandler<RemovePointsRequest, RemovePointsResponse>
    {
        private readonly IStorage<Point> _storage;

        public RemovePointsHandler(IStorage<Point> storage)
        {
            if (storage == null)
                throw new ArgumentNullException(nameof(storage));

            _storage = storage;
        }

        public override RemovePointsResponse HandleCore(RemovePointsRequest request)
        {
            var result = new RemovePointsResponse();

            _storage.RemoveFromList(request.Points, request.ListName);

            return result;
        }
    }
}