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
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var stubblClient = new StubblClient();
            var teamResponse = await stubblClient.GetTeams();
            if (teamResponse.IsSuccessStatusCode)
            {
                var teams = JObject.Parse(await teamResponse.Content.ReadAsStringAsync());
                Teams = teams.GetValue("teams").ToList().Select(x => new TeamListItem
                {
                    Id = x.Value<string>("id"),
                    Name = x.Value<string>("name"),
                    Role = x.Value<string>("role")
                });
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("CurrentTeam")))
                    HttpContext.Session.SetString("CurrentTeam", Teams.First().Id);
                return View(Teams);
            }
            return View();
        }

        public IEnumerable<TeamListItem> Teams { get; set; }
    }
}
