using Fakture.Enums;
using Fakture.MEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fakture.Extenesions
{
    public static class PriceCalculationExtension
    {
        public static decimal CalculatePrice<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector, Porezi porez, decimal cijena)
        {
            PorezCalcualtionManager _porezCalculation = new PorezCalcualtionManager();

            return _porezCalculation.PriceCalcualtion(porez, cijena);
        }
    }
}