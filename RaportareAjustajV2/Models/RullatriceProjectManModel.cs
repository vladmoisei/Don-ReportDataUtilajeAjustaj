﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RaportareAjustajV2
{
    public class RullatriceProjectManModel
    {
        public int RullatriceProjectManModelId { get; set; }
        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }
        [MaxLength(100)]
        [DisplayName("Data Introducere")]
        public string DataIntroducere { get; set; }
        public int Diametru { get; set; }
        [MaxLength(100)]
        public string Calitate { get; set; }
        [MaxLength(100)]
        public string Sarja { get; set; }
        [DisplayName("Nr bare")]
        public int NrBare { get; set; }
        [Required]
        public Motiv Motiv { get; set; }
        public int Lungime { get; set; }
        public double Masa { get; set; }
    }
}
