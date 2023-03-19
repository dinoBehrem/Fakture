using Fakture.Entities;
using Fakture.Interfaces;
using Fakture.MEF;
using Fakture.Models;
using Fakture.ViewModels.Fakture;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Fakture
{
    public class FakturaService : IFaktureService
    {
        ApplicationDbContext _context = new ApplicationDbContext();
        PorezCalcualtionManager porezCalculation = new PorezCalcualtionManager();

        public void AddFaktura(FakturaInsertVM fakturaInsert)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == fakturaInsert.Username);

            var faktura = new Faktura()
            {
                CijenaBezPoreza = fakturaInsert.CijenaBezPoreza,
                DatumDospijeca = fakturaInsert.DatumDospijeca,
                CijenaSaPorezom = porezCalculation.PriceCalcualtion(fakturaInsert.Porez, fakturaInsert.CijenaBezPoreza),
                DatumKreiranja = DateTime.UtcNow,
                Primatelj = fakturaInsert.Primatelj,
                StvarateljId = user.Id
            };

            _context.Fakture.Add(faktura);
            _context.SaveChanges();
        }
    }
}