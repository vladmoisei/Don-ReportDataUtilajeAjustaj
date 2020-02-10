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
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataIntroducere { get; set; }

        [Display(Name = "Data Control"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataControl { get; set; }

        public int Diametru { get; set; }

        public Furnizor Furnizor { get; set; }

        [Required]
        [MaxLength(100)]
        public string Sarja { get; set; }

        public int CalitateOtelModelId { get; set; }

        [DisplayName("Calitate Otel")]
        public virtual CalitateOtelModel CalitateOtel { get; set; }

        [MaxLength(100)]
        [DisplayName("Stare Material")]
        public StareMaterial StareMaterial { get; set; }

        public int Clasa3 { get; set; }

        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Nr invalid, pune punct!")]
        [DisplayName("Marime Defect[mm]")]
        public double MarimeDefect3 { get; set; }

        public int Clasa2 { get; set; }

        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Nr invalid, pune punct!")]
        [DisplayName("Marime Defect[mm]")]
        public double MarimeDefect2 { get; set; }

        public int Clasa2Plus { get; set; }

        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Nr invalid, pune punct!")]
        [DisplayName("Marime Defect[mm]")]
        public double MarimeDefect2Plus { get; set; }

        public int Clasa1 { get; set; }

        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Nr invalid, pune punct!")]
        [DisplayName("Marime Defect[mm]")]
        public double MarimeDefect1 { get; set; }

        public int SS { get; set; }

        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Nr invalid, pune punct!")]
        [DisplayName("Marime Defect[mm]")]
        public double MarimeDefectSS { get; set; }

        [DisplayName("Tip Discontinuitate")]
        public TipDiscontinuitate TipDiscontinuitate { get; set; }

        [MaxLength(250)]
        public string Observatii { get; set; }
    }
}
