using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using stubbl.ViewModels;

namespace stubbl.Controllers
{
    public class TeamController:Controller
    {
        private StubblClient _stubblClient;

        public TeamController(StubblClient stubblClient)
        {
            _stubblClient = stubblClient;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(CreateTeamViewModel createTeamViewModel)
        {
            var createTeamResponse = await _stubblClient.CreateTeam(createTeamViewModel.Name);
            if (createTeamResponse.IsSuccessStatusCode)
            {
                var response = await createTeamResponse.Content.ReadAsStringAsync();
                var team = JObject.Parse(response);
                HttpContext.Session.SetString("CurrentTeam", team.Value<string>("teamId"));
                HttpContext.Session.Remove("NoTeam");
                return RedirectToAction("Index", "Stubs");
            }
            return View("Create");
        }
    }
}
