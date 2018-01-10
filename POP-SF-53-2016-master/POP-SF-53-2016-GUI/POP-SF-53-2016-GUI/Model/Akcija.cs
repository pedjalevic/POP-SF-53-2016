using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Threading.Tasks;

namespace POP_SF_53_2016_GUI.Model
{
     public class Akcija : INotifyPropertyChanged, ICloneable
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

        private DateTime pocetakAkcije;

        public DateTime PocetakAkcije
        {
            get { return pocetakAkcije; }
            set
            {
                pocetakAkcije = value;
                OnPropertyChanged("PocetakAkcije");
            }
        }

        private DateTime krajAkcije;

        public DateTime KrajAkcije
        {
            get { return krajAkcije; }
            set
            {
                krajAkcije = value;
                OnPropertyChanged("KrajAkcije");
            }
        }

        private double popust;

        public double Popust
        {
            get { return popust; }
            set
            {
                popust = value;
                OnPropertyChanged("Popust");
            }
        }

        private int namestajPopustId;

        public int NamestajPopustId
        {
            get { return namestajPopustId; }
            set
            {
                namestajPopustId = value;
                OnPropertyChanged("NamestajPopustId");
            }
        }
        private ObservableCollection<Namestaj> namestajPopust;
        [XmlIgnore]
        public ObservableCollection<Namestaj> NamestajPopust
        {
            get
            {
                return namestajPopust;
            }
            set
            {
                namestajPopust = value;
                OnPropertyChanged("NamestajPopust");
            }
        }
        public Akcija()
        {
            pocetakAkcije = DateTime.Now;
            krajAkcije = DateTime.Now;
            namestajPopust = new ObservableCollection<Namestaj>();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            if (!Obrisan)
            {


                string ispis = $"{Id}. {PocetakAkcije} {KrajAkcije} {Popust} ";
                return ispis;
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
        public static Akcija PronadjiAkciju(int id)
        {
            foreach (var akcija in Projekat.Instance.Akcije)
            {
                if (akcija.Id == id)
                {
                    return akcija;
                }

            }
            return null;
        }
        public object Clone()
        {
            Akcija kopija = new Akcija();
            kopija.Id = Id;
            kopija.PocetakAkcije = PocetakAkcije;
            kopija.KrajAkcije = KrajAkcije;
            kopija.NamestajPopust = NamestajPopust;
            kopija.Popust = Popust;
            return kopija;
        }
    }
}
