using Fakture.ViewModels.StavkeFakture;
using Fakture.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fakture.ViewModels.Fakture
{
    public class FaktureUpdateVM
    {
        public int Id { get; set; }
        public DateTime DatumKreiranja { get; set; }
        public DateTime DatumDospijeca { get; set; }
        public decimal CijenaBezPoreza { get; set; }
        public string Primatelj { get; set; }
        public UserVM Stvaratelj { get; set; }
        public ICollection<StavkaFaktureVM> MyProperty { get; set; }
    }
}