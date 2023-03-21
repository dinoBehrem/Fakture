using Fakture.Models;
using Fakture.Services;
using Fakture.ViewModels.StavkeFakture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace Fakture.Controllers
{
    public class StavkeFaktureController : Controller
    {
        StavkeFaktureService _service = new StavkeFaktureService();

        // GET: StavkeFakture
        [ChildActionOnly]
        public ActionResult Index(int fakturaId)
        {
            var stavkeFakture = _service.DobaviStavkeRacuna(fakturaId);

            var model = new StavkeIndexVM()
            {
                FakturaId = fakturaId,
                Stavke = stavkeFakture
            };

            return PartialView("_Stavke", model);
        }

        public ActionResult Create(int fakturaId)
        {
            var model = new StavkeAddVM()
            {
                FakturaId = fakturaId
            };


            return View("Create", model);
        }

        [HttpPost]
        public ActionResult Create(StavkeAddVM stavkeAdd)
        {
            if (!IsValidModel(stavkeAdd))
            {   
                return View(stavkeAdd);
            }

            var model = _service.DodajStavkuFakture(stavkeAdd);

            return RedirectToAction("Edit", "Fakture", new { id = stavkeAdd.FakturaId });
        }
        
        public ActionResult Edit(int stavkaId)
        {
            var result = _service.DobaviStavku(stavkaId, User.Identity.Name);

            if (!result.IsSuccess)
            {
                return RedirectToAction("Index", "Home");
            }
            
            return View("Edit", result.Data);
        }

        [HttpPost]
        public ActionResult Edit(StavkeAddVM stavkeAdd)
        {
            if (!IsValidModel(stavkeAdd))
            {   
                return View(stavkeAdd);
            }

            var model = _service.IzmjeniStavku(stavkeAdd);

            return RedirectToAction("Edit", "Fakture", new { id = stavkeAdd.FakturaId });
        }

        private bool IsValidModel(StavkeAddVM stavkeAdd)
        {
            return stavkeAdd.Kolicina > 1 || stavkeAdd.CijenaBezPoreza > 1;
        }
    }
}