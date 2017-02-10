using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp;
using Squares.Contracts.Lists.CreateList;
using Squares.Contracts.Lists.RemoveList;
using Squares.Contracts.Lists.RetrieveLists;

namespace Squares.Integration.Tests.Helpers
{
    public static class ListsHelper
    {
        private static readonly Uri UriAddress = new Uri("lists/", UriKind.Relative);

        public static IRestResponse AddList(this RestClient client, string name)
        {
            var addListRequest = new RestRequest(UriAddress);
            addListRequest.AddJsonBody(new CreateListRequest { ListName = name });
            return client.Post(addListRequest);
        }

        public static RetrieveListsResponse GetLists(this RestClient client)
        {
            var getListsRequest = new RestRequest(UriAddress);
            IRestResponse response = client.Get(getListsRequest);
            return JsonConvert.DeserializeObject<RetrieveListsResponse>(response.Content);
        }

        public static IRestResponse DeleteList(this RestClient client, string name)
        {
            var deleteListRequest = new RestRequest(UriAddress);
            deleteListRequest.AddJsonBody(new RemoveListRequest { ListName = name });
            return client.Delete(deleteListRequest);
        }
    }
}