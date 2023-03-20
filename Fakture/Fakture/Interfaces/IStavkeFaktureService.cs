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
        void DodajStavkuFakture(StavkaFaktureVM stavkaFakture);
        List<StavkaFaktureVM> DobaviStavkeRacuna(int id, string username);
    }
}
