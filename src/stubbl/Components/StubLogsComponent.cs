using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using stubbl.ViewModels;

namespace stubbl.Components
{
    [ViewComponent(Name = "StubLogs")]
    public class StubLogsComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string teamId)
        {
            return await Task.FromResult<IViewComponentResult>(View(teamId));
        }
    }
}
