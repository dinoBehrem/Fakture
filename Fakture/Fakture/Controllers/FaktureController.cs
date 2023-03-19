using Fakture.Enums;
using Fakture.MEF;
using Fakture.ViewModels.Fakture;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fakture.Controllers
{
    [Authorize]
    public class FaktureController : Controller
    {
        PorezCalcualtionManager porez = new PorezCalcualtionManager();
        FakturaService fakturaService = new FakturaService();


        // GET: Create
        public ActionResult Create()
        {
            ViewBag.Porez = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = Porezi.BH.ToString(),
                    Value = Porezi.BH.ToString()
                },
                new SelectListItem()
                {
                    Text = Porezi.HR.ToString(),
                    Value = Porezi.HR.ToString()
                },
            };

            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "Porez, DatumKreiranja, DatumDospijeca, CijenaBezPoreza, Primatelj")] FakturaInsertVM faktura)
        {
            ViewBag.Porez = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = Porezi.BH.ToString(),
                    Value = Porezi.BH.ToString()
                },
                new SelectListItem()
                {
                    Text = Porezi.HR.ToString(),
                    Value = Porezi.HR.ToString()
                },
            };

            if (!ValidateFaktura(faktura))
            {
                return View();
            }

            var user =  User.Identity.Name;

            faktura.Username = user;

            fakturaService.AddFaktura(faktura);

            return View();
        }
        
        // GET: Edit
        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(FakturaInsertVM faktura)
        {
            return View();
        }

        #region Utils

        private bool ValidateFaktura(FakturaInsertVM fakturaInsert)
        {
            return ValidDate(fakturaInsert.DatumDospijeca) || fakturaInsert.CijenaBezPoreza > 0;
        }

        private bool ValidDate(DateTime date)
        {
            return date < DateTime.Now;
        }
        
        #endregion Utils
    }
}