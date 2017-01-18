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
    [ViewComponent(Name = "StubLogs")]
    public class StubLogsComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(TeamListItem team)
        {
            return View(team);
        }
    }
}
