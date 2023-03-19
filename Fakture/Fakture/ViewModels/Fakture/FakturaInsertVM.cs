using Fakture.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fakture.ViewModels.Fakture
{
    public class FakturaInsertVM
    {
        public DateTime DatumKreiranja { get; set; }
        public DateTime DatumDospijeca { get; set; }
        public decimal CijenaBezPoreza { get; set; }
        public string Primatelj { get; set; }
        public Porezi Porez { get; set; }

        public List<SelectListItem> Porezi { get; set; }
        public string Username { get; set; }
    }
}