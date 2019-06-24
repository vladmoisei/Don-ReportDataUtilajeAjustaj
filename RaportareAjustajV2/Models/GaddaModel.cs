using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RaportareAjustajV2
{
    public class GaddaModel
    {
        public int GaddaModelId { get; set; }
        [Required]
        [MaxLength(100)]
        [DisplayName("User Name")]
        public string UserName { get; set; }
        [MaxLength(100)]
        [DisplayName("Data Introducere")]
        public string DataIntroducere { get; set; }
        public int Cuptor { get; set; }
        [DisplayName("Tratament Termic")]
        public TratamentTermic TratamentTermic { get; set; }
        public int Diametru { get; set; }
        [Required]
        [MaxLength(100)]
        public string Calitate { get; set; }
        [Required]
        [MaxLength(100)]
        public string Sarja { get; set; }
        [DisplayName("Numar Bare")]
        public int NumarBare { get; set; }
        [DisplayName("Lungime Bare")]
        public int LungimeBare { get; set; }
        public double Masa { get; set; }
        [MaxLength(100)]
        [DisplayName("Data Incarcare")]
        public string DataIncarcare { get; set; }
        [MaxLength(100)]
        [DisplayName("Ora Incarcare")]
        public string OraIncarcare { get; set; }
        [MaxLength(100)]
        [DisplayName("Data Descarcare")]
        public string DataDescarcare { get; set; }
        [MaxLength(100)]
        [DisplayName("Ora Descarcare")]
        public string OraDescarcare { get; set; }
        [DisplayName("Consum Gaz")]
        public int ConsumGaz { get; set; }
        [DisplayName("Consum Electricitate")]
        public int ConsumElectricitate { get; set; }
    }
}
