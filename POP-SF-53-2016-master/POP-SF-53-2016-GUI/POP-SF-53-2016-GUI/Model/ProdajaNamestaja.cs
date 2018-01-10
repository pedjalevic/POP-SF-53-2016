using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using POP_SF_53_2016_GUI.Model;

namespace POP_SF_53_2016_GUI.Model
{
    public class ProdajaNamestaja : INotifyPropertyChanged, ICloneable
    {
        public ProdajaNamestaja()
        {
            datumProdaje = DateTime.Today;
            stavkeProdaje = new ObservableCollection<StavkaProdaje>();
            dodatneUsluge = new ObservableCollection<DodatneUsluge>();
            dodatneUslugeId = new List<int>();
        }
        private int id;


        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        private List<int> dodatneUslugeId;

        public List<int> DodatneUslugeId
        {
            get { return dodatneUslugeId; }
            set { dodatneUslugeId = value; }
        }

        private ObservableCollection<StavkaProdaje> stavkeProdaje;
        [XmlIgnore]
        public ObservableCollection<StavkaProdaje> StavkeProdaje
        {
            get
            {

                return stavkeProdaje;
            }
            set
            {
                stavkeProdaje = value;
                OnPropertyChanged("StavkeProdaje");
            }
        }

        private DateTime datumProdaje;

        public DateTime DatumProdaje

        {
            get { return datumProdaje; }
            set
            {
                datumProdaje = value;
                OnPropertyChanged("DatumProdaje");
            }
        }

        private int brojRacuna;

        public int BrojRacuna
        {
            get { return brojRacuna; }
            set
            {
                brojRacuna = value;
                OnPropertyChanged("BrojRacuna");
            }
        }

        private string kupac;

        public string Kupac
        {
            get { return kupac; }
            set
            {
                kupac = value;
                OnPropertyChanged("Kupac");
            }
        }

        public const double PDV = 0.02;

        public event PropertyChangedEventHandler PropertyChanged;



        private double ukupanIznos;

        public double UkupanIznos
        {
            get { return ukupanIznos = stavkeProdaje.Sum(item => item.Cena) + dodatneUsluge.Sum(item => item.Cena); }
            set
            {
                ukupanIznos = stavkeProdaje.Sum(item => item.Cena) + dodatneUsluge.Sum(item => item.Cena);


                OnPropertyChanged("UkupanIznos");

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

        private ObservableCollection<DodatneUsluge> dodatneUsluge;
        [XmlIgnore]
        public ObservableCollection<DodatneUsluge> DodatneUsluge
        {
            get
            {

                return dodatneUsluge;
            }
            set
            {
                dodatneUsluge = value;
                OnPropertyChanged("DodatneUsluge");
            }
        }

        public override string ToString()
        {
            if (!Obrisan)
            {
                var ispis = $"{Id}. {DatumProdaje} {BrojRacuna} {Kupac} ";

                return ispis;
            }
            return null;


        }
        public static ProdajaNamestaja PronadjiProdaju(int id)
        {
            foreach (var prodaja in Projekat.Instance.Prodaja)
            {
                if (prodaja.Id == id)
                {
                    return prodaja;
                }

            }
            return null;
        }
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public object Clone()
        {
            ProdajaNamestaja kopija = new ProdajaNamestaja();
            kopija.Id = Id;
            kopija.Kupac = Kupac;
            kopija.DatumProdaje = DatumProdaje;
            kopija.UkupanIznos = UkupanIznos;
            kopija.BrojRacuna = BrojRacuna;
            ObservableCollection<StavkaProdaje> kopijaStavki = new ObservableCollection<StavkaProdaje>(StavkeProdaje);
            ObservableCollection<DodatneUsluge> kopijaUsluga = new ObservableCollection<DodatneUsluge>(DodatneUsluge);
            kopija.StavkeProdaje = kopijaStavki;
            kopija.DodatneUsluge = kopijaUsluga;
            return kopija;
        }
    }
}