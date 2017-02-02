using System;
using Squares.Contracts.Lists.CreateList;
using Squares.Storage.Client;

namespace Squares.Handlers.ListsHandlers
{
    public class CreateListHandler : BaseHandler<CreateListRequest, CreateListResponse>
    {
        private readonly IStorage _storage;

        public CreateListHandler(IStorage storage)
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