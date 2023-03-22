using Fakture.Enums;
using Fakture.MEF;
using Fakture.Models;
using Fakture.Services;
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
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Porez, DatumKreiranja, DatumDospijeca, CijenaBezPoreza, Primatelj")] FakturaInsertVM fakturaInsert)
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

            if (!ValidateFaktura(fakturaInsert))
            {
                return View();
            }

            fakturaInsert.Username = User.Identity.Name;

            var result =  fakturaService.AddFaktura(fakturaInsert);

            if (!result.IsSuccess)
            {
                return View();
            }

            return RedirectToAction(nameof(Edit), new { id = result.Data.Id });
        }
        
        // GET: Details
        public ActionResult Details(int id)
        {
            var faktura = fakturaService.DobaviFakturu(id, User.Identity.Name);

            if (!faktura.IsSuccess)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(faktura.Data);
        }

        
        public ActionResult Edit(int id)
        {
            var faktura = fakturaService.DobaviFakturu(id, User.Identity.Name);

            if (!faktura.IsSuccess)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Porez = new SelectList(new List<SelectListItem>()
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
            }, "Value", "Text", faktura.Data.Porez);

            return View(faktura.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Porez, DatumKreiranja, DatumDospijeca, CijenaBezPoreza, Primatelj")] FakturaVM fakturaVM)
        {
            if (!ValidDate(fakturaVM.DatumDospijeca))
            {
                return RedirectToAction("Edit", fakturaVM.Id);
            }

            var faktura = fakturaService.IzmjeniFakturu(fakturaVM);

            if (!faktura.IsSuccess)
            {
                return RedirectToAction("Edit", fakturaVM.Id);
            }

            var result = fakturaService.DobaviFakturu(fakturaVM.Id, User.Identity.Name);

            ViewBag.Porez = new SelectList(new List<SelectListItem>()
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
            }, "Value", "Text", faktura.Data.Porez);

            return View(result.Data);
        }

        #region Utils

        private bool ValidateFaktura(FakturaInsertVM fakturaInsert)
        {
            return ValidDate(fakturaInsert.DatumDospijeca);
        }

        private bool ValidDate(DateTime date)
        {
            return date > DateTime.Now;
        }
        
        #endregion Utils
    }
}