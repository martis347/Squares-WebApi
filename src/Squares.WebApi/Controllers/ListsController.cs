using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Squares.Contracts.Lists.CreateList;
using Squares.Contracts.Lists.RemoveList;
using Squares.Contracts.Lists.RetrieveLists;

namespace Squares.WebApi.Controllers
{
    public class ListsController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetLists(RetrieveListsRequest request)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public HttpResponseMessage CreateList(CreateListRequest request)
        {
            Console.WriteLine(request.ListName);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        public HttpResponseMessage DeleteList(RemoveListRequest request)
        {
            Console.WriteLine(request.ListName);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}