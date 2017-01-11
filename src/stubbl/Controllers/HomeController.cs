using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace stubbl.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return !User.Identity.IsAuthenticated ? View() : (IActionResult)RedirectToAction("Index", "Team"); ;
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
