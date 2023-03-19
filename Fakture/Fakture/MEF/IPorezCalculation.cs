using Fakture.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakture.MEF
{
    public interface IPorezCalculation
    {
        decimal IzracunajCijenuSaPorezom(decimal cijena);
    }
}
