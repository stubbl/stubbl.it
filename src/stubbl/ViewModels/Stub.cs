using System.Collections.Generic;

namespace stubbl.ViewModels
{
    public class Stub
    {
        public Stub()
        {
            Request = new Request
            {
                BodyTokens = new List<BodyToken>(),
                Headers = new List<Header>(),
                QueryStringParameters = new List<QuerystringParameter>()
            };
            Response = new Response
            {
                Headers = new List<Header>(),
            };
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public Request Request { get; set; }
        public Response Response { get; set; }
        public string[] Tags { get; set; }
        public string TeamId { get; set; }
    }
}
