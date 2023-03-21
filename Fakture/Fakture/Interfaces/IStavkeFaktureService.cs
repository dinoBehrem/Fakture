using Fakture.ViewModels.Shared;
using Fakture.ViewModels.StavkeFakture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakture.Interfaces
{
    public interface IStavkeFaktureService
    {
        StavkeAddVM DodajStavkuFakture(StavkeAddVM stavkaFakture);
        List<StavkaFaktureVM> DobaviStavkeRacuna(int fakturaId);
        Result<StavkeAddVM> DobaviStavku(int stavkaId, string username);
        StavkeAddVM IzmjeniStavku(StavkeAddVM stavka);
    }
}
