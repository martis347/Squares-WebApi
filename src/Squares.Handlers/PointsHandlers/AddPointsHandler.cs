using System;
using System.Linq;
using Squares.Contracts;
using Squares.Contracts.Points;
using Squares.Contracts.Points.AddPoints;
using Squares.Machine;
using Squares.Storage.Client;

namespace Squares.Handlers.PointsHandlers
{
    public class AddPointsHandler : BaseHandler<AddPointsRequest, AddPointsResponse>
    {
        private readonly IStorage<Point> _storage;
        private readonly IMachine<BaseRequest> _machine;

        public AddPointsHandler(IStorage<Point> storage, IMachine<BaseRequest> machine)
        {
            if (storage == null)
                throw new ArgumentNullException(nameof(storage));
            if (machine == null)
                throw new ArgumentNullException(nameof(machine));

            _storage = storage;
            _machine = machine;
        }

        public override AddPointsResponse HandleCore(AddPointsRequest request)
        {
            var result = new AddPointsResponse();

            _storage.AddToList(request.Points.OrderBy(c => c.X).ThenBy(c => c.Y).ToList(), request.ListName);
            _machine.AddPoints(request);
            return result;
        }
    }
}