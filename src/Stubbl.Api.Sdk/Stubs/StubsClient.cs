
namespace Stubbl.Api.Sdk.Stubs
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Stubbl.Api.Sdk.Stubs.Delete;
    using Stubbl.Api.Sdk.Stubs.Read;
    using Stubbl.Api.Sdk.Stubs.Update;
    using Stubbl.Api.Sdk.Stubs.Create;
    using System.Security.Claims;
    using System.Linq;
    public class StubsClient
    {
        private readonly StubsClientSettings _settings;
        private readonly HttpClient _httpClient;
        private string StubUri => $"{_settings.Uri}/{_settings.Version}/{StubsConstants.StubEndPoint}";
        public StubsClient(StubsClientSettings settings, HttpClient httpClient = null)
        {
            _settings = settings;
            _httpClient = httpClient ?? new HttpClient();
        }
        //public async Task<UpdateResponse> UpdateAsync(UpdateRequest request)
        //{
        //    var httpResponse = await _httpClient.SendAsync(CreateRequest(HttpMethod.Post, StubUri, request));
        //    return await Deserialise<UpdateResponse>(httpResponse);
        //}
        public async Task<ReadResponse> GetListAsync(ReadRequest request, ClaimsIdentity identity)
        {
            var httpResponse = await _httpClient.SendAsync(CreateRequest(HttpMethod.Post, StubUri, request,identity));
            return await Deserialise<ReadResponse>(httpResponse);
        }
        //public async Task<ReadResponse> GetAsync(ReadRequest request)
        //{
        //    var httpResponse = await _httpClient.SendAsync(CreateRequest(HttpMethod.Post, StubUri, request));
        //    return await Deserialise<ReadResponse>(httpResponse);
        //}
        //public async Task<DeleteResponse> DeleteAsync(DeleteRequest request)
        //{
        //    var httpResponse = await _httpClient.SendAsync(CreateRequest(HttpMethod.Post, StubUri, request));
        //    return await Deserialise<DeleteResponse>(httpResponse);
        //}

        private async Task<T> Deserialise<T>(HttpResponseMessage httpResponse)
        {
            return JsonConvert.DeserializeObject<T>(await httpResponse.Content.ReadAsStringAsync());
        }

        private string Serialise<T>(T data)
        {
            return JsonConvert.SerializeObject(data);
        }

        private HttpRequestMessage CreateRequest<T>(HttpMethod method, string uri, T request,ClaimsIdentity identity)
        {
            var httpRequest = new HttpRequestMessage(method, uri)
            {
                Content = new StringContent(Serialise(request))
            };
            httpRequest.Headers.Add("Authorize", $"Bearer {identity.Claims.First(x=>x.Type == "id_token").Value}");
            return httpRequest;
        }
    }
}
