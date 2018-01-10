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

namespace POP_SF_53_2016_GUI.Data
{
    /// <summary>
    /// Interaction logic for DodatneUslugeDodavanjeIzmene.xaml
    /// </summary>
    public partial class DodatneUslugeDodavanjeIzmene : Window
    {
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };
        public DodatneUsluge dodatneUsluge;
        private Operacija operacija;
        public DodatneUslugeDodavanjeIzmene(DodatneUsluge dodatneUsluge, Operacija operacija)
        {
            InitializeComponent();
            this.operacija = operacija;
            this.dodatneUsluge = dodatneUsluge;
            tbNazivUsluge.DataContext = dodatneUsluge;
            tbCenaUsluge.DataContext = dodatneUsluge;
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
        this.DialogResult = true;

        if (operacija == Operacija.DODAVANJE)
        {
                UslugeDAO.DodavanjeUsluge(dodatneUsluge);
            }

            UslugeDAO.IzmenaUsluge(dodatneUsluge);
            Close();
    }
    }
}
