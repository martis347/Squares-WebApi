using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Autofac;
using Squares.Contracts.Points;
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
        public HttpResponseMessage GetPoints(string name, [FromUri] string sort = "asc", [FromUri] int pageSize = 20, [FromUri] int pageNumber = 1)
        {
            var request = new RetrievePointsRequest
            {
                ListName = name,
                SortDirection = sort == "desc" ? ListSortDirection.Descending : ListSortDirection.Ascending,
                PageSize = pageSize,
                PageNumber = pageNumber
            };
            var handler = Container.Resolve<IHandler<RetrievePointsRequest, RetrievePointsResponse>>();
            var result = handler.Handle(request);

            return Request.CreateResponse(HttpStatusCode.Created, result);
        }

        [HttpPut]
        public HttpResponseMessage AddPoints(AddPointsRequest request)
        {
            var handler = Container.Resolve<IHandler<AddPointsRequest, AddPointsResponse>>();

            /*request.Points = new List<Point>
            {
                new Point
                {
                    X = -4,
                    Y = -4
                },
                new Point
                {
                    X = -4,
                    Y = -1
                },
                new Point
                {
                    X = -2,
                    Y = -4
                },
                new Point
                {
                    X = -2,
                    Y = -2
                },
                new Point
                {
                    X = -2,
                    Y = -1
                },
                new Point
                {
                    X = -2,
                    Y = 2
                },
                new Point
                {
                    X = -1,
                    Y = -2
                },
                new Point
                {
                    X = 0,
                    Y = 0
                },
                new Point
                {
                    X = 0,
                    Y = 2
                },
                new Point
                {
                    X = 1,
                    Y = -2
                },
                new Point
                {
                    X = 1,
                    Y = 1
                },
                new Point
                {
                    X = 2,
                    Y = -2
                },
                new Point
                {
                    X = 2,
                    Y = 2
                },
                new Point
                {
                    X = 2,
                    Y = 0
                },
                new Point
                {
                    X = 3,
                    Y = 1
                },
                new Point
                {
                    X = 4,
                    Y = -2
                },
                new Point
                {
                    X = 4,
                    Y = 2
                },
                new Point
                {
                    X = 4,
                    Y = 0
                }
            };
*/
            var result = handler.Handle(request);

            return Request.CreateResponse(HttpStatusCode.Created, result);
        }

        [HttpDelete]
        public HttpResponseMessage DeletePoints(RemovePointsRequest request)
        {
            var handler = Container.Resolve<IHandler<RemovePointsRequest, RemovePointsResponse>>();
            handler.Handle(request);

            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}