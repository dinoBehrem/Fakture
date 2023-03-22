using Fakture.Entities;
using Fakture.ViewModels.Fakture;
using Fakture.ViewModels.Shared;
using Fakture.ViewModels.User;

namespace Fakture.Interfaces
{
    public interface IFaktureService
    {
        Result<FakturaVM> AddFaktura(FakturaInsertVM fakturaInsert);
        Result<FakturaVM> IzmjeniFakturu(FakturaVM fakturaVM);
        Result<FakturaVM> DobaviFakturu(int fakturaId, string username);
        Result<UserVM> DobaviFakture(string username);
    }
}
