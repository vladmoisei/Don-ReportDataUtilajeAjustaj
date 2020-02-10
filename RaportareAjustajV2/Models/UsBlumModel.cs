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
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataIntroducere { get; set; }

        [Display(Name = "Data Control"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataControl { get; set; }

        [Required]
        [MaxLength(100)]
        public string Sarja { get; set; }

        [Required]
        [MaxLength(100)]
        public string FormatBlum { get; set; }

        public Furnizor Furnizor { get; set; }

        public int CalitateOtelModelId { get; set; }

        [DisplayName("Calitate Otel")]
        public virtual CalitateOtelModel CalitateOtel { get; set; }

        [MaxLength(100)]
        [DisplayName("Ø programat")]
        public string FiProgramat { get; set; }

        public int Fir1 { get; set; }

        public int Blum1 { get; set; }

        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Nr invalid, pune punct!")]
        public double MarimeDefect1 { get; set; }

        public int Fir2 { get; set; }

        public int Blum2 { get; set; }

        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Nr invalid, pune punct!")]
        public double MarimeDefect2 { get; set; }

        public int Fir3 { get; set; }

        public int Blum3 { get; set; }

        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Nr invalid, pune punct!")]
        public double MarimeDefect3 { get; set; }

        [MaxLength(250)]
        public string Observatii { get; set; }
    }
}
