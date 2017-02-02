using System;
using System.ComponentModel;
using System.Linq;
using Squares.Contracts.Points;
using Squares.Contracts.Points.RetrievePoints;
using Squares.Storage.Client;

namespace Squares.Handlers.PointsHandlers
{
    public class RetrievePointsHandler : BaseHandler<RetrievePointsRequest, RetrievePointsResponse>
    {
        private readonly IStorage<Point> _storage;

        public RetrievePointsHandler(IStorage<Point> storage)
        {
            if (storage == null)
                throw new ArgumentNullException(nameof(storage));

            _storage = storage;
        }

        public override RetrievePointsResponse HandleCore(RetrievePointsRequest request)
        {
            var result = new RetrievePointsResponse
            {
                Points = request.SortDirection == ListSortDirection.Ascending
                    ? _storage.RetrieveItems(request.ListName, request.PageSize, request.PageNumber).OrderBy(c => c.X).ThenBy(c => c.Y).ToList()
                    : _storage.RetrieveItems(request.ListName, request.PageSize, request.PageNumber).OrderByDescending(c => c.X).ThenBy(c => c.Y).ToList()
            };

            return result;
        }
    }
}