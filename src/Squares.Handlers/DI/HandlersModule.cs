using Autofac;
using Squares.Contracts.Lists.CreateList;
using Squares.Contracts.Lists.RemoveList;
using Squares.Contracts.Lists.RetrieveLists;
using Squares.Handlers.ListsHandlers;

namespace Squares.Handlers.DI
{
    public class HandlersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
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
    }
}