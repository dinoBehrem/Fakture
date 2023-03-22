using Fakture.Entities;
using Fakture.Extenesions;
using Fakture.Interfaces;
using Fakture.MEF;
using Fakture.Models;
using Fakture.ViewModels.Fakture;
using Fakture.ViewModels.Shared;
using Fakture.ViewModels.StavkeFakture;
using Fakture.ViewModels.User;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Fakture.Services
{
    public class FakturaService : IFaktureService
    {
        ApplicationDbContext _context = new ApplicationDbContext();
        PorezCalcualtionManager _porezCalculation = new PorezCalcualtionManager();

        public Result<FakturaVM> AddFaktura(FakturaInsertVM fakturaInsert)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == fakturaInsert.Username);

            var count = _context.Fakture.Where(f => f.DatumKreiranja.Year == DateTime.UtcNow.Year).Count();

            var faktura = new Faktura()
            {
                CijenaBezPoreza = fakturaInsert.CijenaBezPoreza,
                DatumDospijeca = fakturaInsert.DatumDospijeca,
                //CijenaSaPorezom = _porezCalculation.PriceCalcualtion(fakturaInsert.Porez, fakturaInsert.CijenaBezPoreza),
                DatumKreiranja = DateTime.UtcNow,
                Primatelj = fakturaInsert.Primatelj,
                StvarateljId = user.Id,
                Kod = $"F-{DateTime.UtcNow:yy}/{count}",
                Porez = fakturaInsert.Porez
            };

            _context.Fakture.Add(faktura);
            _context.SaveChanges();

            var result = new Result<FakturaVM>()
            {
                Data = new FakturaVM()
                {
                    Id = faktura.Id,
                    CijenaBezPoreza = faktura.CijenaBezPoreza,
                    CijenaSaPorezom = faktura.CijenaSaPorezom,
                    DatumDospijeca = faktura.DatumDospijeca,
                    DatumKreiranja = faktura.DatumKreiranja,
                    Primatelj = faktura.Primatelj,
                    Stvaratelj = faktura.Stvaratelj,
                    Kod = faktura.Kod
                },
                Errors = new List<string>() { },
                IsSuccess = true,
                Message = "Uspjesno dodavanje",
                Status = "OK"
            };

            return result;
        }

        public Result<UserVM> DobaviFakture(string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == username);

            var fakture = _context.Fakture
                .Include(f => f.StavkeFakture)
                .Include(f => f.Stvaratelj)
                .Where(f => f.StvarateljId == user.Id)
                .Select(f => new FakturaVM()
                {
                    Id = f.Id,
                    DatumDospijeca = f.DatumDospijeca,
                    DatumKreiranja = f.DatumKreiranja,
                    Kod = f.Kod,
                    Primatelj = f.Primatelj,
                    Stvaratelj = f.Stvaratelj,
                    Porez = f.Porez,
                    Stavke = f.StavkeFakture.Select(sf => new StavkaFaktureVM()
                    {
                        CijenaBezPoreza = sf.CijenaBezPoreza,
                        Kolicina = sf.Kolicina,
                        Opis = sf.Opis,
                        UkupnaCijena = sf.Kolicina * sf.CijenaBezPoreza
                    }).ToList()
                })
                .ToList();

            foreach(var faktura in fakture)
            {
                faktura.CijenaBezPoreza = faktura.Stavke == null ? 0 : Math.Round(faktura.Stavke.Sum(f => f.UkupnaCijena),2);
                faktura.CijenaSaPorezom = _porezCalculation.PriceCalcualtion(faktura.Porez, faktura.CijenaBezPoreza);
            }

            var korisnickeFakture = new UserVM()
            {
                Email = user.Email,
                Fakture = fakture,
                Username = user.UserName
            };

            var result = new Result<UserVM>()
            {
                IsSuccess = true,
                Message = "Fakture uspjesno dobavljene!",
                Status = "OK",
                Data = korisnickeFakture,
            };

            return result;
        }

        public Result<FakturaVM> DobaviFakturu(int fakturaId, string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == username);

            var faktura = _context.Fakture
                .Include(f => f.StavkeFakture)
                .Include(f => f.Stvaratelj)
                .Where(f => f.Id == fakturaId)
                .Select(f => new FakturaVM()
                {
                    Id = f.Id,
                    DatumDospijeca = f.DatumDospijeca,
                    DatumKreiranja = f.DatumKreiranja,
                    Primatelj = f.Primatelj,
                    Stvaratelj = f.Stvaratelj,
                    Porez = f.Porez,
                    Stavke = f.StavkeFakture.Select(sf => new StavkaFaktureVM()
                    {   
                        Id = sf.Id,
                        CijenaBezPoreza = sf.CijenaBezPoreza,
                        Kolicina = sf.Kolicina,
                        Opis = sf.Opis,
                        UkupnaCijena = sf.Kolicina * sf.CijenaBezPoreza
                    }
                    ).ToList()
                })
                .FirstOrDefault();

            if (faktura.Stvaratelj.Id != user.Id)
            {
                return new Result<FakturaVM>()
                {
                    IsSuccess = false,
                    Message = "Nema permisije",
                    Status = "NOT_FOUND",
                    Errors = new List<string>() { "Faktura nije pronadjena!"}
                };              
            }

            faktura.CijenaBezPoreza = faktura.Stavke.Sum(s => s.UkupnaCijena);
            faktura.CijenaBezPoreza = _porezCalculation.PriceCalcualtion(faktura.Porez, faktura.CijenaBezPoreza);

            var result = new Result<FakturaVM>()
            {
                IsSuccess = true,
                Message = "Faktura pronadjena",
                Status = "OK",
                Errors = new List<string>() { },
                Data = faktura
            };

            return result;
        }

        public Result<FakturaVM> IzmjeniFakturu(FakturaVM fakturaVM)
        {
            var entity = _context.Fakture.Find(fakturaVM.Id);

            entity.DatumDospijeca = fakturaVM.DatumDospijeca;
            entity.Primatelj = fakturaVM.Primatelj;
            entity.Porez = fakturaVM.Porez;

            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();

            var result = new Result<FakturaVM>()
            {
                IsSuccess = true,
                Message = "Faktura pronadjena",
                Status = "OK",
                Errors = new List<string>() { },
                Data = fakturaVM
            };

            return result;
        }
    }
}