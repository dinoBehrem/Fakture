using Fakture.Models;
using Fakture.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fakture.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();

        [Authorize]
        public ActionResult Index()
        {
            var user = dbContext.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);

            var fakture = dbContext.Fakture
                .Include("StavkeFakture")
                .Where(f => f.StvarateljId == user.Id)
                .ToList();

            var model = new UserVM()
            {
                Username = user.UserName,
                Email = user.Email,
                Fakture = fakture,
            };

            return View(model);
        }

        [Authorize]
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