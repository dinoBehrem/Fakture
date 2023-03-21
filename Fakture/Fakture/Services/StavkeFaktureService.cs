using Fakture.Entities;
using Fakture.Interfaces;
using Fakture.MEF;
using Fakture.Models;
using Fakture.ViewModels.Shared;
using Fakture.ViewModels.StavkeFakture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fakture.Services
{
    public class StavkeFaktureService : IStavkeFaktureService
    {
        ApplicationDbContext _context = new ApplicationDbContext();
        PorezCalcualtionManager porezCalculation = new PorezCalcualtionManager();

        public List<StavkaFaktureVM> DobaviStavkeRacuna(int id)
        {
            var stavke = _context.StavkeFakture
                .Where(s => s.FakturaId == id)
                .Select(s => new StavkaFaktureVM()
                {
                    Id = s.Id,
                    Opis = s.Opis,
                    Kolicina = s.Kolicina,
                    CijenaBezPoreza = s.CijenaBezPoreza,
                    UkupnaCijena = s.CijenaBezPoreza * s.Kolicina
                })
                .ToList();

            return stavke;
        }

        public Result<StavkeAddVM> DobaviStavku(int stavkaId, string username)
        {
            var user = _context.Users.Where(u => u.UserName == username).FirstOrDefault();

            var stavka = _context.StavkeFakture
                .Include("Faktura")
                .Where(s => s.Id == stavkaId)
                .FirstOrDefault();

            var model = new StavkeAddVM()
            {
                Id = stavka.Id,
                CijenaBezPoreza = stavka.CijenaBezPoreza,
                Kolicina = stavka.Kolicina,
                Opis = stavka.Opis,
                FakturaId = stavka.FakturaId,
            };

            if (user == null || stavka.Faktura.StvarateljId != user.Id)
            {
                return new Result<StavkeAddVM>()
                {
                    IsSuccess = false,
                    Message = "Nemate permisije",
                    Status = "NOT_FOUND",
                    Errors = new List<string>() { "Stavka nije pronadjena!w"},
                    Data = model
                };
            }

            var result = new Result<StavkeAddVM>()
            {
                IsSuccess = true,
                Message = "Uspjesno dobavljanje stavke!",
                Status = "OK",
                Data = model
            };

            return result;
        }

        public StavkeAddVM DodajStavkuFakture(StavkeAddVM stavkaFakture)
        {
            _context.StavkeFakture.Add(new StavkaFakture()
            {
                Kolicina = stavkaFakture.Kolicina,
                CijenaBezPoreza = stavkaFakture.Kolicina,
                Opis = stavkaFakture.Opis,
                UkupnaCijena = stavkaFakture.CijenaBezPoreza * stavkaFakture.Kolicina,
                FakturaId = stavkaFakture.FakturaId
            });

            _context.SaveChanges();

            return stavkaFakture;
        }

        public StavkeAddVM IzmjeniStavku(StavkeAddVM stavka)
        {
            var entity = _context.StavkeFakture.Find(stavka.Id);

            entity.Kolicina = stavka.Kolicina;
            entity.CijenaBezPoreza = stavka.CijenaBezPoreza;
            entity.UkupnaCijena = stavka.CijenaBezPoreza * stavka.Kolicina;
            entity.Opis = stavka.Opis;

            _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();

            return stavka;
        }
    }
}