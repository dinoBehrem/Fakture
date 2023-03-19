using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;

namespace Fakture.MEF
{
    [Export(typeof(IPorezCalculation))]
    public class HRPorez : IPorezCalculation
    {
        public decimal IzracunajCijenuSaPorezom(decimal cijena)
        {
            return cijena * (decimal)0.25 + cijena;
        }
    }
}