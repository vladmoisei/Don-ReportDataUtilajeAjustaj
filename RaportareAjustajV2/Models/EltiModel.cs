using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RaportareAjustajV2
{

    public class EltiModel
    {

        public int EltiModelId { get; set; }
        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }
        [MaxLength(100)]
        public string DataIntroducere { get; set; }
        public int Cuptor { get; set; }
        public TratamentTermic TratamentTermic { get; set; }
        public int Diametru { get; set; }
        [Required]
        [MaxLength(100)]
        public string Calitate { get; set; }
        [Required]
        [MaxLength(100)]
        public string Sarja { get; set; }
        public int NumarBare { get; set; }
        public int LungimeBare { get; set; }
        public double Masa { get; set; }
        [MaxLength(100)]
        public string DataIncarcare { get; set; }
        [MaxLength(100)]
        public string OraIncarcare { get; set; }
        [MaxLength(100)]
        public string DataDescarcare { get; set; }
        [MaxLength(100)]
        public string OraDescarcare { get; set; }
        public int ConsumGaz { get; set; }
        public int ConsumElectricitate { get; set; }
    }
}
