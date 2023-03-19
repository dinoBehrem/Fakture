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
        void AddFaktura(FakturaInsertVM fakturaInsert);
    }
}
