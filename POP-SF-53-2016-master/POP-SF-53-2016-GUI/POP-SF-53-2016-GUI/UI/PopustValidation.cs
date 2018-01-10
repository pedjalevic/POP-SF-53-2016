using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace POP_SF_53_2016_GUI.UI
{
    public class PopustValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var vrednost = value as string;

            try
            {

                int parsirano;
                bool rezultat = int.TryParse(vrednost, out parsirano);
                if (!rezultat)
                    return new ValidationResult(false, "Morate uneti pozitivan broj");
                else if (parsirano < 0)
                    return new ValidationResult(false, "Morate uneti pozitivan broj");
                else if (parsirano < 5 || parsirano > 90)
                    return new ValidationResult(false, "Uneseni popust nije omogucen");
                else
                    return new ValidationResult(true, null);
            }
            catch (Exception)
            {
                return new ValidationResult(false, "Morate uneti pozitivan ceo broj");
            }
        }
    }
}
