using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using TacoFinder.Models;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace TacoFinder.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetToken()
        {
            var client = new RestClient("https://api.yelp.com/");
            var request = new RestRequest("oauth2/token", Method.POST);
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("client_id", EnvironmentVariables.AppId);
            request.AddParameter("client_secret", EnvironmentVariables.AppSecret);
            var response = new RestResponse();
            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();
            JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);
            return Json(jsonResponse);
        }

        public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response => {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }

        public JObject GetTacoSpots(string location)
        {
            var client = new RestClient("https://api.yelp.com/");
            var request = new RestRequest("v3/businesses/search");
            request.AddParameter("term", "tacos");
            request.AddParameter("location", location);
            //client.Authenticator = new HttpBasicAuthenticator(EnvironmentVariables.AppId, EnvironmentVariables.AppToken);
            request.AddHeader("Authorization", "Bearer " + EnvironmentVariables.AppToken);
            //client.AddDefaultHeader("Authorization", "Bearer " + EnvironmentVariables.AppToken);
            var response = new RestResponse();
            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();
            JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);
            return jsonResponse;
        }
    }
}
