using POP_SF_53_2016_GUI.Model;
using POP_SF_53_2016_GUI.Utils;
using POP_SF_53_2016_GUI.DAO;
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
    /// Interaction logic for AkcijaDodavanjeIzmena.xaml
    /// </summary>
    public partial class AkcijaDodavanjeIzmena : Window
    {
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };
        public Akcija akcija;
        private Operacija operacija;
        public AkcijaDodavanjeIzmena(Akcija akcija, Operacija operacija)
        {
            InitializeComponent();
            this.operacija = operacija;
            this.akcija = akcija;
            dpPocetak.DataContext = akcija;
            dpKraj.DataContext = akcija;
            tbPopust.DataContext = akcija;
            dgNamestajAkcija.ItemsSource = akcija.NamestajPopust;
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            if (operacija == Operacija.DODAVANJE)
            {
                AkcijaDAO.DodavanjeAkcije(akcija);
            }
            AkcijaDAO.IzmenaAkcije(akcija);
            Close();
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            PreuzmiNamestaj pn = new PreuzmiNamestaj();
            if (pn.ShowDialog() == true)
            {
                akcija = AkcijaDAO.DodavanjeNaAkciju(akcija, pn.Namestaj);
            }

        }

        private void btnUkloni_Click(object sender, RoutedEventArgs e)
        {
            var izabrana = dgNamestajAkcija.SelectedItem as Namestaj;
            akcija = AkcijaDAO.BrisanjeSaAkcije(akcija, izabrana);
        }
        private void dgNamestajAkcija_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisan" || (string)e.Column.Header == "Id")
                e.Cancel = true;
        }
    }
}
