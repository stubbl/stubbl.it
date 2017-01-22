using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using stubbl.ViewModels;

namespace stubbl.Controllers
{
    public class StubsController : Controller
    {
        private readonly IRetrieveCurrentTeam _retrieveCurrentTeam = null;

        public StubsController(IRetrieveCurrentTeam retrieveCurrentTeam)
        {
            _retrieveCurrentTeam = retrieveCurrentTeam;
        }

        [Authorize]
        public async Task<IActionResult> Index([FromQuery]string stub, [FromQuery]string team)
        {
            var currentTeamId = _retrieveCurrentTeam.GetCurrentTeamId();
            var stubblClient = new StubblClient();
            var stubListViewModel = new StubListViewModel();

            stubListViewModel.CurrentTeamId = currentTeamId;
            var stubsResponse = await stubblClient.GetStubs(new GetStubsRequest()
            {
                TeamId = currentTeamId
            });
            if (stubsResponse.IsSuccessStatusCode)
            {
                var stubs = await stubsResponse.Content.ReadAsStringAsync();
                stubListViewModel.Stubs = JObject.Parse(stubs).GetValue("stubs").ToList().Select(x => JsonConvert.DeserializeObject<Stub>(x.ToString()));
                var currentStub = string.IsNullOrEmpty(stub) ? stubListViewModel.Stubs.First() : stubListViewModel.Stubs.First(x => x.Id == stub);
                stubListViewModel.CurrentStub = currentStub;
            }
            else
            {
                stubListViewModel.CurrentStub = new Stub();
                stubListViewModel.Stubs = new List<Stub>();
            }
            return View(stubListViewModel);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var stubblClient = new StubblClient();
            var stubListViewModel = new StubListViewModel();
            var currentTeamId = _retrieveCurrentTeam.GetCurrentTeamId();
            var stubsResponse = await stubblClient.GetStubs(new GetStubsRequest()
            {
                TeamId = currentTeamId
            });
            var stubs = await stubsResponse.Content.ReadAsStringAsync();
            stubListViewModel.Stubs = JObject.Parse(stubs).GetValue("stubs").ToList().Select(x => JsonConvert.DeserializeObject<Stub>(x.ToString()));
            return View(stubListViewModel);
        }
    }
}

