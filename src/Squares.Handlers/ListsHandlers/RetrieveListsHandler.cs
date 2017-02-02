using System;
using Squares.Contracts.Lists.RetrieveLists;
using Squares.Storage.Client;

namespace Squares.Handlers.ListsHandlers
{
    public class RetrieveListsHandler : BaseHandler<RetrieveListsRequest, RetrieveListsResponse>
    {
        private readonly IStorage _storage;

        public RetrieveListsHandler(IStorage storage)
        {
            if (storage == null)
                throw new ArgumentNullException(nameof(storage));

            _storage = storage;
        }

        public override RetrieveListsResponse HandleCore(RetrieveListsRequest request)
        {
            return new RetrieveListsResponse
            {
                ListNames = _storage.RetrieveListNames()
            };
        }
    }
}