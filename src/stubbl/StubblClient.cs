using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace stubbl
{
    public class StubblClient
    {
        private readonly HttpClient _httpClient;

        public StubblClient()
        {
            var settings = new StubblClientSettings()
            {
                ApiUrl = "stubbl.api.stubbl.it",
                Scheme = "http://"
            };
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri($"{settings.Scheme}{settings.ApiUrl}")
            }; 
        }

        public StubblClient(StubblClientSettings settings, HttpClientHandler handler)
        {
            _httpClient = new HttpClient(handler)
            {
                BaseAddress = settings.Uri
            };
        }

        public async Task<HttpResponseMessage> GetTeams()
        {
            return await _httpClient.GetAsync("teams");
        }

        public async Task<HttpResponseMessage> GetStubs()
        {
            return await GetStubs(new GetStubsRequest());
        }

        public async Task<HttpResponseMessage> GetStubs(GetStubsRequest getStubsRequest)
        {
            var url = "stubs".AddQueryParameter(  new KeyValuePair<string, string>("teamId", getStubsRequest.TeamId), 
                                    new KeyValuePair<string, string>("search", getStubsRequest.Search), 
                                    new KeyValuePair<string, string>("pageNumber", getStubsRequest.PageNumber.ToString()), 
                                    new KeyValuePair<string, string>("pageSize", getStubsRequest.PageSize.ToString()));
            return await _httpClient.GetAsync(url);
        }

        public async Task<HttpResponseMessage> CreateTeam(string teamName)
        {
            return await _httpClient.PostAsync("teams", new StringContent(JsonConvert.SerializeObject(new { Name = teamName }), Encoding.UTF8, "application/json"));
        }
    }

    public class GetStubsRequest
    {
        public string TeamId { get; set; }
        public string Search { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }

    public static class StringExtensions
    {
        public static string AddQueryParameter(this string uri, KeyValuePair<string, string> keyValuePair)
        {
            return uri.AddQueryParameter(keyValuePair.Key, keyValuePair.Value);
        }

        public static string AddQueryParameter(this string uri, string key, string value)
        {
            if (string.IsNullOrEmpty(value))
                return uri;
            var pair = $"{key}={value}";
            if (!uri.Contains("?"))
            {
                return $"{uri.ToString().TrimEnd('/')}?{pair}";
            }
            else
            {
                return $"{uri.ToString().TrimEnd('/')}&{pair}";
            }
        }

        public static string AddQueryParameter(this string uri, params KeyValuePair<string,string>[] parameters )
        {
            foreach (var keyValuePair in parameters)
            {
                uri = uri.AddQueryParameter(keyValuePair);
            }
            return uri;
        }
    }
}
