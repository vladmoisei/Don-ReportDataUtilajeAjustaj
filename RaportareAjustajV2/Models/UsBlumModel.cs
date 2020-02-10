using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RaportareAjustajV2
{
    public class UsBlumModel
    {
        public int UsBlumModelId { get; set; }

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

        [Required]
        [MaxLength(100)]
        public string Sarja { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Format Blum")]
        public string FormatBlum { get; set; }

        [Required]
        public Furnizor Furnizor { get; set; }

        [DisplayName("Calitate Otel")]
        public int CalitateOtelModelId { get; set; }

        [DisplayName("Calitate Otel")]
        public virtual CalitateOtelModel CalitateOtel { get; set; }

        [MaxLength(100)]
        [DisplayName("Ø programat")]
        public string FiProgramat { get; set; }

        [DisplayName("Fir 1")]
        public int Fir1 { get; set; }

        [DisplayName("Blum 1")]
        public int Blum1 { get; set; }

        [DisplayName("Marime defect 1")]
        public double MarimeDefect1 { get; set; }

        [DisplayName("Fir 2")]
        public int Fir2 { get; set; }

        [DisplayName("Blum 2")]
        public int Blum2 { get; set; }

        [DisplayName("Marime defect 2")]
        public double MarimeDefect2 { get; set; }

        [DisplayName("Fir 3")]
        public int Fir3 { get; set; }

        [DisplayName("Blum 3")]
        public int Blum3 { get; set; }

        [DisplayName("Marime defect 3")]
        public double MarimeDefect3 { get; set; }

        [MaxLength(250)]
        public string Observatii { get; set; }
    }
}
