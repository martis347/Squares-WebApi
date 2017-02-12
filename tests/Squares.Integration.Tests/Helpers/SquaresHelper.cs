using Newtonsoft.Json;
using RestSharp;
using Squares.Contracts.Squares.RetrieveSquares;

namespace Squares.Integration.Tests.Helpers
{
    public static class SquaresHelper
    {
        private static readonly string UriAddress = "squares";

        public static RetrieveSquaresResponse GetSquares(this RestClient client, string listName, int page = 0, int pageSize = 0)
        {
            var getPointsRequest = new RestRequest($"{UriAddress}/{listName}?pageNumber={page}&pageSize={pageSize}");
            IRestResponse response = client.Get(getPointsRequest);
            return JsonConvert.DeserializeObject<RetrieveSquaresResponse>(response.Content);
        }
    }
}