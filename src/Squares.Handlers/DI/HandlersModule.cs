using Autofac;
using Squares.Contracts.Lists.CreateList;
using Squares.Contracts.Lists.RemoveList;
using Squares.Contracts.Lists.RetrieveLists;
using Squares.Handlers.ListsHandlers;
using Squares.Storage.Client;

namespace Squares.Handlers.DI
{
    public class HandlersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new CreateListHandler(c.Resolve<IStorage>()))
                .As<IHandler<CreateListRequest, CreateListResponse>>();

            builder.Register(c => new RemoveListHandler(c.Resolve<IStorage>()))
                .As<IHandler<RemoveListRequest, RemoveListResponse>>();

            builder.Register(c => new RetrieveListsHandler(c.Resolve<IStorage>()))
                .As<IHandler<RetrieveListsRequest, RetrieveListsResponse>>();
        }
    }
}