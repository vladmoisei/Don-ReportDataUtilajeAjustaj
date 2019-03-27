using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using RaportareAjustajV2.Models;

namespace RaportareAjustajV2.Controllers
{
    public class HomeController : Controller
    {

        RaportareDbContext _context;
        public HomeController(RaportareDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {            
            return View(new UserLogatModel { PrimaAccesare = "da"});
        }

        [HttpPost]
        public async Task<IActionResult> Index(string UserName, string Password)
        {
            var user = await _context.Users
            .FirstOrDefaultAsync(m => m.UserName == UserName);
            if (user == null)
            {
                return View(new UserLogatModel() { NumeUtilizatorGresit = true });                
            }
            else if (user.Password != Password)
                {
                return View(new UserLogatModel() { ParolaGresita = true });
            }

            return RedirectToAction("Cuprins", "Home", user.UserName);




            //return View(post);

            //return RedirectToAction(nameof(Login));
        }

        public IActionResult Cuprins(string UserName)
        {
            string userN = UserName;
            //ViewData["Message"] = "Your application description page.";
            if (userN!= "")
                return View(userN);
            return RedirectToAction("Index");
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
