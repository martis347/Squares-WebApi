using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Autofac;
using Squares.Contracts.Lists.CreateList;
using Squares.Contracts.Lists.RemoveList;
using Squares.Contracts.Lists.RetrieveLists;
using Squares.Handlers;

namespace Squares.WebApi.Controllers
{
    public class ListsController : ApiController
    {
        public ILifetimeScope Container { get; set; }

        [HttpGet]
        public HttpResponseMessage GetLists(RetrieveListsRequest request)
        {
            var handler = Container.Resolve<IHandler<RetrieveListsRequest, RetrieveListsResponse>>();
            handler.Handle(request);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public HttpResponseMessage CreateList(CreateListRequest request)
        {
            var handler = Container.Resolve<IHandler<CreateListRequest, CreateListResponse>>();
            handler.Handle(request);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        public HttpResponseMessage DeleteList(RemoveListRequest request)
        {
            var handler = Container.Resolve<IHandler<RemoveListRequest, RemoveListResponse>>();
            handler.Handle(request);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}