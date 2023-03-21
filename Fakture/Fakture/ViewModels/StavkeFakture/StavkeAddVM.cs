using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fakture.ViewModels.StavkeFakture
{
    public class StavkeAddVM
    {
        public int Id { get; set; }
        public string Opis { get; set; }
        public int Kolicina { get; set; }
        public decimal CijenaBezPoreza { get; set; }
        public int FakturaId { get; set; }
    }
}