using Fakture.Interfaces;
using Fakture.MEF;
using Fakture.Models;
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

        public List<StavkaFaktureVM> DobaviStavkeRacuna(int id, string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == username);
            throw new NotImplementedException();
        }

        public void DodajStavkuFakture(StavkaFaktureVM stavkaFakture)
        {
            throw new NotImplementedException();
        }
    }
}