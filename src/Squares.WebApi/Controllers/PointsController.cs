using System.Net;
using System.Net.Http;
using System.Web.Http;
using Autofac;
using Squares.Contracts.Points.AddPoints;
using Squares.Contracts.Points.RemovePoint;
using Squares.Contracts.Points.RetrievePoints;
using Squares.Handlers;

namespace Squares.WebApi.Controllers
{
    [RoutePrefix("points")]
    public class PointsController : ApiController
    {
        public ILifetimeScope Container { get; set; }

        [HttpGet]
        [Route("{name}")]
        public HttpResponseMessage GetPoints(string name)
        {
            var request = new RetrievePointsRequest
            {
                ListName = name
            };
            var handler = Container.Resolve<IHandler<RetrievePointsRequest, RetrievePointsResponse>>();
            var result = handler.Handle(request);

            return Request.CreateResponse(HttpStatusCode.Created, result);
        }

        [HttpPut]
        public HttpResponseMessage AddPoints(AddPointsRequest request)
        {
            var handler = Container.Resolve<IHandler<AddPointsRequest, AddPointsResponse>>();
            var result = handler.Handle(request);

            return Request.CreateResponse(HttpStatusCode.Created, result);
        }

        [HttpDelete]
        public HttpResponseMessage DeletePoints(RemovePointsRequest request)
        {
            var handler = Container.Resolve<IHandler<RemovePointsRequest, RemovePointsResponse>>();
            var result = handler.Handle(request);

            return Request.CreateResponse(HttpStatusCode.NoContent, result);
        }
    }
}