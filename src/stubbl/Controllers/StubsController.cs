using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using stubbl.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace stubbl.Controllers
{
    public class StubsController : Controller
    {
        // GET: /<controller>/
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var stubblClient = new StubblClient();
            var teamResponse = await stubblClient.GetTeams();
            var stubListViewModel = new StubListViewModel();
            if (teamResponse.IsSuccessStatusCode)
            {
                var teams = JObject.Parse(await teamResponse.Content.ReadAsStringAsync());
                teams.GetValue("teams").ToList().ForEach(x =>
                {
                    stubListViewModel.Teams.Add(new TeamListItem
                    {
                        Id = x.Value<string>("id"),
                        Name = x.Value<string>("name"),
                        Role = x.Value<string>("role")
                    });
                });
                return View(stubListViewModel);
            }
            return RedirectToAction("Create", "Team");
        }
    }

    public class StubListViewModel
    {
        public List<TeamListItem> Teams { get; set; } = new List<TeamListItem>();
        public TeamListItem CurrentTeam { get; set; }
    }
}

