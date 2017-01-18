using System.Collections.Generic;

namespace stubbl.ViewModels
{
    public class StubListViewModel
    {
        public IEnumerable<TeamListItem> Teams { get; set; } = new List<TeamListItem>();
        public TeamListItem CurrentTeam { get; set; }
        public IEnumerable<Stub> Stubs { get; set; }
        public Stub CurrentStub { get; set; }
    }
}