using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_53_2016_GUI.Model
{
    public class ProdajaNamestaja : INotifyPropertyChanged
    {
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
        private List<int> namestajProdajaId;

        public List<int> NamestajProdajaId

        {
            get { return namestajProdajaId; }
            set
            {
                namestajProdajaId = value;
                OnPropertyChanged("NamestajProdajaId");
            }
        }
        private ObservableCollection<Namestaj> namestajProdaja;

        public ObservableCollection<Namestaj> NamestajProdaja

        {
            get
            {
                if (namestajProdaja == null)
                {
                    for (int i = 0; i < namestajProdajaId.Count; id++)
                        namestajProdaja.Add(Namestaj.PronadjiNamestaj(namestajProdajaId[i]));
                }
                return namestajProdaja;
            }
            set
            {
                namestajProdaja = value;
                for (int i = 0; i < namestajProdaja.Count; i++)
                    namestajProdajaId.Add(namestajProdaja[i].Id);
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

        private List<int> dodatnaUslugaId;

        public List<int> DodatnaUslugaId
        {
            get { return dodatnaUslugaId; }
            set
            {
                dodatnaUslugaId = value;
                OnPropertyChanged("DodatnaUslugaId");
            }
        }
        private ObservableCollection<DodatneUsluge> dodatnaUsluga;

        public ObservableCollection<DodatneUsluge> DodatnaUsluga
        {
            get
            {
                if (dodatnaUsluga == null)
                {
                    for (int i = 0; i < dodatnaUslugaId.Count; id++)
                        dodatnaUsluga.Add(Model.DodatneUsluge.PronadjiUslugu(dodatnaUslugaId[i]));
                }
                return dodatnaUsluga;
            }
            set
            {
                dodatnaUsluga = value;
                for (int i = 0; i < dodatnaUsluga.Count; i++)
                    dodatnaUslugaId.Add(dodatnaUsluga[i].Id);
                OnPropertyChanged("DodatnaUsluga");
            }
        }


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
                for (int i = 0; i < namestajProdaja.Count; i++)
                {
                    ispis += namestajProdaja[i].Naziv + " ,";

                }

                for (int i = 0; i < dodatnaUsluga.Count; i++)
                {
                    ispis += dodatnaUsluga[i].Naziv + " ,";

                }
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
    }
}
