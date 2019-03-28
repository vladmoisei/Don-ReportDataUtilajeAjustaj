using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaportareAjustajV2
{
    public class ViewUserModel
    {
        public ViewUserModel() { }
        public ViewUserModel(User user){
            UserId = user.UserId;
            UserName = user.UserName;
            Password = user.Password;
            Nume = user.Nume;
            Prenume = user.Prenume;
            if (user.IsAdmin) IsAdmin = 1;
            else IsAdmin = 0;
            if (user.IsEnable) IsEnable = 1;
            else IsEnable = 0;
        }
        public int UserId { get; set; }


        public string UserName { get; set; }

        public string Password { get; set; }

        public string Nume { get; set; }

        public string Prenume { get; set; }

        public int IsAdmin { get; set; }        
        public int IsEnable { get; set; }
    }
}
