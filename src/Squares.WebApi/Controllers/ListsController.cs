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
    [RoutePrefix("lists")]
    public class ListsController : ApiController
    {
        public ILifetimeScope Container { get; set; }

        [HttpGet]
        [Route("{name}")]
        public HttpResponseMessage GetList(string name)
        {
            var request = new RetrieveListsRequest
            {
                ListName = name
            };
            var handler = Container.Resolve<IHandler<RetrieveListsRequest, RetrieveListsResponse>>();
            var response = handler.Handle(request);

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpGet]
        public HttpResponseMessage GetLists()
        {
            var request = new RetrieveListsRequest();
            var handler = Container.Resolve<IHandler<RetrieveListsRequest, RetrieveListsResponse>>();
            var response = handler.Handle(request);

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpPost]
        public HttpResponseMessage CreateList(CreateListRequest request)
        {
            var handler = Container.Resolve<IHandler<CreateListRequest, CreateListResponse>>();
            var response = handler.Handle(request);

            return Request.CreateResponse(HttpStatusCode.Created, response);
        }

        [HttpDelete]
        public HttpResponseMessage DeleteList(RemoveListRequest request)
        {
            var handler = Container.Resolve<IHandler<RemoveListRequest, RemoveListResponse>>();
            handler.Handle(request);

            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}