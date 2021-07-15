using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RaportareAjustajV2.Models
{
    public class StrungarieCilindriModel
    {
        public int StrungarieCilindriModelId { get; set; }

        [Display(Name = "Data Introducere"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}", ApplyFormatInEditMode = false)]
        public DateTime DataIntroducere { get; set; }

        [MaxLength(50)]
        [DisplayName("Eticheta")]
        public string CodCartellino { get; set; }


        [MaxLength(30)]
        public string Furnizor { get; set; }

        [MaxLength(50)]
        public string Sarja { get; set; }

        [MaxLength(10)]
        public string Diametru { get; set; }

        [MaxLength(50)]
        public string Calitate { get; set; }

        [MaxLength(10)]
        public string Lungime { get; set; }

        [MaxLength(10)]
        public string Greutate { get; set; }

        [MaxLength(250)]
        [DisplayName("Motiv declasare")]
        public string MotivNeexpediere { get; set; }

        [MaxLength(250)]
        [DisplayName("Descr.SdF")]
        public string DescrSdF { get; set; }

        [DisplayName("In Lucru")]
        public bool IsInLucru { get; set; }

        [DisplayName("Diam Final")]
        [Range(0, 999.99)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal DiametruFinal { get; set; }

        [Display(Name = "Data In Lucru"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}", ApplyFormatInEditMode = false)]
        public DateTime DataBifatInLucru { get; set; }

        [Display(Name = "Data Diam Final"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}", ApplyFormatInEditMode = false)]
        public DateTime DataDiamFinal { get; set; }

        [MaxLength(250)]
        public string ComentariuStrungarie { get; set; }



    }
}
