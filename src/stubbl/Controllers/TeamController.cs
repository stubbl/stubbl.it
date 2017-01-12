using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using stubbl.ViewModels;

namespace stubbl.Controllers
{
    public class TeamController:Controller
    {
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(CreateTeamViewModel createTeamViewModel)
        {
            var stubblClient = new StubblClient();

            var createTeamResponse = await stubblClient.CreateTeam(createTeamViewModel.Name);

            if (createTeamResponse.IsSuccessStatusCode)
            {
                var response = await createTeamResponse.Content.ReadAsStringAsync();
                var team = JObject.Parse(response);
                HttpContext.Session.SetString("CurrentTeam", team.Value<string>("teamId"));
                return RedirectToAction("Index", "Stubs");
            }
            return View("Create");
        }
    }
}
