using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace RaportareAjustajV2
{
    public static class CalculeAuxiliar
    {

        public static double CalculMasa(int diametru, int nrBare, int lungime)
        {            
            return (diametru/2)* (diametru / 2)*3.14*7.85*lungime*nrBare/1000000;
        }

        public static DateTime ReturnareDataFromString(string dateToParse)
        {

            DateTime parsedDate = DateTime.ParseExact(dateToParse,
                                                      "dd/MM/yyyy HH:mm",
                                                      CultureInfo.InvariantCulture);
            return parsedDate;
        }

        public static bool IsCurrentDay(DateTime data)
        {
            if (data.Day == DateTime.Now.Day) return true;
            return false;
        }
    }
}
