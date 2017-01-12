using System;
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
                ApiUrl = "stubbl.api.stubbl.it/",
                Scheme = "https://"
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
            return await _httpClient.GetAsync("http://stubbl.api.stubbl.it/teams");
        }

        public async Task<HttpResponseMessage> CreateTeam(string teamName)
        {
            return await _httpClient.PostAsync("http://stubbl.api.stubbl.it/teams", new StringContent(JsonConvert.SerializeObject(new { Name = teamName }), Encoding.UTF8, "application/json"));
        }
    }

    public class StubblClientSettings
    {
        public string Scheme { get; set; }
        public string ApiUrl { get; set; }
        public HttpClient HttpClient { get; set; }
        public Uri Uri => new Uri($"{Scheme}{ApiUrl}");
    }
}
