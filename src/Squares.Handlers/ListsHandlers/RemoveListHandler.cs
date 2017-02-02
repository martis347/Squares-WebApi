using System;
using Squares.Contracts.Lists.RemoveList;
using Squares.Contracts.Points;
using Squares.Storage.Client;

namespace Squares.Handlers.ListsHandlers
{
    public class RemoveListHandler : BaseHandler<RemoveListRequest, RemoveListResponse>
    {
        private readonly IStorage<Point> _storage;

        public RemoveListHandler(IStorage<Point> storage)
        {
            if (storage == null)
                throw new ArgumentNullException(nameof(storage));

            _storage = storage;
        }
        public override RemoveListResponse HandleCore(RemoveListRequest request)
        {
            var result = new RemoveListResponse();

            _storage.RemoveList(request.ListName);

            return result;
        }
    }
}