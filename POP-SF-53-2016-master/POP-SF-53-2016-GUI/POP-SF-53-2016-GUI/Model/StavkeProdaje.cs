using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace POP_SF_53_2016_GUI.Model
{
    public class StavkaProdaje : INotifyPropertyChanged
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private double cena;



        private int kolicina;

        public int Kolicina
        {
            get { return kolicina; }
            set { kolicina = value; }
        }

        private int namestajProdajaId;

        public int NamestajProdajaId

        {
            get { return namestajProdajaId; }
            set
            {
                namestajProdajaId = value;
                OnPropertyChanged("NamestajProdajaId");
            }
        }
        private Namestaj namestajProdaja;

        public event PropertyChangedEventHandler PropertyChanged;
        [XmlIgnore]
        public Namestaj NamestajProdaja

        {
            get
            {

                if (namestajProdaja == null)
                {

                    namestajProdaja = Namestaj.PronadjiNamestaj(namestajProdajaId);
                }
                return namestajProdaja;
            }
            set
            {
                namestajProdaja = value;
                namestajProdajaId = namestajProdaja.Id;
                OnPropertyChanged("NamestajProdaja");
            }

        }

        private int dodatnaUslugaId;

        public int DodatnaUslugaId

        {
            get { return dodatnaUslugaId; }
            set
            {
                dodatnaUslugaId = value;
                OnPropertyChanged("DodatneUslugaId");
            }
        }

        private DodatneUsluge dodatneUsluge;
        [XmlIgnore]
        public DodatneUsluge DodatneUsluge
        {
            get
            {
                if (dodatneUsluge == null)
                {

                    dodatneUsluge = DodatneUsluge.PronadjiUslugu(DodatnaUslugaId);
                }
                return dodatneUsluge;
            }
            set
            {
                dodatneUsluge = value;

                dodatnaUslugaId = dodatneUsluge.Id;
                OnPropertyChanged("DodatneUsluge");
            }
        }
        private bool obrisan;

        public bool Obrisan
        {
            get { return obrisan; }
            set
            {
                obrisan = value;
                OnPropertyChanged("Obrisan");
            }
        }
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public double Cena
        {
            get { return cena; }
            set
            {
                cena = value;
                OnPropertyChanged("Cena");
            }
        }
        public override string ToString()
        {
            string ispis = "";
            ispis += NamestajProdaja.Naziv;
            return ispis;

        }
        public static StavkaProdaje PronadjiStavku(int id)
        {
            foreach (var stavka in Projekat.Instance.StavkeProdaje)
            {
                if (stavka.Id == id)
                {
                    return stavka;
                }

            }
            return null;
        }
        public static ObservableCollection<StavkaProdaje> PronadjiStavke(List<int> id)
        {
            ObservableCollection<StavkaProdaje> stavke = new ObservableCollection<StavkaProdaje>();
            if (id != null)
            {
                for (int i = 0; i < id.Count; i++)
                {

                    stavke.Add(PronadjiStavku(id[i]));
                }
                return stavke;
            }
            return null;
        }
        public static List<int> PronadjiIdove(ObservableCollection<StavkaProdaje> stavke)
        {
            var lista = new List<int>();
            if (stavke != null)
            {
                for (int i = 0; i < stavke.Count; i++)
                {
                    lista.Add(stavke[i].Id);
                }
                return lista;
            }
            return null;
        }
    }
}
