using POP_SF_53_2016_GUI.DAO;
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
        public ObservableCollection<StavkaProdaje> StavkeProdaje { get; set; }
        public Salon Salon { get; set; }

        private Projekat()
        {
            Namestaj = NamestajDAO.SavNamestaj();
            TipNamestaja = TipNamestajaDAO.SviTipovi();
            Akcije = AkcijaDAO.SveAkcije();
            Korisnici = KorisnikDAO.SviKorisnici();
            DodatneUsluge = UslugeDAO.SveUsluge();
            Prodaja = ProdajaDAO.SveProdaje();
            Salon = SalonDAO.PrikazPodataka();

        }
    }
}
