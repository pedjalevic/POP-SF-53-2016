using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_53_2016_GUI.Model
{
     public class Salon
    {
        public int Id { get; set; }
        private string Naziv { get; set; }
        private string Adresa { get; set; }
        private string Telefon { get; set; }
        private string Email { get; set; }
        private string AdresaInternetSajta { get; set; }
        private int PIB { get; set; }
        private int MaticniBroj { get; set; }
        private string BrojZiroRacuna { get; set; }
    }
}
