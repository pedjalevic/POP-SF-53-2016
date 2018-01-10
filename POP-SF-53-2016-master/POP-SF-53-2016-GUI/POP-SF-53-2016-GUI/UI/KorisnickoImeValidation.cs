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
    class KorisnickoImeValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string korIme = value as string;
            if (korIme.Length == 0)
            {
                return new ValidationResult(false, "Morate uneti korisnicko ime");
            }
            if (korIme == null)
            {
                return new ValidationResult(false, "Morate uneti korisnicko ime");
            }
            foreach (var korisnik in Projekat.Instance.Korisnici)
            {
                if (korisnik.KorisnickoIme == korIme)
                {
                    return new ValidationResult(false, "To korisnicko ime vec postoji");
                }
            }
            return new ValidationResult(true,null);

        }
    }

}
