using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace stubbl.it.Views.Team
{
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
    }
}

