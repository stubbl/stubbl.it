using System.Collections.Generic;

namespace stubbl.ViewModels
{
    public class Response
    {
        public string Body { get; set; }
        public List<Header> Headers { get; set; }
        public int StatusCode { get; set; }
    }
}