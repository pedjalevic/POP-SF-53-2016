using POP_SF_53_2016_GUI.Model;
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
    /// Interaction logic for IzlistajStavke.xaml
    /// </summary>
    public partial class IzlistajStavke : Window
    {
        public ProdajaNamestaja prodaja;
        public Akcija akcija;
        public IzlistajStavke(ProdajaNamestaja prodaja = null, Akcija akcija = null)
        {
            InitializeComponent();
            this.prodaja = prodaja;
            this.akcija = akcija;
            if (prodaja == null)
                tbIspis.Text = String.Format(@"\n\n\n\n{0,12}{1,8}{2,8}\n", "Namestaj", "Cena", "Akcijska cena");
            tbIspis.Text = IspisZaAkciju();

        }

        private void Izlaz(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dgIspisStavki_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Id" || (string)e.Column.Header == "NamestajProdajaId" ||
                (string)e.Column.Header == "DodatnaUslugaId" || (string)e.Column.Header == "Obrisan")
                e.Cancel = true;
        }
        private string IspisZaAkciju()
        {
            string zaglavlje = String.Format(@"{0,12}{1,8}{2,8}\n", "Namestaj", "Cena", "Akcijska cena");
            string ispis = "";
            foreach (var item in akcija.NamestajPopust)
            {
                string ispisi = String.Format("\n\n\n{0,12}|{1,8:f}|{2,8:f}|\n", item.Naziv, item.Cena, item.Cena - ((item.Cena * akcija.Popust) / 100));
                ispis += ispisi;
            }
            return ispis;
        }
    }
}
