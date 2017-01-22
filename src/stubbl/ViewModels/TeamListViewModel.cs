using System.Collections.Generic;

namespace stubbl.ViewModels
{
    public class TeamListViewModel
    {
        public TeamListItem CurrentTeam { get; set; }
        public IEnumerable<TeamListItem> Teams { get; set; }
    }
}
