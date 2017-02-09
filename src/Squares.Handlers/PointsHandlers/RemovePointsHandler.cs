using System;
using Squares.Contracts;
using Squares.Contracts.Points;
using Squares.Contracts.Points.RemovePoint;
using Squares.Machine;
using Squares.Storage.Client;

namespace Squares.Handlers.PointsHandlers
{
    public class RemovePointsHandler : BaseHandler<RemovePointsRequest, RemovePointsResponse>
    {
        private readonly IStorage<Point> _storage;
        private readonly IMachine<BaseRequest> _machine;

        public RemovePointsHandler(IStorage<Point> storage, IMachine<BaseRequest> machine)
        {
            if (storage == null)
                throw new ArgumentNullException(nameof(storage));
            if (machine == null)
                throw new ArgumentNullException(nameof(machine));

            _storage = storage;
            _machine = machine;
        }

        public override RemovePointsResponse HandleCore(RemovePointsRequest request)
        {
            var result = new RemovePointsResponse();

            _storage.RemoveFromList(request.Points, request.ListName);
            _machine.RemovePoints(request);
            return result;
        }
    }
}