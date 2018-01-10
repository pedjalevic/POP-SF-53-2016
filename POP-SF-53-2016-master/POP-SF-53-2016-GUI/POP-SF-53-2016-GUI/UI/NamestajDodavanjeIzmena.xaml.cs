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
            tbSifraNamestaja.DataContext = namestaj;
            tbKolicinaNamestaja.DataContext = namestaj;
            cbTipNamestaja.DataContext = namestaj;
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            var izabraniTip = (TipNamestaja)cbTipNamestaja.SelectedItem;


            if (operacija == Operacija.DODAVANJE)
            {
                NamestajDAO.DodavanjeNamestaja(namestaj);
            }

            NamestajDAO.IzmenaNamestaja(namestaj);
            Close();
        }
    }
}
