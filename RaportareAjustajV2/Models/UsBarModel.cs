using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RaportareAjustajV2
{
    public class UsBarModel
    {
        public int UsBarModelId { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [Display(Name = "Data Introducere"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = false)]
        public DateTime DataIntroducere { get; set; }

        [Display(Name = "Data Control"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = false)]
        public DateTime DataControl { get; set; }

        public int Diametru { get; set; }

        public Furnizor Furnizor { get; set; }

        [Required]
        [MaxLength(100)]
        public string Sarja { get; set; }

        [DisplayName("Calitate Otel")]
        public int CalitateOtelModelId { get; set; }

        [DisplayName("Calitate Otel")]
        public virtual CalitateOtelModel CalitateOtel { get; set; }

        [DisplayName("Stare Suprafata")]
        public StareMaterial StareMaterial { get; set; }

        [DisplayName("Clasa 3")]
        public int Clasa3 { get; set; }

        [DisplayName("Marime Defect 3 [mm]")]
        public double MarimeDefect3 { get; set; }

        [DisplayName("Clasa 2")]
        public int Clasa2 { get; set; }

        [DisplayName("Marime Defect 2 [mm]")]
        public double MarimeDefect2 { get; set; }

        [DisplayName("Clasa 2+")]
        public int Clasa2Plus { get; set; }

        [DisplayName("Marime Defect 2+ [mm]")]
        public double MarimeDefect2Plus { get; set; }

        [DisplayName("Clasa 1")]
        public int Clasa1 { get; set; }

        [DisplayName("Marime Defect 1 [mm]")]
        public double MarimeDefect1 { get; set; }

        public int SS { get; set; }

        [DisplayName("Marime Defect SS [mm]")]
        public double MarimeDefectSS { get; set; }

        [DisplayName("Tip Discontinuitate")]
        public TipDiscontinuitate TipDiscontinuitate { get; set; }

        [DisplayName("Range [mm]")]
        public string Range { get; set; }

        public string Gain { get; set; }

        [MaxLength(250)]
        public string Observatii { get; set; }
    }
}
