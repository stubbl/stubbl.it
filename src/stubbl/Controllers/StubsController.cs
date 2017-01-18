using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.ProjectModel;
using stubbl.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace stubbl.Controllers
{
    public class StubsController : Controller
    {

        [Authorize]
        public async Task<IActionResult> Index([FromQuery]string stub, [FromQuery]string team)
        {
            var stubblClient = new StubblClient();
            var teamResponse = await stubblClient.GetTeams();
            var stubListViewModel = new StubListViewModel();
            if (teamResponse.IsSuccessStatusCode)
            {
                var teams = JObject.Parse(await teamResponse.Content.ReadAsStringAsync());
                stubListViewModel.Teams = teams.GetValue("teams").ToList().Select(x => new TeamListItem
                {
                    Id = x.Value<string>("id"),
                    Name = x.Value<string>("name"),
                    Role = x.Value<string>("role")
                });
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("CurrentTeam")))
                    HttpContext.Session.SetString("CurrentTeam", stubListViewModel.Teams.First().Id);
                var currentTeamId = string.IsNullOrEmpty(team) ? HttpContext.Session.GetString("CurrentTeam") : team;
                var stubsResponse = await stubblClient.GetStubs(new GetStubsRequest()
                {
                    TeamId = currentTeamId
                });
                stubListViewModel.CurrentTeam = stubListViewModel.Teams.First(x => x.Id == currentTeamId);
                var stubs = await stubsResponse.Content.ReadAsStringAsync();
                stubListViewModel.Stubs = JObject.Parse(stubs).GetValue("stubs").ToList().Select(x => JsonConvert.DeserializeObject<Stub>(x.ToString()));
                var currentStub = string.IsNullOrEmpty(stub) ? stubListViewModel.Stubs.First() : stubListViewModel.Stubs.First(x => x.Id == stub);
                stubListViewModel.CurrentStub = currentStub;
                return View(stubListViewModel);
            }
            return RedirectToAction("Create", "Team");
        }

        public async Task<IActionResult> Edit(string id)
        {
            var stubblClient = new StubblClient();
            var teamResponse = await stubblClient.GetTeams();
            var stubListViewModel = new StubListViewModel();
            if (teamResponse.IsSuccessStatusCode)
            {
                var teams = JObject.Parse(await teamResponse.Content.ReadAsStringAsync());
                stubListViewModel.Teams = teams.GetValue("teams").ToList().Select(x => new TeamListItem
                {
                    Id = x.Value<string>("id"),
                    Name = x.Value<string>("name"),
                    Role = x.Value<string>("role")
                });
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("CurrentTeam")))
                    HttpContext.Session.SetString("CurrentTeam", stubListViewModel.Teams.First().Id);
                var currentTeamId = HttpContext.Session.GetString("CurrentTeam");
                var stubsResponse = await stubblClient.GetStubs(new GetStubsRequest()
                {
                    TeamId = currentTeamId
                });
                stubListViewModel.CurrentTeam = stubListViewModel.Teams.First(x => x.Id == currentTeamId);
                var stubs = await stubsResponse.Content.ReadAsStringAsync();
                stubListViewModel.Stubs = JObject.Parse(stubs).GetValue("stubs").ToList().Select(x => JsonConvert.DeserializeObject<Stub>(x.ToString()));
                return View(stubListViewModel);
            }
            return RedirectToAction("Create", "Team");
        }
    }
}

