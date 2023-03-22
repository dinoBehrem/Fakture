using Fakture.Enums;
using Fakture.Models;
using Fakture.ViewModels.StavkeFakture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fakture.ViewModels.Fakture
{
    public class FakturaVM
    {
        public int Id { get; set; }
        public DateTime DatumKreiranja { get; set; }
        public DateTime DatumDospijeca { get; set; }
        public decimal CijenaBezPoreza { get; set; }
        public decimal CijenaSaPorezom { get; set; }
        public string Primatelj { get; set; }
        public Porezi Porez { get; set; }
        public string Kod { get; set; }

        public ApplicationUser Stvaratelj { get; set; }
        public List<StavkaFaktureVM> Stavke { get; set; }
    }
}