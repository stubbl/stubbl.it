using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using stubbl.ViewModels;

namespace stubbl.Components
{
    [ViewComponent(Name = "TeamsList")]
    public class TeamsListComponent : ViewComponent
    {
        private IRetrieveCurrentTeam _retrieveCurrentTeam;
        private StubblClient _stubblClient;

        public TeamsListComponent(IRetrieveCurrentTeam retrieveCurrentTeam, StubblClient stubblClient)
        {
            _retrieveCurrentTeam = retrieveCurrentTeam;
            _stubblClient = stubblClient;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var teamResponse = await _stubblClient.GetTeams();
            if (teamResponse.IsSuccessStatusCode)
            {
                var teams = JObject.Parse(await teamResponse.Content.ReadAsStringAsync());
                Teams = teams.GetValue("teams").ToList().Select(x => new TeamListItem
                {
                    Id = x.Value<string>("id"),
                    Name = x.Value<string>("name"),
                    Role = x.Value<string>("role")
                });
                
                return View(new TeamListViewModel
                {
                    CurrentTeam = Teams.FirstOrDefault(x => x.Id == _retrieveCurrentTeam.GetCurrentTeamId()),
                    Teams = Teams
                });
            }
            return View();
        }

        public IEnumerable<TeamListItem> Teams { get; set; }
    }
}
