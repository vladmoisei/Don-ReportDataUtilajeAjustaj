using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RaportareAjustajV2
{
    public class NovafluxModel
    {
        public int NovafluxModelId { get; set; }
        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }
        [MaxLength(100)]
        public string DataIntroducere { get; set; }
        public int Diametru { get; set; }
        [MaxLength(100)]
        public string Calitate { get; set; }
        [MaxLength(100)]
        public string Sarja { get; set; }
        public double DefectEtalon { get; set; }
        public int NrBareConform { get; set; }
        public double MasaConform { get; set; }
        public int NrBareNeConform { get; set; }
        public double MasaNeConform { get; set; }
    }
}
