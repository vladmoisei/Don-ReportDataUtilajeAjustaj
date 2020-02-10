using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RaportareAjustajV2
{
    public class CalitateOtelModel
    {
        public int CalitateOtelModelId { get; set; }

        [MaxLength(100)]
        public string Valoare { get; set; }
    }
}
