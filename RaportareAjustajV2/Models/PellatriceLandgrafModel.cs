using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RaportareAjustajV2
{
    public class PellatriceLandgrafModel
    {
        public int PellatriceLandgrafModelId { get; set; }
        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }
        [MaxLength(100)]
        public string DataIntroducere { get; set; }
        public double DiametruIntrare { get; set; }
        public double DiametruIesire { get; set; }
        [MaxLength(100)]
        public string Calitate { get; set; }
        [MaxLength(100)]
        public string Sarja { get; set; }
        public int NrBare { get; set; }
        public int Lungime { get; set; }
        public double Masa { get; set; }
    }
}
