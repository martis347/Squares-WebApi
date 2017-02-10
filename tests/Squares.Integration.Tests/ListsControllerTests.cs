using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using Squares.Contracts.Lists.RetrieveLists;
using Squares.Integration.Tests.Helpers;

namespace Squares.Integration.Tests
{
    [TestFixture]
    public class TestListsController
    {
        private RestClient _client;
        private readonly string _baseUrl = ConfigurationManager.AppSettings["WebApiServiceUrl"];

        [OneTimeSetUp]
        public void Setup()
        {
            StartWebApi.Start(_baseUrl);
            _client = new RestClient { BaseUrl = new Uri(_baseUrl) };
        }

        [TearDown]
        public void Dispose()
        {
            var lists = _client.GetLists();
            foreach (var list in lists.ListNames)
            {
                _client.DeleteList(list);
            }
        }

        [Test]
        public void AddOneListTest()
        {
            RetrieveListsResponse expectedJsonResult = new RetrieveListsResponse
            {
                ListNames = new List<string> { "Test" }
            };

            _client.AddList("Test");
            var jsonResult = _client.GetLists();

            Assert.AreEqual(expectedJsonResult.ListNames, jsonResult.ListNames);
        }

        [Test]
        public void AddMultipleListsTest()
        {
            RetrieveListsResponse expectedJsonResult = new RetrieveListsResponse { ListNames = new List<string>() };

            for (int i = 0; i < 10; i++)
            {
                _client.AddList($"Test-{i}");
                expectedJsonResult.ListNames.Add($"Test-{i}");
            }
            var jsonResult = _client.GetLists();

            Assert.AreEqual(expectedJsonResult.ListNames, jsonResult.ListNames);
        }

        [Test]
        public void DeleteOneListTest()
        {
            string listToDelete = "Test-5";

            List<string> lists = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                _client.AddList($"Test-{i}");
                lists.Add($"Test-{i}");
            }
            _client.DeleteList(listToDelete);
            RetrieveListsResponse jsonResult = _client.GetLists();

            Assert.AreEqual(lists.Except(new List<string> { listToDelete }), jsonResult.ListNames);
        }

        [Test]
        public void DeleteMultipleListsTest()
        {
            List<string> lists = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                lists.Add($"Test-{i}");
                _client.AddList($"Test-{i}");
            }

            var listsToDelete = new List<string>();
            for (int i = 0; i <= 3; i++)
            {
                listsToDelete.Add($"Test-{i*3}");
                _client.DeleteList($"Test-{i * 3}");
            }
            
            RetrieveListsResponse jsonResult = _client.GetLists();

            Assert.AreEqual(lists.Except(listsToDelete), jsonResult.ListNames);
        }

        [Test]
        public void AddTwoSameListsTest()
        {
            _client.AddList("Test");
            var response = _client.AddList("Test");
            dynamic jsonResult = JsonConvert.DeserializeObject<dynamic>(response.Content);

            Assert.AreEqual("List with given name already exists.", jsonResult.Message.Value);
            Assert.AreEqual("listExists", jsonResult.Reason.Value);
        }

        [Test]
        public void DeleteNonExistingListTest()
        {
            var response = _client.DeleteList("Test");
            dynamic jsonResult = JsonConvert.DeserializeObject<dynamic>(response.Content);

            Assert.AreEqual("List with given name does not exist.", jsonResult.Message.Value);
            Assert.AreEqual("listNotFound", jsonResult.Reason.Value);
        }
    }
}
