using POP_SF_53_2016_GUI.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_53_2016_GUI.Model
{
    public class Projekat
    {
        public static Projekat Instance { get; } = new Projekat();
        public ObservableCollection<Korisnik> Korisnici { get; set; }

        private Projekat()
        {
            Korisnici = GenericSerializer.Deserialize<Korisnik>("korisnici.xml");
        }
    }
}
