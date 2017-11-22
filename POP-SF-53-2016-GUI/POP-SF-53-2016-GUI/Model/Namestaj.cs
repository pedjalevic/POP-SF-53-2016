using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_53_2016_GUI.Model
{
    class Namestaj
    {
        public int Id { get; set; }
        public bool Obrisan { get; set; }
        public string Naziv { get; set; }
        public string Sifra { get; set; }
        public double JedinicnaCena { get; set; }
        public int KolicinaUMagacinu { get; set; }
        public string Tip { get; set; }
        public TipNamestaja TipNamestaja { get; set; }
    }
}
