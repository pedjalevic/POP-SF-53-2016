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
    /// Interaction logic for NamestajDodavanjeIzmena.xaml
    /// </summary>
    public partial class NamestajDodavanjeIzmena : Window
    {
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };
        public Namestaj namestaj;
        private Operacija operacija;
        public NamestajDodavanjeIzmena(Namestaj namestaj, Operacija operacija)
        {
            InitializeComponent();
            this.namestaj = namestaj;
            this.operacija = operacija;
            cbTipNamestaja.ItemsSource = Projekat.Instance.TipNamestaja;
            tbNazivNamestaja.DataContext = namestaj;
            tbCenaNamestaja.DataContext = namestaj;
            tbKolicinaNamestaja.DataContext = namestaj;
            cbTipNamestaja.DataContext = namestaj;
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            if (Provera() == true)
            {
                return;
            }
            this.DialogResult = true;
            if (cbTipNamestaja.SelectedItem != null)
                namestaj.Sifra = tbNazivNamestaja.Text.Substring(0, 2).ToUpper() + tbKolicinaNamestaja.Text + cbTipNamestaja.Text.Substring(0, 2).ToUpper();
            var izabraniTip = (TipNamestaja)cbTipNamestaja.SelectedItem;
            if (operacija == Operacija.DODAVANJE)
            {
                NamestajDAO.DodavanjeNamestaja(namestaj);

            }
            else
                NamestajDAO.IzmenaNamestaja(namestaj);
            this.Close();
        }
        public bool Provera()
        {
            BindingExpression be1 = tbNazivNamestaja.GetBindingExpression(TextBox.TextProperty);
            be1.UpdateSource();
            BindingExpression be2 = tbKolicinaNamestaja.GetBindingExpression(TextBox.TextProperty);
            be1.UpdateSource();
            BindingExpression be3 = tbCenaNamestaja.GetBindingExpression(TextBox.TextProperty);
            be1.UpdateSource();
            if (Validation.GetHasError(tbNazivNamestaja) == true || Validation.GetHasError(tbKolicinaNamestaja) == true || Validation.GetHasError(tbCenaNamestaja) == true)
            {
                return true;
            }
            return false;
        }
    }
}
