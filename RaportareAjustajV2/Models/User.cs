using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RaportareAjustajV2
{
    
    public class User
    {
        public int UserId { get; set; }
        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }
        [MaxLength(100)]
        public string Password{ get; set; }
        [MaxLength(100)]
        public string Nume { get; set; }
        [MaxLength(100)]
        public string Prenume { get; set; }
        public UtilajeAjustaj Utilaj { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsEnable { get; set; }
    }
}
