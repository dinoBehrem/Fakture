using Fakture.Enums;
using Fakture.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakture.Entities
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
        public string Kod { get; set; }
        public DateTime DatumKreiranja { get; set; }
        public DateTime DatumDospijeca { get; set; }
        public decimal CijenaBezPoreza { get; set; }
        public decimal CijenaSaPorezom { get; set; }
        public string Primatelj { get; set; }
        public Porezi Porez { get; set; }
        public string StvarateljId { get; set; }
        public ApplicationUser Stvaratelj { get; set; }

        public ICollection<StavkaFakture> StavkeFakture { get; set; }
    }
}
