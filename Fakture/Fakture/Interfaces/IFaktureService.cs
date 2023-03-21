using Fakture.Models;
using Fakture.ViewModels.Fakture;
using Fakture.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakture.Interfaces
{
    public interface IFaktureService
    {
        Result<FakturaVM> AddFaktura(FakturaInsertVM fakturaInsert);
        Result<FakturaVM> DobaviFakturu(int fakturaId, string username);
        Result<List<FakturaVM>> DobaviFakture(ApplicationUser user);
    }
}
