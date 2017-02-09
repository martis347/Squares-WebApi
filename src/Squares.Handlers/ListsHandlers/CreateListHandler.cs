using System;
using Squares.Contracts;
using Squares.Contracts.Lists.CreateList;
using Squares.Contracts.Points;
using Squares.Machine;
using Squares.Storage.Client;

namespace Squares.Handlers.ListsHandlers
{
    public class CreateListHandler : BaseHandler<CreateListRequest, CreateListResponse>
    {
        private readonly IStorage<Point> _storage;
        private readonly IMachine<BaseRequest> _machine;

        public CreateListHandler(IStorage<Point> storage, IMachine<BaseRequest> machine)
        {
            if (storage == null)
                throw new ArgumentNullException(nameof(storage));
            if (machine == null)
                throw new ArgumentNullException(nameof(machine));

            _storage = storage;
            _machine = machine;
        }

        public override CreateListResponse HandleCore(CreateListRequest request)
        {
            var result = new CreateListResponse();

            _storage.CreateList(request.ListName);
            _machine.CreateList(request.ListName);

            return result;
        }
    }
}