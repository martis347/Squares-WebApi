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
            builder.RegisterType<RetrieveSquaresHandler>()
                .As<IHandler<RetrieveSquaresRequest, RetrieveSquaresResponse>>()
                .PropertiesAutowired();
        }

        private void RegisterListHandlers(ContainerBuilder builder)
        {
            builder.RegisterType<CreateListHandler>()
                .As<IHandler<CreateListRequest, CreateListResponse>>()
                .PropertiesAutowired();

            builder.RegisterType<RemoveListHandler>()
                .As<IHandler<RemoveListRequest, RemoveListResponse>>()
                .PropertiesAutowired();

            builder.RegisterType<RetrieveListsHandler>()
                .As<IHandler<RetrieveListsRequest, RetrieveListsResponse>>()
                .PropertiesAutowired();
        }

        private void RegisterPointsHandlers(ContainerBuilder builder)
        {
            builder.RegisterType<AddPointsHandler>()
                .As<IHandler<AddPointsRequest, AddPointsResponse>>()
                .PropertiesAutowired();

            builder.RegisterType<RemovePointsHandler>()
                .As<IHandler<RemovePointsRequest, RemovePointsResponse>>()
                .PropertiesAutowired();

            builder.RegisterType<RetrievePointsHandler>()
                .As<IHandler<RetrievePointsRequest, RetrievePointsResponse>>()
                .PropertiesAutowired();
        }
    }
}