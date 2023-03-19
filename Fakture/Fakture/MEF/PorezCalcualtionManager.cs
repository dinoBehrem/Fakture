using Fakture.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Fakture.MEF
{
    public class PorezCalcualtionManager
    {
        [ImportMany]
        private readonly IEnumerable<IPorezCalculation> _porezi;

        public PorezCalcualtionManager()
        {
            var catalog = new AssemblyCatalog(typeof(PorezCalcualtionManager).Assembly);
            var container = new CompositionContainer(catalog);
            container.ComposeParts();

            _porezi = container.GetExportedValues<IPorezCalculation>();
        }

        public decimal PriceCalcualtion(Porezi porez, decimal cijena)
        {
            IPorezCalculation porezCalculation = null;

            if (porez == Porezi.BH)
            {
                porezCalculation = GetPorezCalculation(typeof(BHPorez));
            }
            else
            {
                porezCalculation = GetPorezCalculation(typeof(HRPorez));
            }

            return porezCalculation.IzracunajCijenuSaPorezom(cijena);
        }

        private IPorezCalculation GetPorezCalculation(Type type)
        {
            return _porezi.FirstOrDefault(p => p.GetType() == type);
        }
    }
}