using System;
using System.Linq;
using System.Threading.Tasks;

namespace stubbl.ViewModels
{
    public class Stub
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Request Request { get; set; }
        public Response Response { get; set; }
        public string[] Tags { get; set; }
        public string TeamId { get; set; }
    }
}
