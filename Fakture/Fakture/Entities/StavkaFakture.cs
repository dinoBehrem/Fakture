using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Fakture.Entities
{
    [Table("StavkeFakture")]
    public class StavkaFakture
    {
        public int Id { get; set; }
        public string Opis{ get; set; }
        public int Kolicina{ get; set; }
        public decimal CijenaBezPoreza { get; set; }
        public decimal UkipnaCijenaSaPorezom { get; set; }

        public int FakturaId { get; set; }
        public Faktura Faktura { get; set; }
    }
}
