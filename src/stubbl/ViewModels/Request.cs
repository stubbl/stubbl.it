using System.Collections.Generic;

namespace stubbl.ViewModels
{
    public class Request
    {
        public List<BodyToken> BodyTokens { get; set; }
        public List<Header> Headers { get; set; }
        public string Method { get; set; }
        public string Path { get; set; }
        public List<QuerystringParameter> QueryStringParameters { get; set; }
    }
}