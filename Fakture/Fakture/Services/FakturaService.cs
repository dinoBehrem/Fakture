﻿using Fakture.Entities;
using Fakture.Extenesions;
using Fakture.Interfaces;
using Fakture.MEF;
using Fakture.Models;
using Fakture.ViewModels.Fakture;
using Fakture.ViewModels.StavkeFakture;
using System;
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

        public FakturaVM AddFaktura(FakturaInsertVM fakturaInsert)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == fakturaInsert.Username);

            var faktura = new Faktura()
            {
                CijenaBezPoreza = fakturaInsert.CijenaBezPoreza,
                DatumDospijeca = fakturaInsert.DatumDospijeca,
                CijenaSaPorezom = _porezCalculation.PriceCalcualtion(fakturaInsert.Porez, fakturaInsert.CijenaBezPoreza),
                DatumKreiranja = DateTime.UtcNow,
                Primatelj = fakturaInsert.Primatelj,
                StvarateljId = user.Id
            };

            _context.Fakture.Add(faktura);
            _context.SaveChanges();

            return new FakturaVM()
            {
                Id = faktura.Id,
                CijenaBezPoreza = faktura.CijenaBezPoreza,
                CijenaSaPorezom = faktura.CijenaSaPorezom,
                DatumDospijeca = faktura.DatumDospijeca,
                DatumKreiranja = faktura.DatumKreiranja,
                Primatelj = faktura.Primatelj,
                Stvaratelj = faktura.Stvaratelj
            };
        }

        public List<FakturaVM> DobaviFakture(ApplicationUser user)
        {
            var fakture = _context.Fakture
                .Include(f => f.StavkeFakture)
                .Include(f => f.Stvaratelj)
                .Where(f => f.StvarateljId == user.Id)
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

            return fakture;
        }

        public FakturaVM DobaviFakturu(int fakturaId, string username)
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

            faktura.CijenaBezPoreza = faktura.Stavke.Sum(s => s.UkupnaCijena);
            faktura.CijenaBezPoreza = _porezCalculation.PriceCalcualtion(faktura.Porez, faktura.CijenaBezPoreza);

            if (faktura.Stvaratelj.Id != user.Id)
            {
                return null;
            }

            return faktura;
        }
    }
}