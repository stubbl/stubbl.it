using System.Collections.Generic;

namespace stubbl.ViewModels
{
    public class StubListViewModel
    {
        public string CurrentTeamId { get; set; }
        public IEnumerable<Stub> Stubs { get; set; }
        public Stub CurrentStub { get; set; }
    }
}