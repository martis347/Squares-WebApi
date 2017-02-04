using System;
using Squares.Contracts.Squares;
using Squares.Contracts.Squares.RetrieveSquares;
using Squares.Storage.Client;

namespace Squares.Handlers.SquaresHandlers
{
    public class RetrieveSquaresHandler : BaseHandler<RetrieveSquaresRequest, RetrieveSquaresResponse>
    {
        private readonly IStorage<Square> _storage;

        public RetrieveSquaresHandler(IStorage<Square> storage)
        {
            if (storage == null)
                throw new ArgumentNullException(nameof(storage));

            _storage = storage;
        }

        public override RetrieveSquaresResponse HandleCore(RetrieveSquaresRequest request)
        {
            var result = new RetrieveSquaresResponse
            {
                Squares = _storage.RetrieveItems(request.ListName, request.PageSize, request.PageNumber),
                SquaresCount = _storage.RetrieveItemsCount(request.ListName)
            };

            return result;
        }
    }
}