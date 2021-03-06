﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Data Introducere")]
        public string DataIntroducere { get; set; }
        [Display(Name = "Data Control"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = false)]
        public DateTime DataControl { get; set; }
        public int Diametru { get; set; }
        [MaxLength(100)]
        public string Calitate { get; set; }
        [MaxLength(100)]
        public string Sarja { get; set; }
        [DisplayName("Defect Etalon")]
        public double DefectEtalon { get; set; }
        [DisplayName("Nr bare Conforme")]
        public int NrBareConform { get; set; }
        [DisplayName("Masa Conform")]
        public double MasaConform { get; set; }
        [DisplayName("Nr bare neconforme")]
        public int NrBareNeConform { get; set; }
        [DisplayName("Masa Neconform")]
        public double MasaNeConform { get; set; }
    }
}
