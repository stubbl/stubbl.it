using System;
using System.Net.Http;

namespace stubbl
{
    public class StubblClientSettings
    {
        public string Scheme { get; set; }
        public string ApiUrl { get; set; }
        public HttpClient HttpClient { get; set; }
        public Uri Uri => new Uri($"{Scheme}{ApiUrl}");
    }
}