using POP_SF_53_2016_GUI.DAO;
using POP_SF_53_2016_GUI.Model;
using POP_SF_53_2016_GUI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace POP_SF_53_2016_GUI.UI
{
    /// <summary>
    /// Interaction logic for KorisniciDodavanjeIzmena.xaml
    /// </summary>
    public partial class KorisniciDodavanjeIzmena : Window
    {
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };
        public Korisnik korisnik;
        private Operacija operacija;
        public KorisniciDodavanjeIzmena(Korisnik korisnik, Operacija operacija)
        {
            InitializeComponent();
            this.korisnik = korisnik;
            this.operacija = operacija;

            tbIme.DataContext = korisnik;
            tbPrezime.DataContext = korisnik;
            tbKorisnickoIme.DataContext = korisnik;
            tbLozinka.DataContext = korisnik;
            cbTipKorisnika.ItemsSource = Enum.GetValues(typeof(TipKorisnika)).Cast<TipKorisnika>();
            cbTipKorisnika.DataContext = korisnik;
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            var tip_korisnika = (TipKorisnika)cbTipKorisnika.SelectedItem;
            if (operacija == Operacija.DODAVANJE)
            {

                KorisnikDAO.DodavanjeKorisnika(korisnik);
            }
            KorisnikDAO.IzmenaKorisnika(korisnik);
            Close();
        }
    }
}
