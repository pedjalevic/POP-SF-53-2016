using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_53_2016_GUI.Model
{
    class ProdajaNamestaja
    {
        public int Id { get; set; }
        public List<Namestaj> NamestajZaProdaju { get; set; }
        public DateTime DatumProdaje { get; set; }
        public int BrojRacuna { get; set; }
        public String Kupac { get; set; }
        public double PDV { get; set; }
        public double UkupanIznos { get; set; }
    }
}
