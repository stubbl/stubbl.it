using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace stubbl
{
    public class StubblClient
    {
        private string _scheme;
        private string _stubblApiUrl;
        private HttpClient _httpClient;

        public StubblClient()
        {
            _scheme = "https://";
            _stubblApiUrl = "stubbl.api.stubbl.it/";
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri($"{_scheme}{_stubblApiUrl}")
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
}
