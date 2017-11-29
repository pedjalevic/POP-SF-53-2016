using POP_SF_53_2016_GUI.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_53_2016_GUI.Model
{
    public class Projekat
    {
        public static Projekat Instance { get; } = new Projekat();
        public ObservableCollection<Namestaj> Namestaj { get; set; }
        public ObservableCollection<TipNamestaja> TipNamestaja { get; set; }
        public ObservableCollection<Akcija> Akcije { get; set; }
        public ObservableCollection<Korisnik> Korisnici { get; set; }
        public ObservableCollection<ProdajaNamestaja> Prodaja { get; set; }
        public ObservableCollection<DodatneUsluge> DodatneUsluge { get; set; }

        private Projekat()
        {
            Namestaj = GenericSerializer.Deserialize<Namestaj>("Namestaj.xml");
            TipNamestaja = GenericSerializer.Deserialize<TipNamestaja>("TipNamestaja.xml");
            Akcije = GenericSerializer.Deserialize<Akcija>("Akcije.xml");
            Korisnici = GenericSerializer.Deserialize<Korisnik>("korisnici.xml");
            Prodaja = GenericSerializer.Deserialize<ProdajaNamestaja>("ProdajaNamestaja.xml");
            DodatneUsluge = GenericSerializer.Deserialize<DodatneUsluge>("DodatneUsluge.xml");

        }
    }
}
