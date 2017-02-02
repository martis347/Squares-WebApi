using System;
using Squares.Contracts.Points.AddPoints;
using Squares.Storage.Client;

namespace Squares.Handlers.PointsHandlers
{
    public class AddPointsHandler : BaseHandler<AddPointsRequest, AddPointsResponse>
    {
        private readonly IStorage _storage;

        public AddPointsHandler(IStorage storage)
        {
            if (storage == null)
                throw new ArgumentNullException(nameof(storage));

            _storage = storage;
        }

        public override AddPointsResponse HandleCore(AddPointsRequest request)
        {
            var result = new AddPointsResponse();

            _storage.AddToList(request.Points, request.ListName);

            return result;
        }
    }
}