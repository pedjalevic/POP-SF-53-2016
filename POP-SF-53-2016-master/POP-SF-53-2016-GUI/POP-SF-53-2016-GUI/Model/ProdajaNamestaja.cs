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
    public class ProdajaNamestaja : INotifyPropertyChanged
    {
        public ProdajaNamestaja()
        {
            datumProdaje = DateTime.Today;
            stavkeProdaje = new ObservableCollection<StavkaProdaje>();
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
        private List<int> stavkaProdajeId;

        public List<int> StavkaProdajeId

        {
            get
            {
                return stavkaProdajeId;
            }
            set
            {
                stavkaProdajeId = value;
                OnPropertyChanged("StavkaProdajeId");
            }
        }

        private ObservableCollection<StavkaProdaje> stavkeProdaje;
        [XmlIgnore]
        public ObservableCollection<StavkaProdaje> StavkeProdaje
        {
            get
            {
                if (stavkeProdaje == null)
                    stavkeProdaje = StavkaProdaje.PronadjiStavke(stavkaProdajeId);
                return stavkeProdaje;
            }
            set
            {
                stavkeProdaje = value;
                if (stavkeProdaje != null)
                    stavkaProdajeId = StavkaProdaje.PronadjiIdove(stavkeProdaje);
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
            get { return ukupanIznos; }
            set
            {
                ukupanIznos = value;
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
            kopija.Id = id;
            kopija.Kupac = kupac;
            kopija.UkupanIznos = ukupanIznos;
            kopija.BrojRacuna = brojRacuna;
            kopija.StavkeProdaje = stavkeProdaje;
            return kopija;
        }
    }
}
