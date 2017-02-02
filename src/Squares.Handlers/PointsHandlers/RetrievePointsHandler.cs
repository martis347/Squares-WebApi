using System;
using Squares.Contracts.Points.RetrievePoints;
using Squares.Storage.Client;

namespace Squares.Handlers.PointsHandlers
{
    public class RetrievePointsHandler : BaseHandler<RetrievePointsRequest, RetrievePointsResponse>
    {
        private readonly IStorage _storage;

        public RetrievePointsHandler(IStorage storage)
        {
            if (storage == null)
                throw new ArgumentNullException(nameof(storage));

            _storage = storage;
        }

        public override RetrievePointsResponse HandleCore(RetrievePointsRequest request)
        {
            var result = new RetrievePointsResponse
            {
                Points = _storage.RetrieveList(request.ListName)
            };

            return result;
        }
    }
}