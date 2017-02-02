using System.Net;
using System.Net.Http;
using System.Web.Http;
using Autofac;
using Squares.Contracts.Squares.RetrieveSquares;
using Squares.Handlers;

namespace Squares.WebApi.Controllers
{
    [RoutePrefix("squares")]
    public class SquaresController : ApiController
    {
        public ILifetimeScope Container { get; set; }

        [HttpGet]
        [Route("{name}")]
        public HttpResponseMessage GetSquares(string name, [FromUri] int pageSize = 20, [FromUri] int pageNumber = 1)
        {
            var request = new RetrieveSquaresRequest
            {
                ListName = name,
                PageSize = pageSize,
                PageNumber = pageNumber
            };

            var handler = Container.Resolve<IHandler<RetrieveSquaresRequest, RetrieveSquaresResponse>>();
            var result = handler.Handle(request);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}