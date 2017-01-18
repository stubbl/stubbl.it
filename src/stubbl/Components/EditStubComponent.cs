using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using stubbl.ViewModels;

namespace stubbl.Components
{
    [ViewComponent(Name = "EditStub")]
    public class EditStubComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Stub stub)
        {
            return View(stub);
        }
    }
}
