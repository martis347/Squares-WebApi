using System;
using Squares.Contracts.Lists.CreateList;
using Squares.Contracts.Points;
using Squares.Storage.Client;

namespace Squares.Handlers.ListsHandlers
{
    public class CreateListHandler : BaseHandler<CreateListRequest, CreateListResponse>
    {
        private readonly IStorage<Point> _storage;

        public CreateListHandler(IStorage<Point> storage)
        {
            if (storage == null)
                throw new ArgumentNullException(nameof(storage));

            _storage = storage;
        }

        public override CreateListResponse HandleCore(CreateListRequest request)
        {
            var result = new CreateListResponse();

            _storage.CreateList(request.ListName);

            return result;
        }
    }
}