using Autofac;
using Squares.Contracts.Lists.CreateList;
using Squares.Contracts.Lists.RemoveList;
using Squares.Contracts.Lists.RetrieveLists;
using Squares.Contracts.Points.AddPoints;
using Squares.Contracts.Points.RemovePoint;
using Squares.Contracts.Points.RetrievePoints;
using Squares.Contracts.Squares.RetrieveSquares;
using Squares.Handlers.ListsHandlers;
using Squares.Handlers.PointsHandlers;
using Squares.Handlers.SquaresHandlers;
using Squares.Storage.Client;

namespace Squares.Handlers.DI
{
    public class HandlersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterListHandlers(builder);
            RegisterPointsHandlers(builder);
            RegisterSquaresHandlers(builder);
        }

        private void RegisterSquaresHandlers(ContainerBuilder builder)
        {
            builder.Register(c => new RetrieveSquaresHandler(c.Resolve<IStorage>()))
                .As<IHandler<RetrieveSquaresRequest, RetrieveSquaresResponse>>();
        }

        private void RegisterListHandlers(ContainerBuilder builder)
        {
            builder.Register(c => new CreateListHandler(c.Resolve<IStorage>()))
                .As<IHandler<CreateListRequest, CreateListResponse>>();

            builder.Register(c => new RemoveListHandler(c.Resolve<IStorage>()))
                .As<IHandler<RemoveListRequest, RemoveListResponse>>();

            builder.Register(c => new RetrieveListsHandler(c.Resolve<IStorage>()))
                .As<IHandler<RetrieveListsRequest, RetrieveListsResponse>>();
        }

        private void RegisterPointsHandlers(ContainerBuilder builder)
        {
            builder.Register(c => new AddPointsHandler(c.Resolve<IStorage>()))
                .As<IHandler<AddPointsRequest, AddPointsResponse>>();

            builder.Register(c => new RemovePointsHandler(c.Resolve<IStorage>()))
                .As<IHandler<RemovePointsRequest, RemovePointsResponse>>();

            builder.Register(c => new RetrievePointsHandler(c.Resolve<IStorage>()))
                .As<IHandler<RetrievePointsRequest, RetrievePointsResponse>>();
        }
    }
}