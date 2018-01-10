using POP_SF_53_2016_GUI.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace POP_SF_53_2016_GUI.UI
{
    public class KolicinaValidation : ValidationRule
    {
        public static Namestaj Nam { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string vrednost = value as string;
            try
            {
                int parsirano;
                bool rezultat = Int32.TryParse(vrednost, out parsirano);
                if (!rezultat)
                    return new ValidationResult(false, "Morate uneti pozitivan ceo broj");
                else if (parsirano < 0)
                    return new ValidationResult(false, "Morate uneti pozitivan ceo broj");
                else if (parsirano > Nam.Kolicina)
                    return new ValidationResult(false, "Namestaja nema u datoj kolicini");
                else
                    return new ValidationResult(true, null);
            }
            catch (Exception)
            {
                return new ValidationResult(false, "Morate uneti pozitivan ceo broj za kolicinu");
            }
        }
    }
}
