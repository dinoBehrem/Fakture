using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fakture.Controllers
{
    public class StavkeFaktureController : Controller
    {
        // GET: StavkeFakture
        public ActionResult Index()
        {
            return View("_Index");
        }
    }
}