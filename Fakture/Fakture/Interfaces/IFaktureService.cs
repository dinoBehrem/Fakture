using Fakture.Models;
using Fakture.ViewModels.Fakture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakture.Interfaces
{
    public interface IFaktureService
    {
        FakturaVM AddFaktura(FakturaInsertVM fakturaInsert);
        FakturaVM DobaviFakturu(int fakturaId, string username);
        List<FakturaVM> DobaviFakture(ApplicationUser user);
    }
}
