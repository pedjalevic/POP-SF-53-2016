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
    /// Interaction logic for TipNamestajaDodavanjeIzmena.xaml
    /// </summary>
    public partial class TipNamestajaDodavanjeIzmena : Window
    {
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };
        public TipNamestaja tipNamestaja;
        private Operacija operacija;
        public TipNamestajaDodavanjeIzmena(TipNamestaja tipNamestaja, Operacija operacija)
        {
            InitializeComponent();
            this.tipNamestaja = tipNamestaja;
            this.operacija = operacija;
            tbNazivTipa.DataContext = tipNamestaja;
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;

            if (operacija == Operacija.DODAVANJE)
            {

                TipNamestajaDAO.DodavanjeTipa(tipNamestaja);
            }
            TipNamestajaDAO.IzmenaTipa(tipNamestaja);
            Close();
        }
    }
}
