using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp;
using Squares.Contracts.Points;
using Squares.Contracts.Points.AddPoints;
using Squares.Contracts.Points.RemovePoint;
using Squares.Contracts.Points.RetrievePoints;

namespace Squares.Integration.Tests.Helpers
{
    public static class PointsHelper
    {
        private static readonly string UriAddress = "points";
        public static IRestResponse AddPoints(this RestClient client, IList<Point> points, string listName)
        {
            var addPointsRequest = new RestRequest(UriAddress);
            addPointsRequest.AddJsonBody(new AddPointsRequest { ListName = listName, Points = points });
            return client.Put(addPointsRequest);
        }

        public static RetrievePointsResponse GetPoints(this RestClient client, string listName, int page = 0, int pageSize = 0)
        {
            var getPointsRequest = new RestRequest($"{UriAddress}/{listName}?pageNumber={page}&pageSize={pageSize}");
            IRestResponse response = client.Get(getPointsRequest);
            return JsonConvert.DeserializeObject<RetrievePointsResponse>(response.Content);
        }

        public static IRestResponse DeletePoints(this RestClient client, List<Point> points, string listName)
        {
            var deletePointsRequest = new RestRequest(UriAddress);
            deletePointsRequest.AddJsonBody(new RemovePointsRequest { ListName = listName, Points = points });
            return client.Delete(deletePointsRequest);
        }
    }
}