using Fakture.Models;
using Fakture.Services;
using Fakture.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fakture.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();
        FakturaService _fakturaService = new FakturaService();
        
        public ActionResult Index()
        {
            var user = dbContext.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);

            var fakture = _fakturaService.DobaviFakture(user);

            var model = new UserVM()
            {
                Username = user.UserName,
                Email = user.Email,
                Fakture = fakture,
            };

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}