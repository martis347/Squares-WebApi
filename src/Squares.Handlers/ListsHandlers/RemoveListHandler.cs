using System;
using Squares.Contracts;
using Squares.Contracts.Lists.RemoveList;
using Squares.Contracts.Points;
using Squares.Machine;
using Squares.Storage.Client;

namespace Squares.Handlers.ListsHandlers
{
    public class RemoveListHandler : BaseHandler<RemoveListRequest, RemoveListResponse>
    {
        private readonly IStorage<Point> _storage;
        private readonly IMachine<BaseRequest> _machine;

        public RemoveListHandler(IStorage<Point> storage, IMachine<BaseRequest> machine)
        {
            if (storage == null)
                throw new ArgumentNullException(nameof(storage));
            if (machine == null)
                throw new ArgumentNullException(nameof(machine));

            _storage = storage;
            _machine = machine;
        }
        public override RemoveListResponse HandleCore(RemoveListRequest request)
        {
            var result = new RemoveListResponse();

            _storage.RemoveList(request.ListName);
            _machine.RemoveList(request.ListName);
            return result;
        }
    }
}