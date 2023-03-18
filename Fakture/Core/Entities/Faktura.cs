using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    [Table("Fakture")]
    public class Faktura
    {
        public Faktura()
        {
            StavkeFakture = new List<StavkaFakture>();    
            Primatelj = string.Empty;
        }

        public int Id { get; set; }
        public DateTime DatumKreiranja { get; set; }
        public DateTime DatumDospijeca { get; set; }
        public decimal CijenaBezPoreza { get; set; }
        public decimal CijenaSaPorezom { get; set; }
        public string Primatelj { get; set; }

        ICollection<StavkaFakture> StavkeFakture { get; set; }
    }
}
