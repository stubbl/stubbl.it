using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using stubbl.Filters;
using stubbl.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace stubbl.Controllers
{
    [TeamFilter]
    public class TeamController : Controller
    {
        // GET: /<controller>/
        [Authorize]
        public IActionResult Index()
        {
            if(User.Claims.Any(x=>x.Type=="team"))
                return View();
            return RedirectToAction("Create", "Team");
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(CreateTeamViewModel createTeamViewModel)
        {
            var stubblClient = new StubblClient();

            var teamResponse = await stubblClient.GetTeams();

            if (teamResponse.IsSuccessStatusCode)
            {
                var response = await teamResponse.Content.ReadAsStringAsync();
                var team = JObject.Parse(response);
                HttpContext.Session.SetString("CurrentTeam", team.Value<string>("teamId"));
                return RedirectToAction("Index", "Team");
            }
            return View("Create");
        }
    }
}

