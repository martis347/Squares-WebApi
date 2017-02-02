using System;
using System.ComponentModel;
using System.Linq;
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
            var result = new RetrieveListsResponse
            {
                ListNames = request.SortDirection == ListSortDirection.Ascending
                    ? _storage.RetrieveListNames().OrderBy(c => c).ToList()
                    : _storage.RetrieveListNames().OrderByDescending(c => c).ToList()
            };

            return result;
        }
    }
}