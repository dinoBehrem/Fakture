using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fakture.ViewModels.StavkeFakture
{
    public class StavkeIndexVM
    {
        public int FakturaId { get; set; }
        public List<StavkaFaktureVM> Stavke { get; set; }
    }
}