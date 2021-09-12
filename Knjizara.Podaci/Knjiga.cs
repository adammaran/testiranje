using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knjizara.Podaci
{
    public class Knjiga
    {
        public Guid Id { get; set; }
        public string Naziv { get; set; }
        public double Cena { get; set; }
        public Kategorija Kategorija { get; set; }

        public Knjiga(Guid id, string naziv, double cena, Kategorija kategorija)
        {
            if (string.IsNullOrWhiteSpace(naziv))
            {
                throw new ArgumentException();
            }

            if (cena <= 0.0)
            {
                throw new ArgumentException();
            }


            this.Id = id;
            this.Naziv = naziv;
            this.Cena = cena;
            this.Kategorija = kategorija ?? throw new ArgumentNullException();
        }

        public Knjiga() { }
    }
}
