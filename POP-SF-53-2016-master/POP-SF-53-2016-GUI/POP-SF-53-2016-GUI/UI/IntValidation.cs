using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace POP_SF_53_2016_GUI.UI
{
    public class IntValidation : ValidationRule
    {
        public static bool uspesno { get; set; } = true;
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string vrednost = value as string;
            if (vrednost == null)
                return new ValidationResult(false, "Polje ne sme biti prazno");
            try
            {
                int parsirano;
                bool rezultat = Int32.TryParse(vrednost, out parsirano);
                if (!rezultat)
                {

                    return new ValidationResult(false, "Morate uneti pozitivan ceo broj");
                }
                else if (parsirano < 0)
                {

                    return new ValidationResult(false, "Morate uneti pozitivan ceo broj");
                }
                else
                {

                    return new ValidationResult(true, null);
                }
            }
            catch (Exception)
            {
                return new ValidationResult(false, "Morate uneti pozitivan ceo broj za cenu/kolicinu");
            }
        }
    }
}

