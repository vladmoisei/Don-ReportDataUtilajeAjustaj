using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace RaportareAjustajV2
{
    public static class CalculeAuxiliar
    {
        // Functie calcul masa
        public static double CalculMasa(int diametru, int nrBare, int lungime)
        {
            return (diametru / 2) * (diametru / 2) * 3.14 * 7.85 * lungime * nrBare / 1000000;
        }

        // Functie convertire din string in dateTime fomrat
        public static DateTime ReturnareDataFromString(string dateToParse)
        {

            DateTime parsedDate = DateTime.ParseExact(dateToParse,
                                                      "dd/MM/yyyy HH:mm",
                                                      CultureInfo.InvariantCulture);
            return parsedDate;
        }

        // Functie verificare data este din ziua de azi
        public static bool IsCurrentDay(DateTime data)
        {
            if (data.Day == DateTime.Now.Day) return true;
            return false;
        }

        // Functie verificare data este din luna curenta
        public static bool IsCurrentMonth(DateTime data)
        {
            if (data.Month == DateTime.Now.Month) return true;
            return false;
        }

        // Functie verificare data cuprinse intre 2 date (format string) 
        public static bool IsDateBetween(string dataItemString, string dataFromString, string dataToString)
        {
            // Convert string data received from View to DateTime format
            DateTime dataItem = CalculeAuxiliar.ReturnareDataFromString(dataItemString);
            DateTime dataFrom = CalculeAuxiliar.ReturnareDataFromString(dataFromString + " 00:00");
            DateTime dataTo = CalculeAuxiliar.ReturnareDataFromString(dataToString + " 00:00");
            if (dataItem.CompareTo(dataFrom) >= 0)
            {
                if (dataItem.CompareTo(dataTo) <= 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
