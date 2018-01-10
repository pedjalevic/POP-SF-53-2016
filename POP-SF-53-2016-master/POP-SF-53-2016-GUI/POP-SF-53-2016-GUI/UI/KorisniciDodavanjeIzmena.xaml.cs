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
            if (Provera() == true)
            {
                return;
            }
            this.DialogResult = true;
            var tip_korisnika = (TipKorisnika)cbTipKorisnika.SelectedItem;
            if (operacija == Operacija.DODAVANJE)
            {
                KorisnikDAO.DodavanjeKorisnika(korisnik);
            }
            else
                KorisnikDAO.IzmenaKorisnika(korisnik);
            this.Close();
        }
        public bool Provera()
        {
            BindingExpression be1 = tbIme.GetBindingExpression(TextBox.TextProperty);
            be1.UpdateSource();
            BindingExpression be2 = tbPrezime.GetBindingExpression(TextBox.TextProperty);
            be1.UpdateSource();
            BindingExpression be3 = tbKorisnickoIme.GetBindingExpression(TextBox.TextProperty);
            be1.UpdateSource();
            BindingExpression be4 = tbLozinka.GetBindingExpression(TextBox.TextProperty);
            be1.UpdateSource();
            if (Validation.GetHasError(tbIme) == true || Validation.GetHasError(tbPrezime) == true
                || Validation.GetHasError(tbKorisnickoIme) == true || Validation.GetHasError(tbLozinka) == true)
            {
                return true;
            }
            return false;
        }
    }
}
